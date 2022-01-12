using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;
using System;

namespace PM.CloudPlatform.ForkliftManager.Apis.Entities
{
    /// <summary>
    /// 车辆档案
    /// </summary>
    public class Car : EntityBase
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        public string LicensePlateNumber { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 车辆型号
        /// </summary>
        public string CarModel { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public Guid CarTypeId { get; set; }

        public CarType CarType { get; set; }

        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime BuyTime { get; set; }

        /// <summary>
        /// 使用时长
        /// </summary>
        public int LengthOfUse { get; set; }

        /// <summary>
        /// 保养次数
        /// </summary>
        public int NumberOfMaintenance { get; set; }

        /// <summary>
        /// 最后一次保养时间
        /// </summary>
        public DateTime LastMaintenanceOfTime { get; set; }

        /// <summary>
        /// 最后一次保养时使用的时间
        /// </summary>
        public int LastLengthOfUse { get; set; }
    }
}