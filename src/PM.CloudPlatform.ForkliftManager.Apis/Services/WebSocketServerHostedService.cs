using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

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
                        if(string.IsNullOrEmpty(s["VerifyCode"].ToString()))
                        {
                            await s.CloseAsync(CloseReason.ProtocolError, "无法获取授权码");
                        }

                        if (package.VerifyCode!.Equals(s["VerifyCode"].ToString()))
                        {
                            var packet = new ClientPackage()
                            {
                                PackageType = PackageType.Heart,
                                ClientId = "",
                            };
                            var msg = packet.ToJson();

                            await s.SendAsync(msg);
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