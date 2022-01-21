using PM.CloudPlatform.ForkliftManager.Apis.Models.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    /// <summary>
    /// 系统设置
    /// </summary>
    public class SystemConfigDto : DtoBase
    {
        /// <summary>
        /// 需要维护保养的时间
        /// </summary>
        public int LengthOfMaintenanceTime { get; set; } = 500;

        /// <summary>
        /// 超出围栏的距离
        /// </summary>
        public int BeyondFenceDistance { get; set; } = 100;
    }
}