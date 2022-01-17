using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PM.CloudPlatform.ForkliftManager.Apis.Controllers.Base;
using PM.CloudPlatform.ForkliftManager.Apis.DtoParameters.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.General;
using PM.CloudPlatform.ForkliftManager.Apis.Models;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Controllers
{
    /// <summary>
    /// 车辆保养记录
    /// </summary>
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize]
    public class CarMaintenanceRecordController : MyControllerBase<CarMaintenanceRecordRepository, CarMaintenanceRecord, CarMaintenanceRecordDto, CarMaintenanceRecordAddOrUpdateDto>
    {
        private readonly IGeneralRepository _generalRepository;

        public CarMaintenanceRecordController(CarMaintenanceRecordRepository repository, IMapper mapper, IGeneralRepository generalRepository) : base(repository, mapper)
        {
            _generalRepository = generalRepository;
        }

        // TODO: 车辆使用记录
        [HttpGet]
        public async Task<IActionResult> GetCarUseRecords([FromQuery] DtoParametersBase parameters)
        {
            var maintenanceRecords = await _generalRepository.GetQueryable<CarMaintenanceRecord>()
                .GroupBy(x => x.CarId)
                .Select(groups => new
                {
                    Id = groups.Key,
                    FirstOrDefault = groups.OrderByDescending(t => t.CreateDate).FirstOrDefault()
                })
                .ToListAsync();

            var data = await _generalRepository.GetQueryable<Car>()
                .Include(x => x.UseRecords!.Where(u => u.CreateDate > maintenanceRecords.FirstOrDefault(t => t.Id.Equals(x.Id))!.FirstOrDefault!.CreateDate).Sum(x => x.LengthOfTime))
                .Select(y => new { })

                .ToListAsync();

            return Success(data);
        }
    }
}