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
                            await s.CloseAsync(CloseReason.ProtocolError, "无法验证授权码");
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

                            lon += (rand.NextDouble() / 100000000);
                            lat += (rand.NextDouble() / 100000000);
                            var packet = new ClientPackage()
                            {
                                PackageType = PackageType.Gps,
                                Data = new
                                {
                                    // 终端Id
                                    TerminalId = "868120278343188",
                                    // 纬度
                                    Lon = 34.826682222222222222222222222,
                                    // 经度
                                    Lat = 113.55184,
                                    // 高德经纬度对象
                                    GdPoint = new Point(lon, lat),
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