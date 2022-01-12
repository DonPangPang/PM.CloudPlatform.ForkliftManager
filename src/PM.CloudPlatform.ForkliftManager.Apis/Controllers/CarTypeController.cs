using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PM.CloudPlatform.ForkliftManager.Apis.Controllers.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Models;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Controllers
{
    /// <summary>
    /// 车辆类型
    /// </summary>
    [ApiController]
    [EnableCors("any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize]
    public class CarTypeController : MyControllerBase<CarTypeRepository, CarType, CarTypeDto>
    {
        /// <summary>
        /// </summary>
        /// <param name="repository"> </param>
        /// <param name="mapper">     </param>
        public CarTypeController(CarTypeRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}