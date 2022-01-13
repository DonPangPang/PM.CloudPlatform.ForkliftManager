using System;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Models.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    /// <summary>
    /// 终端登录记录
    /// </summary>
    public class TerminalLoginRecordDto : DtoBase
    {
        /// <summary>
        /// 终端Id
        /// </summary>
        public Guid TerminalId { get; set; }

        /// <summary>
        /// 终端
        /// </summary>
        public Terminal Terminal { get; set; }
    }
}