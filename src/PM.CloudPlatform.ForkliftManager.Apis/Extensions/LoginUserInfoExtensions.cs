using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PM.CloudPlatform.ForkliftManager.Apis.Helper;

namespace PM.CloudPlatform.ForkliftManager.Apis.Extensions
{
    /// <summary>
    /// 用户登录信息扩展
    /// </summary>
    public static class LoginUserInfoExtension
    {
        /// <summary>
        /// 添加用户登录信息服务
        /// </summary>
        /// <param name="services"> </param>
        /// <returns> </returns>
        public static IServiceCollection AddLoginUserInfo(this IServiceCollection services)
        {
            services.AddSession();
            services.AddHttpContextAccessor();

            return services;
        }

        /// <summary>
        /// 使用Session存储用户登录信息
        /// </summary>
        /// <param name="app"> </param>
        /// <returns> </returns>
        public static IApplicationBuilder UseLoginUserInfo(this IApplicationBuilder app)
        {
            app.UseSession();
            var httpContextAccessor =
                app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            LoginUserInfo.Configure(httpContextAccessor);

            return app;
        }

        /// <summary>
        /// 使用Session存储用户登录信息
        /// </summary>
        /// <param name="app"> </param>
        /// <returns> </returns>
        [Obsolete("此方法已废除", true)]
        public static IApplicationBuilder UseLoginUserInfo<T>(this IApplicationBuilder app)
        {
            app.UseSession();
            var httpContextAccessor =
                app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            LoginUserInfo.Configure(httpContextAccessor);

            return app;
        }
    }
}