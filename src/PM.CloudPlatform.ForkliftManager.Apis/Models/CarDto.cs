using PM.CloudPlatform.ForkliftManager.Apis.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Enums;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    /// <summary>
    /// 车辆档案
    /// </summary>
    public class CarDto : DtoBase
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        public string? LicensePlateNumber { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string? Brand { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string? SerialNumber { get; set; }

        /// <summary>
        /// 车辆型号
        /// </summary>
        public string? CarModel { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public Guid CarTypeId { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        [JsonIgnore]
        public CarTypeDto? CarType { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public string? CarTypeName => CarType is null ? "" : CarType.Name;

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
        public int LastLengthOfUse { get; set; }

        /// <summary>
        /// 电子围栏的Id
        /// </summary>
        public Guid? ElectronicFence { get; set; }

        public ICollection<ElectronicFence>? ElectronicFences { get; set; }

        /// <summary>
        /// 租赁公司Id
        /// </summary>
        public Guid? RentalCompanyId { get; set; }

        /// <summary>
        /// 租赁公司
        /// </summary>

        public RentalCompany? RentalCompany { get; set; }

        /// <summary>
        /// 使用记录
        /// </summary>
        public ICollection<UseRecord>? UseRecords { get; set; }

        /// <summary>
        /// 租借记录
        /// </summary>
        public ICollection<RentalRecord>? RentalRecords { get; set; }

        /// <summary>
        /// 车辆维护记录
        /// </summary>
        public ICollection<CarMaintenanceRecord>? CarMaintenanceRecords { get; set; }

        /// <summary>
        /// 车辆状态
        /// </summary>
        public CarStates CarStates =>
            RentalRecords is not null ? RentalRecords!.FirstOrDefault(x => !x.IsReturn) is null ? CarStates.空闲 : CarStates.租赁: CarStates.空闲;
    }
}