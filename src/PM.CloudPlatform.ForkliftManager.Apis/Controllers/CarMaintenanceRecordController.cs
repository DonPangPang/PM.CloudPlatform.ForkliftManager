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
    /// 车辆保养记录
    /// </summary>
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize("Identify")]
    public class CarMaintenanceRecordController : MyControllerBase<CarMaintenanceRecordRepository, CarMaintenanceRecord, CarMaintenanceRecordDto, CarMaintenanceRecordAddOrUpdateDto>
    {
        private readonly IGeneralRepository _generalRepository;

        public CarMaintenanceRecordController(CarMaintenanceRecordRepository repository, IMapper mapper, IGeneralRepository generalRepository) : base(repository, mapper)
        {
            _generalRepository = generalRepository;
        }

        /// <summary>
        /// 获取车辆维护的历史记录
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCarMaintenanceRecords([FromQuery]DtoParametersBase parameters)
        {
            var data = await _generalRepository.GetQueryable<CarMaintenanceRecord>()
                .FilterDeleted()
                .Include(x => x.Car)
                .ThenInclude(t => t!.CarType)
                .ApplyPaged(parameters)
                .Select(x => new
                {
                    x.Id,
                    x.CarId,
                    x.Car!.LicensePlateNumber,
                    x.Car!.CarType!.Name,
                    x.Maintainer,
                    x.MaintainerTel,
                    x.MaintainerContent,
                    x.Remarks,
                    x.MaintenanceTime,
                    x.MaintenanceDateLength
                })
                .ToListAsync();

            return Success(data);
        }

        /// <summary>
        /// 获取车辆自上次维护后的使用时长
        /// </summary>
        /// <param name="parameters"> </param>
        /// <returns> </returns>
        // TODO: 车辆使用记录
        [HttpGet]
        public async Task<IActionResult> GetCarUseRecordsMaintenance([FromQuery] DtoParametersBase parameters)
        {
            //var maintenanceRecords = await _generalRepository.GetQueryable<CarMaintenanceRecord>()
            //    .GroupBy(x => x.CarId)
            //    .Select(groups => new
            //    {
            //        Id = groups.Key,
            //        FirstOrDefault = groups.OrderByDescending(t => t.CreateDate).FirstOrDefault()
            //    })
            //    .ToListAsync();

            //var data = await _generalRepository.GetQueryable<Car>()
            //    .Include(x => x.CarType)
            //    .Include(x => x.UseRecords!
            //        .Where(u => u.CreateDate > maintenanceRecords
            //            .FirstOrDefault(t => t.Id.Equals(x.Id))!.FirstOrDefault!.CreateDate)/*.Sum(x => x.LengthOfTime)*/)
            //    .Select(x => new
            //    {
            //        Source = x.MapTo<CarDto>(),
            //        CarId = x.Id,
            //        LicensePlateNumber = x.LicensePlateNumber,
            //        Brand = x.Brand,
            //        SerialNumber = x.SerialNumber,
            //        BuyTime = x.BuyTime,
            //        LengthOfTime = x.UseRecords!.Sum(t => t.LengthOfTime),
            //    })
            //    .ToListAsync();

            var data = await _generalRepository.GetQueryable<Car>()
                .FilterDeleted()
                .Include(x => x.CarType)
                .Include(x => x.UseRecords)
                .Include(x => x.CarMaintenanceRecords)
                .ApplyPaged(parameters)
                .Select(x => new
                {
                    //Source = x.MapTo<CarDto>(),
                    CarId = x.Id,
                    LicensePlateNumber = x.LicensePlateNumber,
                    Brand = x.Brand,
                    SerialNumber = x.SerialNumber,
                    BuyTime = x.BuyTime,
                    LengthOfUse = x.UseRecords!.Where(
                        t => x.CarMaintenanceRecords!.Any() ?
                            //.OrderByDescending(t => t.CreateDate)
                            //.FirstOrDefault()!.CreateDate
                            t.CreateDate > x.CarMaintenanceRecords!.Max(t => t.CreateDate)
                                 : true)
                        .Sum(t => t.LengthOfTime),
                    MaintenanceTimes = x.CarMaintenanceRecords!.Count,
                    LastOfMaintenanceTime = x.CarMaintenanceRecords
                        .Max(t => t.CreateDate)
                })
                .AsSplitQuery()
                .ToListAsync();

            return Success(data);
        }

        /// <summary>
        /// 获取车辆总使用时长
        /// </summary>
        /// <param name="parameters"> </param>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> GetCarUseRecordsFill([FromQuery] DtoParametersBase parameters)
        {
            //var maintenanceRecords = await _generalRepository.GetQueryable<CarMaintenanceRecord>()
            //    .GroupBy(x => x.CarId)
            //    .Select(groups => new
            //    {
            //        Id = groups.Key,
            //        FirstOrDefault = groups.OrderByDescending(t => t.CreateDate).FirstOrDefault()
            //    })
            //    .ToListAsync();

            //var data = await _generalRepository.GetQueryable<Car>()
            //    .Include(x => x.CarType)
            //    .Include(x => x.UseRecords/*.Sum(x => x.LengthOfTime)*/)
            //    .Select(y => new { y, Sum = y.UseRecords!.Sum(t => t.LengthOfTime) })
            //    .ToListAsync();

            //return Success(data);

            var data = await _generalRepository.GetQueryable<Car>()
                .FilterDeleted()
                .Include(x => x.CarType)
                .Include(x => x.UseRecords)
                .Include(x => x.CarMaintenanceRecords)
                .ApplyPaged(parameters)
                .Select(x => new
                {
                    //Source = x.MapTo<CarDto>(),
                    CarId = x.Id,
                    LicensePlateNumber = x.LicensePlateNumber,
                    Brand = x.Brand,
                    SerialNumber = x.SerialNumber,
                    BuyTime = x.BuyTime,
                    LengthOfUse = x.UseRecords!.Sum(t => t.LengthOfTime) + x.LengthOfUse,
                    MaintenanceTimes = x.CarMaintenanceRecords!.Count,
                    //.OrderByDescending(t => t.CreateDate).FirstOrDefault()!.CreateDate
                    LastOfMaintenanceTime = x.CarMaintenanceRecords.Max(t => t.CreateDate)
                })
                .AsSplitQuery()
                .ToListAsync();
            return Success(data);
        }
    }
}