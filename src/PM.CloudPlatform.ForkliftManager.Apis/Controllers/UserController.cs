using System.Linq;
using System.Threading.Tasks;
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

        /// <summary>
        /// </summary>
        /// <param name="repository">        </param>
        /// <param name="mapper">            </param>
        /// <param name="generalRepository"> </param>
        public UserController(UserRepository repository, IMapper mapper, IGeneralRepository generalRepository) : base(repository, mapper)
        {
            _generalRepository = generalRepository;
            _user = _generalRepository.GetQueryable<User>();
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

            return Ok(userInfo);
        }

        /// <summary>
        /// 获取登录用户的角色
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> GetLoginUserRoles()
        {
            var userInfo = LoginUserInfo.Get<UserDto>();

            var userRoles = await _user.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id.Equals(userInfo.Id));

            return Ok(userRoles);
        }

        ///// <summary>
        ///// 获取登录用户的模块
        ///// </summary>
        ///// <returns> </returns>
        //public async Task<IActionResult> GetLoginUserModules()
        //{
        //    var userInfo = LoginUserInfo.Get<UserDto>();

        // var userRoles = await _user.Include(x => x.Roles).ThenInclude(y => y.Modules)
        // .FirstOrDefaultAsync(x => x.Id.Equals(userInfo.Id));

        //    return Ok(userRoles);
        //}
    }
}