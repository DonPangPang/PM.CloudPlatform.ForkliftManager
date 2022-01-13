using System;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Entities
{
    /// <summary>
    /// 终端登录记录
    /// </summary>
    public class TerminalLoginRecord : EntityBase
    {
        public Guid TerminalId { get; set; }

        public Terminal? Terminal { get; set; }
    }
}