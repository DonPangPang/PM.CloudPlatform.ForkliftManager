using Microsoft.AspNetCore.Http;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Extensions;

namespace PM.CloudPlatform.ForkliftManager.Apis.Helper
{
    /// <summary>
    /// 登录信息(默认)
    /// </summary>
    public static class LoginUserInfo
    {
        private static IHttpContextAccessor _httpContextAccessor = null!;

        /// <summary>
        /// </summary>
        /// <param name="httpContextAccessor"> </param>
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 获取身份信息
        /// </summary>
        /// <returns> </returns>
        public static T? Get<T>()
        {
            var res = _httpContextAccessor.HttpContext!.Session.GetString("UserInfo");
            return string.IsNullOrEmpty(res) ? default(T) : res.ToObject<T>();
        }

        /// <summary>
        /// 设置身份信息
        /// </summary>
        /// <param name="userInfo"> </param>
        public static void Set(User userInfo)
        {
            _httpContextAccessor.HttpContext!.Session.SetString("UserInfo", userInfo.ToJson());
        }

        /// <summary>
        /// 设置身份信息
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="userInfo"> </param>
        public static void Set<T>(T userInfo)
        {
            _httpContextAccessor.HttpContext!.Session.SetString("UserInfo", userInfo.ToJson());
        }
    }
}