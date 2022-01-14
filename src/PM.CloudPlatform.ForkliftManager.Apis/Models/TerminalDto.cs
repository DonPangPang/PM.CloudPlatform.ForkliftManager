using System;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Models.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    /// <summary>
    /// 终端
    /// </summary>
    public class TerminalDto : DtoBase
    {
        /// <summary>
        /// IMEI
        /// </summary>
        public string? IMEI { get; set; }

        public Guid? CarId { get; set; }

        public Car? Car { get; set; }
    }
}