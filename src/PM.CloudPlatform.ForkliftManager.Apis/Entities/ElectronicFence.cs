using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PM.CloudPlatform.ForkliftManager.Apis.Entities
{
    /// <summary>
    /// 电子围栏
    /// </summary>
    public class ElectronicFence : EntityBase
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid RentalCompanyId { get; set; }

        /// <summary>
        /// 围栏名称
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 坐标集合
        /// </summary>
        public string? LngLats { get; set; }

        /// <summary>
        /// 租借公司
        /// </summary>
        [JsonIgnore]
        public RentalCompany? RentalCompany { get; set; }

        /// <summary>
        /// 围栏内的车辆
        /// </summary>
        [JsonIgnore]
        public ICollection<Car>? Cars { get; set; }
    }
}