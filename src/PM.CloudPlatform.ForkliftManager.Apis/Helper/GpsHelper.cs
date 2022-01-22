using System;

namespace PM.CloudPlatform.ForkliftManager.Apis.Helper
{
    public static class GpsHelper
    {
        /****************************************************/

        #region 坐标转换

        private const double pi = 3.14159265358979324;
        private const double x_pi = 3.14159265358979324 * 3000.0 / 180.0;

        //克拉索天斯基椭球体参数值
        private const double a = 6378245.0;

        //第一偏心率
        private const double ee = 0.00669342162296594323;

        //中国大陆，高德和谷歌用的都是GCJ-02

        /// <summary>
        /// GCJ-02转换BD-09 高德转百度
        /// </summary>
        /// <param name="gd_lon"> </param>
        /// <param name="gd_lat"> </param>
        /// <returns> </returns>
        public static double[] GCJ02_to_BD09(double gd_lon, double gd_lat)
        {
            double[] BD09_lon_lat = new double[2];
            double x = gd_lon, y = gd_lat;
            double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * x_pi);
            double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * x_pi);
            double bd_lon = z * Math.Cos(theta) + 0.0065;
            double bd_lat = z * Math.Sin(theta) + 0.006;
            BD09_lon_lat[0] = bd_lon;
            BD09_lon_lat[1] = bd_lat;
            return BD09_lon_lat;
        }

        /// <summary>
        /// BD-09转换GCJ-02 百度转高德
        /// </summary>
        /// <param name="bd_lon"> </param>
        /// <param name="bd_lat"> </param>
        /// <returns> </returns>
        public static double[] BD09_to_GCJ02(double bd_lon, double bd_lat)
        {
            double[] GCJ02_lon_lat = new double[2];
            double x = bd_lon - 0.0065, y = bd_lat - 0.006;
            double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * x_pi);
            double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * x_pi);
            double gd_lon = z * Math.Cos(theta);
            double gd_lat = z * Math.Sin(theta);
            GCJ02_lon_lat[0] = gd_lon;
            GCJ02_lon_lat[1] = gd_lat;
            return GCJ02_lon_lat;
        }

        /// <summary>
        /// WGS-84转换GCJ-02 GPS转高德
        /// </summary>
        /// <param name="wg_lon"> </param>
        /// <param name="wg_lat"> </param>
        /// <returns> </returns>
        public static double[] WGS84_to_GCJ02(double wg_lon, double wg_lat)
        {
            double[] GCJ02_lon_lat = new double[2];
            if (OutOfChina(wg_lon, wg_lat))
            {
                GCJ02_lon_lat[0] = wg_lon;
                GCJ02_lon_lat[1] = wg_lat;
                return GCJ02_lon_lat;
            }
            double dLon = TransformLon(wg_lon - 105.0, wg_lat - 35.0);
            double dLat = TransformLat(wg_lon - 105.0, wg_lat - 35.0);
            double radLat = wg_lat / 180.0 * pi;
            double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt(magic);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            double lng = wg_lon + dLon;
            double lat = wg_lat + dLat;
            GCJ02_lon_lat[0] = lng;
            GCJ02_lon_lat[1] = lat;
            return GCJ02_lon_lat;
        }

        /// <summary>
        /// GCJ-02转换WGS-84 高德转GPS
        /// </summary>
        /// <param name="gd_lon"> </param>
        /// <param name="gd_lat"> </param>
        /// <returns> </returns>
        public static double[] GCJ02_to_WGS84(double gd_lon, double gd_lat)
        {
            double[] WGS84_lon_lat = new double[2];

            double dLon = TransformLon(gd_lon - 105.0, gd_lat - 35.0);
            double dLat = TransformLat(gd_lon - 105.0, gd_lat - 35.0);
            double radLat = gd_lat / 180.0 * pi;
            double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt(magic);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            double lng = gd_lon - dLon;
            double lat = gd_lat - dLat;
            WGS84_lon_lat[0] = lng;
            WGS84_lon_lat[1] = lat;
            return WGS84_lon_lat;
        }

        public static void Transform(double wg_lon, double wg_lat, double[] lng_lat)
        {
            if (OutOfChina(wg_lon, wg_lat))
            {
                lng_lat[0] = wg_lon;
                lng_lat[1] = wg_lat;
                return;
            }
            double dLon = TransformLon(wg_lon - 105.0, wg_lat - 35.0);
            double dLat = TransformLat(wg_lon - 105.0, wg_lat - 35.0);

            double radLat = wg_lat / 180.0 * pi;
            double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt(magic);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);

            lng_lat[0] = wg_lon + dLon;
            lng_lat[1] = wg_lat + dLat;
        }

        /// <summary>
        /// 判断是否在中国区域外
        /// </summary>
        /// <param name="lon"> </param>
        /// <param name="lat"> </param>
        /// <returns> </returns>
        private static bool OutOfChina(double lon, double lat)
        {
            if (lon < 72.004 || lon > 137.8347)
                return true;
            if (lat < 0.8293 || lat > 55.8271)
                return true;
            return false;
        }

        /// <summary>
        /// 经度加密公式
        /// </summary>
        /// <param name="x"> </param>
        /// <param name="y"> </param>
        /// <returns> </returns>
        private static double TransformLon(double x, double y)
        {
            double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0 * pi)) * 2.0 / 3.0;
            return ret;
        }

        /// <summary>
        /// 纬度加密公式
        /// </summary>
        /// <param name="x"> </param>
        /// <param name="y"> </param>
        /// <returns> </returns>
        private static double TransformLat(double x, double y)
        {
            double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(y / 12.0 * pi) + 320 * Math.Sin(y * pi / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        #endregion 坐标转换

        /****************************************************/
    }
}