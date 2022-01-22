using System;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Entities
{
    /// <summary>
    /// 终端绑定记录
    /// </summary>
    public class TerminalBindRecord : EntityBase
    {
        /// <summary>
        /// 终端Id
        /// </summary>
        public Guid? TerminalId { get; set; }

        /// <summary>
        /// 车辆Id
        /// </summary>
        public Guid? CarId { get; set; }

        public string IMEI { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// </summary>
        public Terminal? Terminal { get; set; }

        /// <summary>
        /// </summary>
        public Car? Car { get; set; }
    }
}