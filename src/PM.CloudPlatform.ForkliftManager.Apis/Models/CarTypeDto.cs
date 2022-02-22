using AutoMapper;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Models.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    /// <summary>
    /// 车辆类型
    /// </summary>
    [AutoMap(typeof(CarType), ReverseMap = true)]
    public class CarTypeDto : DtoBase
    {
        /// <summary>
        /// 维护保养时间
        /// </summary>
        public int LengthOfMaintenanceTime { get; set; } = 500;
    }
}