using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Entities
{
    /// <summary>
    /// 系统设置
    /// </summary>
    public class SystemConfig : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        public SystemConfig()
        {
            LengthOfMaintenanceTime = 500;
            BeyondFenceDistance = 100;
        }

        /// <summary>
        /// 需要维护保养的时间
        /// </summary>
        public int LengthOfMaintenanceTime { get; set; }

        /// <summary>
        /// 超出围栏的距离
        /// </summary>
        public int BeyondFenceDistance { get; set; }
    }
}