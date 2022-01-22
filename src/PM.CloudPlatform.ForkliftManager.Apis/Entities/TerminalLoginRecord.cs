using System;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Entities
{
    /// <summary>
    /// 终端登录记录
    /// </summary>
    public class TerminalLoginRecord : EntityBase
    {
        /// <summary>
        /// 终端Id
        /// </summary>
        public Guid? TerminalId { get; set; }

        /// <summary>
        /// 终端
        /// </summary>
        public Terminal? Terminal { get; set; }
    }
}