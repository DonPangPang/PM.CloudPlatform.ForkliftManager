using NetTopologySuite.Geometries;
using PM.CloudPlatform.ForkliftManager.Apis.Helper;

namespace PM.CloudPlatform.ForkliftManager.Apis.Extensions
{
    public static class GpsTransformExtensions
    {
        public static Point Transform_GCJ02_To_WGS84(this Point point)
        {
            var res = GpsHelper.GCJ02_to_WGS84(point.X, point.Y);

            return new Point(res[0], res[1]) { SRID = 4326 };
        }

        public static Point Transform_WGS84_To_GCJ02(this Point point)
        {
            var res = GpsHelper.WGS84_to_GCJ02(point.X, point.Y);

            return new Point(res[0], res[1]);
        }

        public static Coordinate Transform_GCJ02_To_WGS84(this Coordinate point)
        {
            var res = GpsHelper.GCJ02_to_WGS84(point.X, point.Y);

            return new Coordinate(res[0], res[1]);
        }

        public static Coordinate Transform_WGS84_To_GCJ02(this Coordinate point)
        {
            var res = GpsHelper.WGS84_to_GCJ02(point.X, point.Y);

            return new Coordinate(res[0], res[1]);
        }

        public static double ShapeDistance(this double distance)
        {
            return distance * 0.99;
        }
    }
}