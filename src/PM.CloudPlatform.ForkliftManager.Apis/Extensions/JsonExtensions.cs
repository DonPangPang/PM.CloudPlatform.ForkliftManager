using System.IO;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;

#nullable disable

namespace PM.CloudPlatform.ForkliftManager.Apis.Extensions
{
    /// <summary>
    /// Json扩展
    /// </summary>
    public static class JsonExtensions
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T ToObject<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string ToGeoJson(this Geometry obj)
        {
            var serializer = GeoJsonSerializer.Create();
            using var writer = new StringWriter();
            using var jsonWriter = new JsonTextWriter(writer);

            serializer.Serialize(jsonWriter, obj);
            return jsonWriter.ToString();
        }

        public static T ToGeometry<T>(this string json) where T : Geometry
        {
            var serializer = GeoJsonSerializer.Create();
            using var reader = new StringReader(json);
            using var jsonReader = new JsonTextReader(reader);
            T res = serializer.Deserialize<T>(jsonReader);

            return res;
        }
    }
}