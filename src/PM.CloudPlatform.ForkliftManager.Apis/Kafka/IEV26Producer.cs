namespace PM.CloudPlatform.ForkliftManager.Apis.Kafka
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEV26Producer
    {
        /// <summary>
        /// 生产消息到默认主题
        /// </summary>
        /// <param name="msgId">消息Id</param>
        /// <param name="terminalNo">设备终端号</param>
        /// <param name="data">data</param>
        void ProduceAsync(string msgId, string terminalNo, byte[] data);

        /// <summary>
        /// 生产消息到指定主题
        /// </summary>
        /// <param name="topicName">主题名</param>
        /// <param name="msgId">消息Id</param>
        /// <param name="terminalNo">设备终端号</param>
        /// <param name="data">data</param>
        void ProduceAsync(string topicName, string msgId, string terminalNo, byte[] data);
    }
}