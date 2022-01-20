using System;
using System.Collections.Generic;
using AutoMapper;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    /// <summary>
    /// 车辆档案
    /// </summary>
    [AutoMap(typeof(Car), ReverseMap = true)]
    public class CarAddOrUpdateDto
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        public string LicensePlateNumber { get; set; } = null!;

        /// <summary>
        /// 品牌
        /// </summary>
        public string? Brand { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string SerialNumber { get; set; } = null!;

        /// <summary>
        /// 车辆型号
        /// </summary>
        public string CarModel { get; set; } = null!;

        /// <summary>
        /// 车辆类型
        /// </summary>
        public Guid CarTypeId { get; set; }

        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime? BuyTime { get; set; }

        /// <summary>
        /// 使用时长
        /// </summary>
        public int? LengthOfUse { get; set; }

        /// <summary>
        /// 保养次数
        /// </summary>
        public int? NumberOfMaintenance { get; set; }

        /// <summary>
        /// 最后一次保养时间
        /// </summary>
        public DateTime? LastMaintenanceOfTime { get; set; }

        /// <summary>
        /// 最后一次保养时使用的时间
        /// </summary>
        public int? LastLengthOfUse { get; set; }
    }
}