using PM.CloudPlatform.ForkliftManager.Apis.Extensions;

namespace PM.CloudPlatform.ForkliftManager.Apis.Kafka
{
    /// <summary>
    /// Kafka分区工厂
    /// <remark>
    /// 作者: Powers
    /// 描述:
    ///     对JT808生产者生产的消息进行Hash,
    /// 负载均衡到同一个主题下的不同分区.
    /// </remark>
    /// </summary>
    public class EV26MsgIdProducerPartitionFactoryImpl : IEV26ProducerPartitionFactory
    {
        /// <summary>
        /// 创建kafka分区, 根据电话号, 通过Hash算法返回分区号
        /// </summary>
        /// <param name="topicName">主题名</param>
        /// <param name="msgId">消息Id</param>
        /// <param name="terminalNo">电话号</param>
        /// <returns>分区号</returns>
        public int CreatePartition(string topicName, string msgId, string terminalNo)
        {
            var key1Byte1 = HashAlgorithmExtensions.ComputeMd5(terminalNo);
            var p = HashAlgorithmExtensions.Hash(key1Byte1, 2) % 8;
            return (int)p;
        }
    }
}