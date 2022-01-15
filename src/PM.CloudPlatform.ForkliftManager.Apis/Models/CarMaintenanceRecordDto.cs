using System;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Models.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    public class CarMaintenanceRecordDto : DtoBase
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
        /// 维护人
        /// </summary>
        public string? Maintainer { get; set; }

        /// <summary>
        /// 维护人联系方式
        /// </summary>
        public string? MaintainerTel { get; set; }

        /// <summary>
        /// 维护内容
        /// </summary>
        public string? MaintainerContent { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remarks { get; set; }

        /// <summary>
        /// 维护时间
        /// </summary>
        public DateTime MaintenanceTime { get; set; }

        /// <summary>
        /// 维护时长
        /// </summary>
        public int MaintenanceDateLength { get; set; }
    }
}