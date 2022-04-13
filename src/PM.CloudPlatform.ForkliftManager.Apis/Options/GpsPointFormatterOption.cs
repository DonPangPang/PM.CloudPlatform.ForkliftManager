using Microsoft.Extensions.Options;

namespace PM.CloudPlatform.ForkliftManager.Apis.Options
{
    /// <summary>
    /// Gps点位纠正
    /// </summary>
    public class GpsPointFormatterOption : IOptions<GpsPointFormatterOption>
    {
        /// <summary>
        /// 
        /// </summary>
        public GpsPointFormatterOption Value => this;

        /// <summary>
        /// 速度
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// 纠正系数
        /// </summary>
        public double Coefficient { get; set; }

        /// <summary>
        /// 检查上个点的时间间隔
        /// </summary>
        public int TimeInterval { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class GpsPointFormatterOptionExtensions
    {
        /// <summary>
        /// km/s -> m/s
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="coefficient"></param>
        /// <returns></returns>
        public static double To_m_s(this double speed, double coefficient = 1)
        {
            return speed / 3.6d * coefficient;
        }
    }
}
