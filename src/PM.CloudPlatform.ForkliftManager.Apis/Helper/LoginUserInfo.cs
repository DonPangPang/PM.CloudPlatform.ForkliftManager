using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Extensions;
using PM.CloudPlatform.ForkliftManager.Apis.General;

namespace PM.CloudPlatform.ForkliftManager.Apis.Helper
{
    /// <summary>
    /// 登录信息(默认)
    /// </summary>
    public static class LoginUserInfo
    {
        private static IHttpContextAccessor _httpContextAccessor = null!;
        private static IGeneralRepository _generalRepository = null!;

        /// <summary>
        /// </summary>
        /// <param name="httpContextAccessor"> </param>
        /// <param name="serviceScopeFactory"> </param>
        public static void Configure(IHttpContextAccessor httpContextAccessor, IServiceScopeFactory serviceScopeFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _generalRepository = serviceScopeFactory.CreateScope().ServiceProvider.GetService<IGeneralRepository>()!;
        }

        /// <summary>
        /// 获取身份信息
        /// </summary>
        /// <returns> </returns>
        public static T? Get<T>() where T : EntityBase
        {
            //var res = _httpContextAccessor.HttpContext!.Session.GetString("UserInfo");
            try
            {
                if (_httpContextAccessor.HttpContext is null || _httpContextAccessor.HttpContext.User is null)
                {
                    return null;
                }

                var id = Guid.Parse(_httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name))!.Value);
                var res = _generalRepository.GetQueryable<T>().AsNoTracking().FirstOrDefault(x => x.Id.Equals(id));

                return res;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 设置身份信息
        /// </summary>
        /// <param name="userInfo"> </param>
        [Obsolete("移除", true)]
        public static void Set(User userInfo)
        {
            _httpContextAccessor.HttpContext!.Session.SetString("UserInfo", userInfo.ToJson());
        }

        /// <summary>
        /// 设置身份信息
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="userInfo"> </param>
        [Obsolete("移除", true)]
        public static void Set<T>(T userInfo)
        {
            _httpContextAccessor.HttpContext!.Session.SetString("UserInfo", userInfo.ToJson());
        }
    }
}