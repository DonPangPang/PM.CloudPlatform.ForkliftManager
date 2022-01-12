using Microsoft.Extensions.DependencyInjection;
using PM.CloudPlatform.ForkliftManager.Apis.General;
using PM.CloudPlatform.ForkliftManager.Apis.Managers;
using PM.CloudPlatform.ForkliftManager.Apis.Services;
using PM.CloudPlatform.ForkliftManager.Apis.Sessions;

namespace PM.CloudPlatform.ForkliftManager.Apis.Extensions
{
    /// <summary>
    /// 服务器扩展
    /// </summary>
    public static class ServerBuilderExtensions
    {
        /// <summary>
        /// 添加Tcp服务器
        /// </summary>
        /// <param name="services"> </param>
        /// <returns> </returns>
        public static IServiceCollection AddTcpServer(
            this IServiceCollection services)
        {
            services.AddSingleton<TerminalSessionManager>();
            services.AddHostedService<TcpSocketServerHostedService>();
            return services;
        }

        /// <summary>
        /// 添加Ws服务器
        /// </summary>
        /// <param name="services"> </param>
        /// <returns> </returns>
        public static IServiceCollection AddWsServer(
            this IServiceCollection services)
        {
            services.AddSingleton<ClientSessionManagers>();
            services.AddHostedService<WebSocketServerHostedService>();
            return services;
        }
    }
}