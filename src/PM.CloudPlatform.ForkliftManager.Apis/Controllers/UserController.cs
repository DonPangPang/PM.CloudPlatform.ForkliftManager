using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Pang.AutoMapperMiddleware;
using PM.CloudPlatform.ForkliftManager.Apis.Controllers.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Extensions;
using PM.CloudPlatform.ForkliftManager.Apis.General;
using PM.CloudPlatform.ForkliftManager.Apis.Helper;
using PM.CloudPlatform.ForkliftManager.Apis.Models;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize("Identify")]
    public class UserController : MyControllerBase<UserRepository, User, UserDto, UserAddOrUpdateDto>
    {
        private readonly IGeneralRepository _generalRepository;

        private readonly DbSet<User> Users;

        /// <summary>
        /// </summary>
        /// <param name="repository">        </param>
        /// <param name="mapper">            </param>
        /// <param name="generalRepository"> </param>
        public UserController(UserRepository repository, IMapper mapper, IGeneralRepository generalRepository) : base(
            repository, mapper)
        {
            _generalRepository = generalRepository;
            Users = _generalRepository.GetDbSet<User>();
        }

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <returns> </returns>

        [HttpGet]
        public IActionResult GetLoginUserInfo()
        {
            var userInfo = LoginUserInfo.Get<User>();

            if (userInfo is not null)
            {
                return Success(userInfo);
            }
            else
            {
                return Fail("用户登录信息丢失");
            }
        }

        /// <summary>
        /// 获取登录用户的角色
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> GetLoginUserRoles()
        {
            var userInfo = LoginUserInfo.Get<User>()?.MapTo<UserDto>();

            if (userInfo is not null)
            {
                var roles = await Users.Where(user => user.Id.Equals(userInfo.Id)).SelectMany(t => t.Roles!)
                    .ToListAsync();

                var returnDto = roles.MapTo<RoleDto>();

                return Success(returnDto);
            }
            else
            {
                return Fail("用户登录信息丢失");
            }
        }

        /// <summary>
        /// 获取登录用户的模块
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> GetLoginUserModules()
        {
            var userInfo = LoginUserInfo.Get<User>()?.MapTo<UserDto>();

            if (userInfo is not null)
            {
                var modules = await Users
                    .FilterDeleted()
                    .Where(user => user.Id.Equals(userInfo.Id))
                    .SelectMany(t => t.Roles!)
                    .SelectMany(t => t.Modules!).ToListAsync();
                var returnDto = modules.MapTo<ModuleDto>();

                //var res = await Users.SelectMany(u => u.Roles!.SelectMany(r => r.Modules!.Select(m => new
                //    {
                //        u,
                //        r,
                //        m
                //    }))).ToListAsync();

                return Success(returnDto);
            }
            else
            {
                return Fail("用户登录信息丢失");
            }
        }
    }
}