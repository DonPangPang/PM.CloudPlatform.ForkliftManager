using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pang.AutoMapperMiddleware;
using PM.CloudPlatform.ForkliftManager.Apis.Controllers.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Extensions;
using PM.CloudPlatform.ForkliftManager.Apis.General;
using PM.CloudPlatform.ForkliftManager.Apis.Helper;
using PM.CloudPlatform.ForkliftManager.Apis.Models;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.Internal.AsyncLock;

namespace PM.CloudPlatform.ForkliftManager.Apis.Controllers
{
    /// <summary>
    /// 角色
    /// </summary>
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize("Identify")]
    public class RoleController : MyControllerBase<RoleRepository, Role, RoleDto, RoleAddOrUpdateDto>
    {
        private readonly IGeneralRepository _generalRepository;
        public RoleController(RoleRepository repository, IMapper mapper, IGeneralRepository generalRepository) : base(repository, mapper)
        {
            _generalRepository = generalRepository;
        }

        /// <summary>
        /// 设置用户的角色
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="roleIds">角色Id集合</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SetRoles([FromQuery]Guid userId, [FromBody] IEnumerable<Guid> roleIds)
        {
            var roles = await _generalRepository.GetQueryable<Role>()
                .Where(x=>roleIds.Contains(x.Id))
                .ToListAsync();
            var user = await _generalRepository.FindAsync<User>(x=>x.Id.Equals(userId));

            if(user is null)
            {
                return Fail("找不到用户");
            }
            _generalRepository.Database.ExecuteSqlRaw($"delete from \"RoleUser\" where \"UsersId\" = '{user.Id}'");

            user.Roles = roles;
            await _generalRepository.UpdateAsync(user);
            await _generalRepository.SaveAsync();

            return Success("保存成功");
        }

        /// <summary>
        /// 获取用户的角色信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpGet]
        public async Task<IActionResult> GetUserRoles(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var roles = await _generalRepository.GetQueryable<User>()
                .Where(user => user.Id.Equals(userId))
                .SelectMany(t => t.Roles!)
                .ToListAsync();

            var returnDto = roles.MapTo<RoleDto>();

            return Success(returnDto);
        }

        /// <summary>
        /// 获取未删除或者禁用的角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _generalRepository.GetQueryable<Role>()
                .FilterDeleted()
                .FilterDisabled()
                .ToListAsync();

            var returnDto = roles.MapTo<RoleDto>();

            return Success(returnDto);
        }
    }
}