using PM.CloudPlatform.ForkliftManager.Apis.Models.Base;
using System.Collections.Generic;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    /// <summary>
    /// 租借单位
    /// </summary>
    public class RentalCompanyDto : DtoBase
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 单位地址
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string? Contact { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string? Tel { get; set; }

        /// <summary>
        /// 租借的车辆
        /// </summary>
        public ICollection<CarDto>? Cars { get; set; } = new List<CarDto>();

        /// <summary>
        /// 电子围栏
        /// </summary>
        public ICollection<ElectronicFenceDto>? ElectronicFences { get; set; } = new List<ElectronicFenceDto>();
    }
}