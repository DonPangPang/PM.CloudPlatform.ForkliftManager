using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PM.CloudPlatform.ForkliftManager.Apis.Controllers.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Models;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories;

namespace PM.CloudPlatform.ForkliftManager.Apis.Controllers
{
    /// <summary>
    /// 车辆档案
    /// </summary>
    [ApiController]
    [EnableCors("any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize]
    public class CarController : MyControllerBase<CarRepository, Car, CarDto>
    {
        private readonly CarRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// </summary>
        /// <param name="repository"> </param>
        /// <param name="mapper">     </param>
        public CarController(CarRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}