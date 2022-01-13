using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.General;

namespace PM.CloudPlatform.ForkliftManager.Apis.Authorization
{
    /// <summary>
    /// 重写Permission
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly PermissionRequirement _tokenParameter;

        public PermissionHandler(IConfiguration config, IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
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
            }

            var id = Guid.Parse(context.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name))!.Value);

            var urls = await _generalRepository.GetQueryable<User>()
                .Include(x => x.Roles)
                .ThenInclude(y => y.Modules).Where(x => x.Id.Equals(id)).ToListAsync();

            context.Succeed(requirement);
            await Task.CompletedTask;
        }
    }
}