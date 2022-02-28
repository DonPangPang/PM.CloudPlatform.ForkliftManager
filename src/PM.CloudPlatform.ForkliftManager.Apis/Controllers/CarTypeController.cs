using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PM.CloudPlatform.ForkliftManager.Apis.Controllers.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Extensions;
using PM.CloudPlatform.ForkliftManager.Apis.General;
using PM.CloudPlatform.ForkliftManager.Apis.Models;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Controllers
{
    /// <summary>
    /// 车辆类型
    /// </summary>
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize("Identify")]
    public class CarTypeController : MyControllerBase<CarTypeRepository, CarType, CarTypeDto, CarTypeAddOrUpdateDto>
    {
        public readonly IGeneralRepository _generalRepository;

        /// <summary>
        /// </summary>
        /// <param name="repository"> </param>
        /// <param name="mapper">     </param>
        public CarTypeController(CarTypeRepository repository, IMapper mapper, IGeneralRepository generalRepository) : base(repository, mapper)
        {
            _generalRepository = generalRepository;
        }

        /// <summary>
        /// 获取未删除以及启用的车辆类型
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetNormalCarTypes()
        {
            var carTypes = await _generalRepository.GetQueryable<CarType>()
                .FilterDeleted()
                .FilterDisabled()
                .ToListAsync();
            return Success(carTypes);
        }
    }
}