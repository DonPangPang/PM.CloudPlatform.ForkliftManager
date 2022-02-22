using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PM.CloudPlatform.ForkliftManager.Apis.Controllers.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
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
        [HttpGet]
        public async Task<IActionResult> SetRoles(Guid userId, IEnumerable<Guid> roleIds)
        {
            var roles = await _generalRepository.GetQueryable<Role>()
                .Where(x=>roleIds.Contains(x.Id))
                .ToListAsync();
            var user = await _generalRepository.FindAsync<User>(x=>x.Id.Equals(userId));

            if(user is null)
            {
                return Fail("找不到用户");
            }

            user.Roles = roles;
            await _generalRepository.UpdateAsync(user);
            await _generalRepository.SaveAsync();

            return Success("保存成功");
        }
    }
}