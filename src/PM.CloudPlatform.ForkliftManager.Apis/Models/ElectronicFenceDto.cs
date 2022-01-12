﻿using PM.CloudPlatform.ForkliftManager.Apis.Models.Base;
using System.Collections.Generic;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    /// <summary>
    /// 电子围栏
    /// </summary>
    public class ElectronicFenceDto : DtoBase
    {
        /// <summary>
        /// 围栏名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 坐标集合
        /// </summary>
        public string LngLats { get; set; }

        /// <summary>
        /// 租借公司
        /// </summary>
        public RentalCompanyDto RentalCompany { get; set; }

        /// <summary>
        /// 围栏内的车辆
        /// </summary>
        public ICollection<CarDto> Cars { get; set; } = new List<CarDto>();
    }
}