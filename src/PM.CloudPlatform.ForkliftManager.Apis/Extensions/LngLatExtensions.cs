using Newtonsoft.Json;
using PM.CloudPlatform.ForkliftManager.Apis.Enums;
using System;
using System.Collections.Generic;

namespace PM.CloudPlatform.ForkliftManager.Apis.Extensions
{
    /// <summary>
    /// 坐标扩展
    /// </summary>
    [Obsolete("丢弃", true)]
    public static class LngLatExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="locations"> </param>
        /// <returns> </returns>
        public static string LngLatListToJsonString(this List<Location> locations)
        {
            return JsonConvert.SerializeObject(locations);
        }

        /// <summary>
        /// </summary>
        /// <param name="locations"> </param>
        /// <returns> </returns>
        public static string LngLatListToJsonString(this IEnumerable<Location> locations)
        {
            return JsonConvert.SerializeObject(locations);
        }

        /// <summary>
        /// </summary>
        /// <param name="lnglats"> </param>
        /// <returns> </returns>
        public static List<Location>? JsonToLngLatList(this string lnglats)
        {
            return JsonConvert.DeserializeObject<List<Location>>(lnglats);
        }

        /// <summary>
        /// </summary>
        /// <param name="lnglats"> </param>
        /// <returns> </returns>
        public static IEnumerable<Location>? JsonToLngLatListAsEnumerable(this string lnglats)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Location>>(lnglats);
        }
    }
}