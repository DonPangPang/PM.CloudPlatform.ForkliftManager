using System;
using System.Threading;

namespace PM.CloudPlatform.ForkliftManager.Apis.Kafka
{
    /// <summary>
    /// 消费者接口
    /// </summary>
    public interface IEV26Consumer : IEV26PubSub, IDisposable
    {
        /// <summary>
        /// 监听接收消息
        /// </summary>
        /// <param name="callback"></param>
        void OnMessage(Action<(string MsgId, byte[] data)> callback);
        /// <summary>
        /// Task Token
        /// </summary>
        CancellationTokenSource Cts { get; }
        /// <summary>
        /// 订阅
        /// </summary>
        void Subscribe();
        /// <summary>
        /// 取消订阅
        /// </summary>
        void Unsubscribe();
    }
}