using Newtonsoft.Json;
using PM.CloudPlatform.ForkliftManager.Apis.Models.Base;
using System.Collections.Generic;
using NetTopologySuite.Geometries;
using PM.CloudPlatform.ForkliftManager.Apis.Extensions;

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
        public string? Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 坐标集合
        /// </summary>
        public string? LngLats { get; set; }

        [JsonIgnore]
        public Polygon? Border => LngLats.ToGeometry<Polygon>();

        /// <summary>
        /// 租借公司
        /// </summary>
        [JsonIgnore]
        public RentalCompanyDto? RentalCompany { get; set; }

        /// <summary>
        /// 围栏内的车辆
        /// </summary>
        [JsonIgnore]
        public ICollection<CarDto>? Cars { get; set; } = new List<CarDto>();
    }
}