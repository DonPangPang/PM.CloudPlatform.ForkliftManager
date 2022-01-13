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
        public Guid TerminalId { get; set; }

        public Terminal Terminal { get; set; }
    }
}