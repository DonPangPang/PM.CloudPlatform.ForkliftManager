using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PM.CloudPlatform.ForkliftManager.Apis.Controllers.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.General;
using PM.CloudPlatform.ForkliftManager.Apis.Models;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace PM.CloudPlatform.ForkliftManager.Apis.Controllers
{
    /// <summary>
    /// 模块
    /// </summary>
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize("Identify")]
    public class ModuleController : MyControllerBase<ModuleRepository, Module, ModuleDto, ModuleAddOrUpdateDto>
    {
        private readonly IGeneralRepository _generalRepository;
        public ModuleController(ModuleRepository repository, IMapper mapper, IGeneralRepository generalRepository) : base(repository, mapper)
        {
            _generalRepository = generalRepository;
        }

        /// <summary>
        /// 设置角色模块
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moduleIds"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SetModules(Guid roleId, IEnumerable<Guid> moduleIds)
        {
            var role = await _generalRepository.FindAsync<Role>(roleId);

            if(role is null)
            {
                return Fail("找不到角色");
            }

            var modules = await _generalRepository.GetQueryable<Module>()
                .Where(x=>moduleIds.Contains(x.Id))
                .ToListAsync();

            role.Modules = modules;
            await _generalRepository.UpdateAsync(role);
            await _generalRepository.SaveAsync();

            return Success("保存成功");
        }
    }
}