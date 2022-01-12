using Microsoft.Extensions.Hosting;
using PM.CloudPlatform.ForkliftManager.Apis.Managers;
using PM.CloudPlatform.ForkliftManager.Apis.Sessions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PM.CloudPlatform.ForkliftManager.Apis.Services
{
    /// <summary>
    /// 消费消息
    /// </summary>
    public class A7670MessageConsumerService : BackgroundService
    {
        private readonly ClientSessionManagers _clientSessionManager;

        /// <summary>
        /// </summary>
        public A7670MessageConsumerService(ClientSessionManagers clientSessionManager)
        {
            _clientSessionManager =
                clientSessionManager ?? throw new ArgumentNullException(nameof(clientSessionManager));
        }

        /// <summary>
        /// </summary>
        /// <param name="stoppingToken"> </param>
        /// <returns> </returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.CompletedTask;
        }
    }
}