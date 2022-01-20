#nullable enable

using System;
using System.Collections.Generic;
using AutoMapper;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    /// <summary>
    /// 电子围栏
    /// </summary>
    [AutoMap(typeof(ElectronicFence), ReverseMap = true)]
    public class ElectronicFenceAddOrUpdateDto
    {
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
        public string LngLats { get; set; } = null!;

        /// <summary>
        /// 租借公司
        /// </summary>
        public RentalCompanyDto? RentalCompany { get; set; }

        /// <summary>
        /// 围栏内的车辆
        /// </summary>
        public ICollection<CarDto>? Cars { get; set; } = new List<CarDto>();
    }
}