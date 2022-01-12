using Microsoft.Extensions.Options;

namespace PM.CloudPlatform.ForkliftManager.Apis.Options
{
    /// <summary>
    /// Kafka设置
    /// </summary>
    public class KafkaOption : IOptions<KafkaOption>
    {
        /// <summary>
        /// 当前配置
        /// </summary>
        public KafkaOption Value => this;

        /// <summary>
        /// EV26主题
        /// </summary>
        public string EV26Topic { get; set; }

        /// <summary>
        /// EV26Session上线主题
        /// </summary>
        public string EV26SessionOnlineTopic { get; set; }

        /// <summary>
        /// EV26Session下线主题
        /// </summary>
        public string EV26SessionOfflineTopic { get; set; }

        /// <summary>
        /// EV26消费者组
        /// </summary>
        public string EV26ConsumerGroup { get; set; }

        /// <summary>
        /// EV26Session消费者组
        /// </summary>
        public string EV26SessionConsumerGroup { get; set; }

        /// <summary>
        /// Kafka Ip和端口
        /// <remarks>Ip:Port</remarks>
        /// </summary>
        public string KafkaConfig { get; set; }
    }
}