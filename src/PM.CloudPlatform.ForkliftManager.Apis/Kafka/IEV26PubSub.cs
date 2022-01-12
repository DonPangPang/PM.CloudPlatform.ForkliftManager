namespace PM.CloudPlatform.ForkliftManager.Apis.Kafka
{
    /// <summary>
    /// 发布与订阅接口
    /// </summary>
    public interface IEV26PubSub
    {
        /// <summary>
        /// 主题名称
        /// </summary>
        /// <value></value>
        string TopicName { get; set; }
    }
}