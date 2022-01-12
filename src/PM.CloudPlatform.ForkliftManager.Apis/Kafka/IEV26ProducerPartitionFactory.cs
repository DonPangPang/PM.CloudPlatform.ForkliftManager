namespace PM.CloudPlatform.ForkliftManager.Apis.Kafka
{
    /// <summary>
    /// EV26分区工厂接口
    /// <remark>
    /// 作者: Powers
    /// 描述:
    ///     提供了JT808负载均衡分区的功能接口
    /// </remark>
    /// </summary>
    public interface IEV26ProducerPartitionFactory
    {
        /// <summary>
        /// EV26生产者分区工厂
        /// 分区策略:
        /// 1. 可以根据设备终端号进行分区
        /// 2. 可以根据MsgId(消息Id)+ 谁被终端号进行分区
        /// </summary>
        int CreatePartition(string topicName, string msgId, string terminalNo);
    }
}