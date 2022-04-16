using System;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Extensions;
using PM.CloudPlatform.ForkliftManager.Apis.General;
using PM.CloudPlatform.ForkliftManager.Apis.Redis;

namespace PM.CloudPlatform.ForkliftManager.Apis.Authorization
{
    /// <summary>
    /// 重写Permission
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly RedisHelper _redisHelper;
        private readonly PermissionRequirement _tokenParameter;

        public PermissionHandler(
            IConfiguration config,
            IGeneralRepository generalRepository,
            IHttpContextAccessor httpContextAccessor
            //RedisHelper redisHelper
            )
        {
            _generalRepository = generalRepository;
            _httpContextAccessor = httpContextAccessor;
            //_redisHelper = redisHelper;
            _tokenParameter = config.GetSection("TokenParameter").Get<PermissionRequirement>();
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            // 校验 颁发和接收对象
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth &&
                                            c.Issuer == _tokenParameter.Issuer))
            {
                await Task.CompletedTask;
            }

            var dateOfBirth = Convert.ToDateTime(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth &&
                                                                             c.Issuer == _tokenParameter.Issuer)
                ?.Value);

            // var test =
            // TimeZone.CurrentTimeZone.ToLocalTime(Convert.ToDateTime(_tokenParameter.AccessExpiration)); 校验过期时间
            var accessExpiration = dateOfBirth.AddMinutes(_tokenParameter.AccessExpiration);
            var nowExpiration = DateTime.Now;
            if (accessExpiration < nowExpiration)
            {
                context.Fail();
                await Task.CompletedTask;
                return;
            }

            var id = Guid.Parse(context.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name))!.Value);

            //var token = await _redisHelper.GetStringAsync(id.ToString());

            //var elderToken = await _httpContextAccessor.HttpContext!.GetTokenAsync("Bearer", "access_token");

            //if (!token.Equals(elderToken))
            //{
            //    context.Fail();
            //    await Task.CompletedTask;
            //    return;
            //}

            var user = await _generalRepository.GetQueryable<User>()
                .FilterDeleted()
                .Where(x => x.Id.Equals(id))
                .Include(x => x.Roles)
                .ThenInclude(y => y.Modules)
                .FirstOrDefaultAsync();

            if (user.IsSuper)
            {
                context.Succeed(requirement);
                await Task.CompletedTask;
                return;
            }

            // var questUrl = _httpContextAccessor.HttpContext!.Request.Path.ToString();

            // if (!user.Roles!.Any(x => x.Modules!.Any(t => questUrl.Contains(t.Name))))
            // {
            //     context.Fail();
            //     await Task.CompletedTask;
            //     return;
            // }

            context.Succeed(requirement);
            await Task.CompletedTask;
        }
    }
}
