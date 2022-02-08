using System.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NetTopologySuite.Geometries;
using PM.CloudPlatform.ForkliftManager.Apis.CorrPacket;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Extensions;
using PM.CloudPlatform.ForkliftManager.Apis.General;
using PM.CloudPlatform.ForkliftManager.Apis.Managers;
using PM.CloudPlatform.ForkliftManager.Apis.Options;
using PM.CloudPlatform.ForkliftManager.Apis.Sessions;

using SuperSocket;
using SuperSocket.WebSocket;
using SuperSocket.WebSocket.Server;

namespace PM.CloudPlatform.ForkliftManager.Apis.Services
{
    /// <summary>
    /// </summary>
    public class WebSocketServerHostedService : IHostedService
    {
        private readonly IOptions<ServerOption> _serverOptions;
        private readonly ClientSessionManagers _clientSessionManager;
        private readonly TerminalSessionManager _gpsTrackerSessionManager;
        private readonly IGeneralRepository _generalRepository;

        private Random rand = new Random();

        

        double lon = 34.826682222222222222222222222;
        double lat = 113.55184;


        /// <summary>
        /// </summary>
        /// <param name="serverOptions">            </param>
        /// <param name="clientSessionManager">     </param>
        /// <param name="gpsTrackerSessionManager"> </param>
        /// <param name="factory">                  </param>
        public WebSocketServerHostedService(
            IOptions<ServerOption> serverOptions,
            ClientSessionManagers clientSessionManager,
            TerminalSessionManager gpsTrackerSessionManager,
            IServiceScopeFactory factory)
        {
            _serverOptions = serverOptions ?? throw new ArgumentNullException(nameof(_serverOptions));
            _clientSessionManager = clientSessionManager ?? throw new ArgumentNullException(nameof(clientSessionManager));
            _gpsTrackerSessionManager = gpsTrackerSessionManager ?? throw new ArgumentNullException(nameof(gpsTrackerSessionManager));
            _generalRepository = factory.CreateScope().ServiceProvider.GetRequiredService<IGeneralRepository>();
        }

        /// <summary>
        /// WebSocketServer
        /// </summary>
        /// <param name="cancellationToken"> </param>
        /// <returns> </returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var host = WebSocketHostBuilder.Create()
                .ConfigureSuperSocket(opts =>
                {
                    foreach (var listener in _serverOptions.Value.WsListeners)
                    {
                        opts.AddListener(new ListenOptions()
                        {
                            Ip = listener.Ip,
                            Port = listener.Port
                        });
                    }
                })
                .UseSession<ClientSession>()
                .UseClearIdleSession()
                .UseSessionHandler(onClosed: async (s, v) =>
                {
                    await _clientSessionManager.TryRemoveBySessionId(s.SessionID);
                })
                .UseWebSocketMessageHandler(async (s, p) =>
                {
                    var package = p.Message.ToObject<ClientPackage>();

                    if (package.PackageType == PackageType.Heart)
                    {
                        if(s["VerifyCode"] is null)
                        {
                            await s.CloseAsync(CloseReason.ProtocolError, "无法验证授权码[Server]");
                        }

                        if(string.IsNullOrEmpty(package.VerifyCode))
                        {
                            await s.CloseAsync(CloseReason.ProtocolError, "无法验证授权码[Client]");
                        }

                        if (package.VerifyCode!.Equals(s["VerifyCode"].ToString()))
                        {
                            // var packet = new ClientPackage()
                            // {
                            //     PackageType = PackageType.Heart,
                            //     ClientId = "",
                            // };
                            // var msg = packet.ToJson();

                            // await s.SendAsync(msg);

                            #region 测试

                            lon += (rand.NextDouble() / 10000);
                            lat += (rand.NextDouble() / 10000);
                            var packet = new ClientPackage()
                            {
                                PackageType = PackageType.Gps,
                                Data = new
                                {
                                    // 终端Id
                                    TerminalId = "868120278343188",
                                    // 纬度
                                    Lon = lon,
                                    // 经度
                                    Lat = lat,
                                    // 高德经纬度对象
                                    GdPoint = new Point(lon, lat).ToGeoJson(),
                                    // 方向
                                    Heading = rand.Next(1, 360),
                                    // 速度
                                    Speed = rand.Next(1, 40)
                                }
                            };
                            var msg = packet.ToJson();

                            await s.SendAsync(msg);

                            var alarmPacket = new ClientPackage()
                            {
                                PackageType = PackageType.Alarm,
                                Data = new
                                {
                                    // 终端Id
                                    TerminalId = "868120278343188",
                                    // 车牌号
                                    LicensePlateNumber = "测A123456",
                                    // 超出距离
                                    Distance = 20,
                                    // 提示信息
                                    Msg = $"[868120278343188]超出围栏{20}米"
                                }
                            }.ToJson();
                            await s.SendAsync(alarmPacket);
                            #endregion
                        }
                        else
                        {
                            await s.CloseAsync(CloseReason.ProtocolError, "授权码错误");
                        }

                        return;
                    }

                    if (package.PackageType == PackageType.Login)
                    {
                        // 客户端登录,登录后下发VerifyCode
                        await _clientSessionManager.TryAddOrUpdate(p.Message, (s as ClientSession)!);

                        var client = _generalRepository.FindAsync<User>(x=>x.Id.Equals(Guid.Parse(package.ClientId)));

                        if(client is null)
                        {
                            await s.CloseAsync(CloseReason.ProtocolError, "ClientId不存在");
                        }

                        var verifyCode = Guid.NewGuid().ToString();
                        var loginPacket = new ClientPackage()
                        {
                            PackageType = PackageType.Login,
                            ClientId = package.ClientId,
                            VerifyCode = verifyCode,
                        };
                        s["VerifyCode"] = verifyCode;

                        var msg = loginPacket.ToJson();
                        await s.SendAsync(msg);
                    }
                
                    // 车辆追踪
                    if(package.PackageType == PackageType.Trace)
                    {
                        if(s["VerifyCode"] is null)
                        {
                            await s.CloseAsync(CloseReason.ProtocolError, "无法验证授权码[Server]");
                        }

                        if(string.IsNullOrEmpty(package.VerifyCode))
                        {
                            await s.CloseAsync(CloseReason.ProtocolError, "无法验证授权码[Client]");
                        }

                        _clientSessionManager.TraceTerminalId = package.Data!.ToString() ?? "";

                        if(string.IsNullOrEmpty(package.Data.ToString()))
                        {
                            _clientSessionManager.IsTrace = false;
                            var tracePacket = new ClientPackage()
                            {
                                PackageType = PackageType.Trace,
                                Data = "停止追踪"
                            }.ToJson();
                            await s.SendAsync(tracePacket);
                            return;
                        }

                        var findTerminal = _gpsTrackerSessionManager.Sessions.AsEnumerable().FirstOrDefault(x=>x.Value["TerminalId"] != null
                            && string.IsNullOrEmpty(x.Value["TerminalId"].ToString())
                            && x.Value["TerminalId"].ToString()!.Equals(_clientSessionManager.TraceTerminalId)).Value;
                        if(findTerminal is null)
                        {
                            _clientSessionManager.IsTrace = false;
                            var tracePacket = new ClientPackage()
                            {
                                PackageType = PackageType.Trace,
                                Data = "车辆不在线"
                            }.ToJson();
                            await s.SendAsync(tracePacket);
                        }
                        else
                        {
                            _clientSessionManager.IsTrace = true;
                            var tracePacket = new ClientPackage()
                            {
                                PackageType = PackageType.Trace,
                                Data = "正在追踪"
                            }.ToJson();
                            await s.SendAsync(tracePacket);
                        }
                    }
                })
                .UseInProcSessionContainer()
                .BuildAsServer();

            await host.StartAsync();

            await Task.CompletedTask;
        }

        /// <summary>
        /// </summary>
        /// <param name="cancellationToken"> </param>
        /// <returns> </returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}