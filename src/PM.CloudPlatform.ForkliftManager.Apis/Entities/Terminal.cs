using System;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Entities
{
    /// <summary>
    /// 终端
    /// </summary>
    public class Terminal : EntityBase
    {
        /// <summary>
        /// IMEI
        /// </summary>
        public string IMEI { get; set; }

        public Guid CarId { get; set; }

        public Car Car { get; set; }
    }
}