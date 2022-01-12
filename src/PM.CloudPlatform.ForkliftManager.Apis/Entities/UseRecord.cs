using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;
using System;

namespace PM.CloudPlatform.ForkliftManager.Apis.Entities
{
    /// <summary>
    /// 使用记录
    /// </summary>
    public class UseRecord : EntityBase
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        public Guid CarId { get; set; }

        /// <summary>
        /// 开始使用时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束使用时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 使用时长
        /// </summary>
        public int LengthOfTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Car Car { get; set; }
    }
}