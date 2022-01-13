using System.Collections.Generic;
using AutoMapper;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    /// <summary>
    /// 租借单位
    /// </summary>
    [AutoMap(typeof(RentalCompany), ReverseMap = true)]
    public class RentalCompanyAddOrUpdateDto
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 单位地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 租借的车辆
        /// </summary>
        public ICollection<CarDto>? Cars { get; set; }

        /// <summary>
        /// 电子围栏
        /// </summary>
        public ICollection<ElectronicFenceDto>? ElectronicFences { get; set; }
    }
}