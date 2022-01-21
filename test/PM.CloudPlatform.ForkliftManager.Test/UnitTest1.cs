using System;
using System.IO;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using PM.CloudPlatform.ForkliftManager.Apis.Extensions;
using PM.CloudPlatform.ForkliftManager.Apis.Helper;
using Xunit;

namespace PM.CloudPlatform.ForkliftManager.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            //var res = await AddressHelper.GetAddress(116.481488M, 39.990464M);
        }

        [Fact]
        public void GeoJsonTest()
        {
            string json = "{\"type\":\"Point\",\"coordinates\":[0.0,0.0]}";

            var serializer = GeoJsonSerializer.Create();
            using (var reader = new StringReader(json))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var res = serializer.Deserialize<Geometry>(jsonReader);
            }
        }
    }
}