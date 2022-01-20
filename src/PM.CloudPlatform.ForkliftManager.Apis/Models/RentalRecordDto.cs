using PM.CloudPlatform.ForkliftManager.Apis.Models.Base;
using System;
using System.Collections.Generic;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    /// <summary>
    /// </summary>
    public class RentalRecordDto : DtoBase
    {
        /// <summary>
        /// 租借公司ID
        /// </summary>
        public RentalCompanyDto? RentalCompany { get; set; }

        /// <summary>
        /// 租借处理人姓名
        /// </summary>
        public string? RentalEmployeeName { get; set; }

        /// <summary>
        /// 租借处理人电话
        /// </summary>
        public string? RentalEmployeeTel { get; set; }

        /// <summary>
        /// 租借开始时间
        /// </summary>
        public DateTime? RentalStartTime { get; set; }

        /// <summary>
        /// 租借结束时间
        /// </summary>
        public DateTime? RentalEndTime { get; set; }

        /// <summary>
        /// 是否归还
        /// </summary>
        public bool IsReturn { get; set; } = false;

        /// <summary>
        /// 归还时间
        /// </summary>
        public DateTime? ReturnTime { get; set; }

        /// <summary>
        /// 归还确认人姓名
        /// </summary>
        public string? ReturnAckEmployeeName { get; set; }

        /// <summary>
        /// 归还确认人电话
        /// </summary>
        public string? ReturnAckEmployeeTel { get; set; }

        /// <summary>
        /// </summary>
        public Guid CarId { get; set; }

        /// <summary>
        /// 租借的车辆
        /// </summary>
        public CarDto? Car { get; set; }

        /// <summary>
        /// 电子围栏Id
        /// </summary>
        public Guid? ElectronicFenceId { get; set; }

        /// <summary>
        /// 电子围栏
        /// </summary>
        public ElectronicFence? ElectronicFence { get; set; }
    }
}