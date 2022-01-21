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
            string json = "{\"type\":\"polygon\",\"coordinates\":[[[113.373481,34.211381],[113.510123,34.244877],[113.547889,34.19264],[113.373481,34.211381]]]}";

            var serializer = GeoJsonSerializer.Create();
            using (var reader = new StringReader(json))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var res = serializer.Deserialize<Geometry>(jsonReader);
            }
        }
    }
}