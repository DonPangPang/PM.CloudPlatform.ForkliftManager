using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pang.AutoMapperMiddleware;
using PM.CloudPlatform.ForkliftManager.Apis.Controllers.Base;
using PM.CloudPlatform.ForkliftManager.Apis.DtoParameters.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Extensions;
using PM.CloudPlatform.ForkliftManager.Apis.General;
using PM.CloudPlatform.ForkliftManager.Apis.Models;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Controllers
{
    /// <summary>
    /// 租借记录
    /// </summary>
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize]
    public class RentalRecordController : MyControllerBase<RentalRecordRepository, RentalRecord, RentalRecordDto, RentalRecordAddOrUpdateDto>
    {
        private readonly IGeneralRepository _generalRepository;

        /// <summary>
        /// </summary>
        /// <param name="repository">        </param>
        /// <param name="mapper">            </param>
        /// <param name="generalRepository"> </param>
        public RentalRecordController(RentalRecordRepository repository, IMapper mapper, IGeneralRepository generalRepository) : base(repository, mapper)
        {
            _generalRepository = generalRepository;
        }

        /// <summary>
        /// 获取租赁车辆档案
        /// </summary>
        /// <param name="parameters"> </param>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> GetRentalCars([FromQuery] DtoParametersBase parameters)
        {
            var data = await _generalRepository.GetQueryable<RentalRecord>()
                .Include(x => x.RentalCompany)
                .Include(x => x.Car)
                .ThenInclude(t => t!.CarType)
                .ApplyPaged(parameters)
                .Select(x => new
                {
                    Source = x.MapTo<RentalRecord>(),
                    RentalRecordId = x.Id,
                    CarId = x.Car!.Id,
                    LicensePlateNumber = x.Car!.LicensePlateNumber,
                    Brand = x.Car!.Brand,
                    SerialNumber = x.Car!.SerialNumber,
                    CarType = x.Car.CarType!.Name,
                    RentalStartTime = x.RentalStartTime,
                    RentalEndTime = x.RentalEndTime
                })
                .ToListAsync();

            return Success(data);
        }
    }
}