using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    /// <summary>
    /// 车辆类型
    /// </summary>
    [AutoMap(typeof(CarType), ReverseMap = true)]
    public class CarTypeAddOrUpdateDto
    {
        /// <summary>
        /// 车辆类型
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 维护保养时间
        /// </summary>
        public int LengthOfMaintenanceTime { get; set; } = 500;
    }
}