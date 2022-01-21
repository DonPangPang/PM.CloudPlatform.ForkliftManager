using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Options;
using PM.CloudPlatform.ForkliftManager.Apis.Options;

#nullable disable

namespace PM.CloudPlatform.ForkliftManager.Apis.Kafka
{
    public class EV26MsgIdProducer : IEV26Producer, IDisposable
    {
        public string TopicName { get; set; }

        private IProducer<string, byte[]> _producer;
        private IEV26ProducerPartitionFactory _ev26ProducerPartitionFactory;

        private ConcurrentDictionary<string, TopicPartition> TopicPartitionCache =
            new ConcurrentDictionary<string, TopicPartition>();

        public EV26MsgIdProducer(ProducerConfig producerConfigAccessor, IOptions<KafkaOption> options)
        {
            _producer = new ProducerBuilder<string, byte[]>(producerConfigAccessor).Build();
            _ev26ProducerPartitionFactory = new EV26MsgIdProducerPartitionFactoryImpl();

            TopicName = options.Value.EV26Topic;

            //AdminClient可以对Kafka进行管理
            //可以选择手动配置, 不使用下列代码

            using var adminClient = new AdminClientBuilder(producerConfigAccessor).Build();
            try
            {
                adminClient.CreateTopicsAsync(new TopicSpecification[] { new TopicSpecification { Name = TopicName, NumPartitions = 1, ReplicationFactor = 1 } }).Wait();
            }
            catch (AggregateException ex)
            {
                //{Confluent.Kafka.Admin.CreateTopicsException: An error occurred creating topics: [jt808]: [Topic 'jt808' already exists.].}
                if (ex.InnerException is Confluent.Kafka.Admin.CreateTopicsException exception)
                {
                }
                else
                {
                    //记录日志
                    //throw ex.InnerException;
                }
            }
            try
            {
                //topic IncreaseTo 只增不减
                adminClient.CreatePartitionsAsync(
                    new List<PartitionsSpecification>
                    {
                         new PartitionsSpecification
                         {
                             IncreaseTo = 8,
                             Topic=TopicName
                         }
                    }
                ).Wait();
            }
            catch (AggregateException ex)
            {
                //记录日志
                // throw ex.InnerException;
                // throw ex.InnerException;
            }
        }

        /// <summary>
        /// 生产消息到默认主题
        /// </summary>
        /// <param name="msgId">      消息ID </param>
        /// <param name="terminalNo"> 电话号 </param>
        /// <param name="data">       数据 </param>
        public void ProduceAsync(string msgId, string terminalNo, byte[] data)
        {
            TopicPartition topicPartition;
            if (!TopicPartitionCache.TryGetValue(terminalNo, out topicPartition))
            {
                topicPartition = new TopicPartition(TopicName, new Partition(_ev26ProducerPartitionFactory.CreatePartition(TopicName, msgId, terminalNo)));
                TopicPartitionCache.TryAdd(terminalNo, topicPartition);
            }
            _producer.ProduceAsync(topicPartition/*TopicName*/, new Message<string, byte[]>
            {
                Key = msgId,
                Value = data
            });
            //producer.Flush(TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// 生产消息到特定的主题
        /// </summary>
        /// <param name="topicName">  主题 </param>
        /// <param name="msgId">      消息ID </param>
        /// <param name="terminalNo"> 电话号 </param>
        /// <param name="data">       数据 </param>
        public void ProduceAsync(string topicName, string msgId, string terminalNo, byte[] data)
        {
            TopicPartition topicPartition;
            if (!TopicPartitionCache.TryGetValue(terminalNo, out topicPartition))
            {
                topicPartition = new TopicPartition(topicName, _ev26ProducerPartitionFactory.CreatePartition(topicName, msgId, terminalNo));
                TopicPartitionCache.TryAdd(terminalNo, topicPartition);
            }
            _producer.ProduceAsync(topicPartition/*topicName*/, new Message<string, byte[]>
            {
                Key = msgId,
                Value = data
            });
            //producer.Flush(TimeSpan.FromSeconds(10));
        }

        public void Dispose()
        {
            _producer.Dispose();
        }

        /// <summary>
        /// 擦除缓存区
        /// </summary>
        public void Flush()
        {
            _producer.Flush();
        }

        /// <summary>
        /// 定时移除缓存区
        /// </summary>
        /// <param name="timeSpan"> </param>
        public void Flush(int timeSpan)
        {
            _producer.Flush(TimeSpan.FromSeconds(timeSpan));
        }
    }
}