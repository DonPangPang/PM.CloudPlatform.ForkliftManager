using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pang.AutoMapperMiddleware;
using PM.CloudPlatform.ForkliftManager.Apis.Controllers.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
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
    [Authorize]
    public class UserController : MyControllerBase<UserRepository, User, UserDto>
    {
        private readonly IGeneralRepository _generalRepository;

        private readonly IQueryable<User> _user;
        private readonly IQueryable<Role> _roles;

        private readonly IQueryable<Module> _modules;

        /// <summary>
        /// </summary>
        /// <param name="repository">        </param>
        /// <param name="mapper">            </param>
        /// <param name="generalRepository"> </param>
        public UserController(UserRepository repository, IMapper mapper, IGeneralRepository generalRepository) : base(repository, mapper)
        {
            _generalRepository = generalRepository;
            _user = _generalRepository.GetQueryable<User>();
            _roles = _generalRepository.GetQueryable<Role>();
            _modules = _generalRepository.GetQueryable<Terminal>()
        }

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <returns> </returns>

        [HttpGet]
        public async Task<IActionResult> GetLoginUserInfo()
        {
            var userInfo = await Task.Run(() =>
            {
                return LoginUserInfo.Get<UserDto>();
            });

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
            var userInfo = LoginUserInfo.Get<UserDto>();

            if (userInfo is not null)
            {
                var user = await _user.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id.Equals(userInfo.Id));

                var returnDto = user.MapTo<UserDto>();

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
            var userInfo = LoginUserInfo.Get<UserDto>();

            if (userInfo is not null)
            {
                var user = await _user.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id.Equals(userInfo.Id));

                var returnDto = user.MapTo<UserDto>();

                return Success(returnDto);
            }
            else
            {
                return Fail("用户登录信息丢失");
            }
        }
    }
}