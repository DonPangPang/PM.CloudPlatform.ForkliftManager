using System.Threading.Tasks;

namespace PM.CloudPlatform.ForkliftManager.Apis.Kafka
{
    /// <summary>
    /// Session发布接口
    /// <remark>
    /// 作者: Powers
    /// 描述:
    ///     提供了Session发布的基本功能功能接口
    /// </remark>
    /// </summary>
    public interface ISessionPublishing
    {
        /// <summary>
        /// 根据默认主题推送消息
        /// </summary>
        /// <param name="value">消息</param>
        /// <returns></returns>
        Task PublishAsync(string value);
        /// <summary>
        /// 根据自定义主题推送消息
        /// </summary>
        /// <param name="topicName">主题</param>
        /// <param name="value">消息</param>
        /// <returns></returns>
        Task PublishAsync(string topicName, string value);
    }
}