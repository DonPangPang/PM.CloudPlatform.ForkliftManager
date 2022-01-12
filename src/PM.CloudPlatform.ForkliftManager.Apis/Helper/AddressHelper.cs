using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;

namespace PM.CloudPlatform.ForkliftManager.Apis.Helper
{
    [Obsolete("移除", true)]
    public static class AddressHelper
    {
        public static async Task<object> GetAddress(decimal lng, decimal lat)
        {
            var key = "99d78ca5e40fbae2f24f3142d90bd88f";
            var url = $"https://restapi.amap.com/v3/geocode/regeo?key={key}&location={lng},{lat}";

            HttpClient http = new HttpClient();

            var result = await http.GetAsync(url);

            return await result.Content.ReadFromJsonAsync<object>();
        }
    }
}