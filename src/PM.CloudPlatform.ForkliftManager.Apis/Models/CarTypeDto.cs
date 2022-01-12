using PM.CloudPlatform.ForkliftManager.Apis.Models.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    /// <summary>
    /// 车辆类型
    /// </summary>
    public class CarTypeDto : DtoBase
    {
        /// <summary>
        /// 车辆类型
        /// </summary>
        public string Type { get; set; }
    }
}