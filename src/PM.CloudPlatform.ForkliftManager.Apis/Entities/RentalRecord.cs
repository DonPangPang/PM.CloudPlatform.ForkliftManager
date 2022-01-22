using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;
using System;
using System.Collections.Generic;

namespace PM.CloudPlatform.ForkliftManager.Apis.Entities
{
    /// <summary>
    /// 租借记录
    /// </summary>
    public class RentalRecord : EntityBase
    {
        /// <summary>
        /// 租借公司ID
        /// </summary>
        public Guid? RentalCompanyId { get; set; }

        /// <summary>
        /// 租借处理人姓名
        /// </summary>
        public string RentalEmployeeName { get; set; } = null!;

        /// <summary>
        /// 租借处理人电话
        /// </summary>
        public string? RentalEmployeeTel { get; set; } = null!;

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
        public string? ReturnAckEmployeeTel { get; set; } = null!;

        /// <summary>
        /// Car Id
        /// </summary>
        public Guid CarId { get; set; }

        /// <summary>
        /// 租借的车辆
        /// </summary>
        public Car? Car { get; set; }

        /// <summary>
        /// 租借的公司
        /// </summary>
        public RentalCompany? RentalCompany { get; set; }

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