using System;

namespace PM.CloudPlatform.ForkliftManager.Apis.Enums
{
    /// <summary>
    /// 坐标
    /// </summary>
    [Obsolete("丢弃", true)]
    public class Location
    {
        /// <summary>
        /// 经度
        /// </summary>
        public double Lng { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Lat { get; set; }
    }
}