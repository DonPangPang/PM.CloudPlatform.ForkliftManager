using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace PM.CloudPlatform.ForkliftManager.Apis.Options
{
    /// <summary>
    /// 服务器配置
    /// </summary>
    public class ServerOption : IOptions<ServerOption>
    {
        /// <summary>
        /// </summary>
        public ServerOption Value => this;

        /// <summary>
        /// </summary>
        public List<Listener> TcpListeners { get; set; } = null!;

        /// <summary>
        /// </summary>
        public List<Listener> WsListeners { get; set; } = null!;
    }

    /// <summary>
    /// 服务器属性
    /// </summary>
    public class Listener
    {
        /// <summary>
        /// IP
        /// </summary>
        public string Ip { get; set; } = "Any";

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = default!;
    }
}