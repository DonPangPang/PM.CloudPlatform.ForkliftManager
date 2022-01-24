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
using PM.CloudPlatform.ForkliftManager.Apis.Models;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories;

namespace PM.CloudPlatform.ForkliftManager.Apis.Controllers
{
    /// <summary>
    /// 系统设置
    /// </summary>
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize]
    public class SystemConfigController : MyControllerBase<SystemConfigRepository, SystemConfig, SystemConfigDto, SystemConfigAddOrUpdateDto>
    {
        private readonly IGeneralRepository generalRepository;

        public SystemConfigController(SystemConfigRepository repository, IMapper mapper, IGeneralRepository generalRepository) : base(repository, mapper)
        {
            this.generalRepository = generalRepository;
        }

        /// <summary>
        /// 查询系统设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetSystemConfig()
        {
            var config = await generalRepository.GetQueryable<SystemConfig>()
                .OrderByDescending(x=>x.CreateDate)
                .FirstOrDefaultAsync();

            if(config is null)
            {
                return Success(new SystemConfig());
            }

            return Success(config);
        }
    }
}