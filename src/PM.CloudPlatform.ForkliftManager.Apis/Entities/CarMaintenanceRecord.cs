using System;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Entities
{
    /// <summary>
    /// 车辆维护记录
    /// </summary>
    public class CarMaintenanceRecord : EntityBase
    {
        /// <summary>
        /// 车辆Id
        /// </summary>
        public Guid CarId { get; set; }

        /// <summary>
        /// 车辆
        /// </summary>
        public Car? Car { get; set; }

        /// <summary>
        /// 维护时间
        /// </summary>
        public DateTime MaintenanceTime { get; set; }
    }
}