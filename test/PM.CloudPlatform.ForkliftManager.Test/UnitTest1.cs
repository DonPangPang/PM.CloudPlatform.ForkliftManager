using System;
using System.IO;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using PM.CloudPlatform.ForkliftManager.Apis.Extensions;
using PM.CloudPlatform.ForkliftManager.Apis.Helper;
using Xunit;
using Xunit.Abstractions;

namespace PM.CloudPlatform.ForkliftManager.Test
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        // [Fact] public async Task Test1() { //var res = await
        // AddressHelper.GetAddress(116.481488M, 39.990464M); }

        [Fact]
        public void GeoJsonTest()
        {
            string json = "{\"type\":\"polygon\",\"coordinates\":[[[113.373481,34.211381],[113.510123,34.244877],[113.547889,34.19264],[113.373481,34.211381]]]}";

            var serializer = GeoJsonSerializer.Create();
            using (var reader = new StringReader(json))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var res = serializer.Deserialize<Geometry>(jsonReader);
            }
        }

        [Fact]
        public void DistanceTest()
        {
            var geoJson =
                "{\"type\":\"Polygon\",\"coordinates\":[[[113.437446,34.315514],[113.661979,34.31835],[113.671592,34.282047],[113.457359,34.27921],[113.437446,34.315514]]]}";

            var border = geoJson.ToGeometry<Geometry>();

            var point = new Point(new Coordinate(116.434027, 39.941037)) { SRID = 4326 };

            var distance = point.ProjectTo(2855).Distance(border.ProjectTo(2855));

            _testOutputHelper.WriteLine(distance.ToString());
        }

        [Fact]
        public void DistanceTest2()
        {
            var geoJson =
                "{\"type\":\"Polygon\",\"coordinates\":[[[113.437446,34.315514],[113.661979,34.31835],[113.671592,34.282047],[113.457359,34.27921],[113.437446,34.315514]]]}";

            var border = geoJson.ToGeometry<Geometry>();

            var point = new Point(new Coordinate(114.437446, 34.315514)) { SRID = 4326 };

            var distance = point.ProjectTo(2855).Distance(border.ProjectTo(2855));

            _testOutputHelper.WriteLine(distance.ToString());
        }

        [Fact]
        public void DistanceTest3()
        {
            var geoJson =
                "{\"type\":\"Polygon\",\"coordinates\":[[[113.437446,34.315514],[113.661979,34.31835],[113.671592,34.282047],[113.457359,34.27921],[113.437446,34.315514]]]}";

            var border = geoJson.ToGeometry<Geometry>();

            var point1 = new Point(new Coordinate(116.368904, 39.923423)) { SRID = 4326 };
            var point2 = new Point(new Coordinate(116.387271, 39.922501)) { SRID = 4326 };

            var distance = point1.ProjectTo(2855).Distance(point2.ProjectTo(2855));

            _testOutputHelper.WriteLine(distance.ToString());
        }

        [Fact]
        public void DistanceTest3_1()
        {
            var geoJson =
                "{\"type\":\"Polygon\",\"coordinates\":[[[113.437446,34.315514],[113.661979,34.31835],[113.671592,34.282047],[113.457359,34.27921],[113.437446,34.315514]]]}";

            var border = geoJson.ToGeometry<Geometry>();

            var point1 = new Point(new Coordinate(116.368904, 39.923423)).Transform_GCJ02_To_WGS84();
            var point2 = new Point(new Coordinate(116.387271, 39.922501)).Transform_GCJ02_To_WGS84();

            var distance = point1.ProjectTo(2855).Distance(point2.ProjectTo(2855));

            _testOutputHelper.WriteLine(distance.ShapeDistance().ToString());
        }

        [Fact]
        public void DistanceTest3_2()
        {
            var geoJson =
                "{\"type\":\"Polygon\",\"coordinates\":[[[113.437446,34.315514],[113.661979,34.31835],[113.671592,34.282047],[113.457359,34.27921],[113.437446,34.315514]]]}";

            var border = geoJson.ToGeometry<Geometry>();

            var point1 = new Point(new Coordinate(116.368904, 39.923423)).Transform_GCJ02_To_WGS84();
            var point2 = new Point(new Coordinate(116.387271, 39.922501)).Transform_GCJ02_To_WGS84();

            var distance = point1.Transform_WGS84_To_GCJ02().Distance(border.ProjectTo(2855));

            _testOutputHelper.WriteLine(distance.ShapeDistance().ToString());
        }

        [Fact]
        public void DistanceTest3_3()
        {
            var geoJson =
                "{\"type\":\"Polygon\",\"coordinates\":[[[113.437446,34.315514],[113.661979,34.31835],[113.671592,34.282047],[113.457359,34.27921],[113.437446,34.315514]]]}";

            Polygon border = geoJson.ToGeometry<Polygon>();

            var point1 = new Point(new Coordinate(116.368904, 39.923423)).Transform_GCJ02_To_WGS84();
            var point2 = new Point(new Coordinate(116.387271, 39.922501)).Transform_WGS84_To_GCJ02();

            _testOutputHelper.WriteLine(point2.SRID.ToString());

            var distance = point1.ProjectTo(2855).Distance(border.Tranform_GCJ02_To_WGS84().ProjectTo(2855));

            _testOutputHelper.WriteLine(distance.ShapeDistance().ToString());
        }

        [Fact]
        public void DistanceTest4()
        {
            var geoJson =
                "{\"type\":\"Polygon\",\"coordinates\":[[[113.437446,34.315514],[113.661979,34.31835],[113.671592,34.282047],[113.457359,34.27921],[113.437446,34.315514]]]}";

            var border = geoJson.ToGeometry<Geometry>();

            var point1 = new Point(117.368904, 39.923423).Transform_GCJ02_To_WGS84();
            var point2 = new Point(116.387271, 38.922501).Transform_GCJ02_To_WGS84();

            var distance = point1.ProjectTo(2855).Distance(point2.ProjectTo(2855));

            _testOutputHelper.WriteLine(distance.ShapeDistance().ToString());
        }

        [Fact]
        public void DistanceTest5()
        {
            var geoJson = "{\"type\":\"Polygon\",\"coordinates\":[[[113.529161,34.886364],[113.555254,34.881858],[113.56006,34.878478],[113.643144,34.879041],[113.700136,34.877915],[113.750948,34.870028],[113.763307,34.866085],[113.816179,34.849745],[113.825106,34.830584],[113.827165,34.789992],[113.812059,34.749945],[113.812746,34.711572],[113.749575,34.680522],[113.696016,34.669793],[113.689836,34.66358],[113.658251,34.665275],[113.636278,34.662451],[113.604692,34.66584],[113.594393,34.700282],[113.5786,34.699153],[113.567613,34.694637],[113.544954,34.694637],[113.536028,34.707621],[113.531221,34.747124],[113.529848,34.769689],[113.530535,34.81029],[113.530535,34.853689],[113.525728,34.864958],[113.529161,34.886364]]]}";
            var border = geoJson.ToGeometry_Transform_GCJ02_To_WGS84<Geometry>();

            var point = new Point(113.80383, 34.863783) { SRID = 4326 };

            var distance = point.ProjectTo(2855).Distance(border.ProjectTo(2855));

            _testOutputHelper.WriteLine(distance.ToString());
        }

        [Fact]
        public void DistanceTest6()
        {
            var gps = new Point(113.55184, 34.8266822222222222222);

            var point = gps.Transform_WGS84_To_GCJ02();

            _testOutputHelper.WriteLine(point.ToGeoJson());
        }

        [Fact]
        public void DistanceTest7()
        {
            var gps = new Point(113.55176, 34.82648);

            var point = gps.Transform_WGS84_To_GCJ02();

            _testOutputHelper.WriteLine(point.ToGeoJson());
        }

        [Fact]
        public void DistanceTest8()
        {
            var gps = new Point(113.55408888888888888888888889, 34.831737777777777777777777778);

            var point = gps.Transform_WGS84_To_GCJ02();

            _testOutputHelper.WriteLine(point.ToGeoJson());
        }

        [Fact]
        public void DistanceTest8_1()
        {
            var gps = new Point(113.55408888888888888888888889, 34.831737777777777777777777778);
            var gps1 = new Point(113.55508888888888888888888889, 34.831737777777777777777777778);

            var distance = gps.Transform_GCJ02_To_WGS84().ProjectTo(2855).Distance(gps1.Transform_GCJ02_To_WGS84().ProjectTo(2855));

            _testOutputHelper.WriteLine($"Distance is: {distance.ToString()}");
        }

        [Fact]
        public void TestName()
        {
            unchecked
            {
                // 相当于5个int相加 结果还是int 然后最后转成了long
                long res1 = 900_000_000 + 900_000_000 + 900_000_000 + 900_000_000 + 900_000_000;
                // 5个long相加
                long res2 = 900_000_000L + 900_000_000L + 900_000_000L + 900_000_000L + 900_000_000L;
                // 5个decmal相加
                decimal res3 = 900_000_000M + 900_000_000M + 900_000_000M + 900_000_000M + 900_000_000M;
                _testOutputHelper.WriteLine($"res1 = {res1}");
                _testOutputHelper.WriteLine($"res2 = {res2}");
                _testOutputHelper.WriteLine($"res3 = {res3}");
            }
        }

        [Fact]
        public void DistanceTest9()
        {
            var point = "{\"type\":\"Point\",\"coordinates\":[113.55784810930275,34.82570795145946]}".ToGeometry<Point>();
            var border = "{\"type\":\"Polygon\",\"coordinates\":[[[113.542468,34.834959],[113.556716,34.834606],[113.551502,34.827209],[113.542296,34.827631],[113.542468,34.834959]]]}".ToGeometry<Polygon>();

            var distance = point.ProjectTo(2855).Distance(border.ProjectTo(2855));

            _testOutputHelper.WriteLine($"Distance is: {distance.ToString()}");
        }
    }
}