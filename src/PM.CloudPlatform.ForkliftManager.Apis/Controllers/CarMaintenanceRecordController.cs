using System;
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
using PM.CloudPlatform.ForkliftManager.Apis.Extensions;
using PM.CloudPlatform.ForkliftManager.Apis.General;
using PM.CloudPlatform.ForkliftManager.Apis.Models;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories;

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

        public class MaintenanceRecordDtoParameters : DtoParametersBase
        {
            /// <summary>
            /// 车牌号
            /// </summary>
            /// <value></value>
            public string? LicensePlateNumber { get; set; }
            /// <summary>
            /// 车辆编号
            /// </summary>
            /// <value></value>
            public string? SerialNumber{get; set;}
            /// <summary>
            /// 车辆类型
            /// </summary>
            /// <value></value>
            public CarType? CarType{get; set;}
            /// <summary>
            /// 维护时间
            /// </summary>
            /// <value></value>
            public DateTime? MaintenanceTime{get; set;}
            /// <summary>
            /// 车辆类型Id
            /// </summary>
            public Guid? CarTypeId { get; set; }
            /// <summary>
            /// 车辆Id
            /// </summary>
            public Guid? CarId { get; set; }
        }

        /// <summary>
        /// 获取车辆维护的历史记录
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCarMaintenanceRecords([FromQuery] MaintenanceRecordDtoParameters parameters)
        {
            var query = _generalRepository.GetQueryable<CarMaintenanceRecord>()
              .Include(x => x.Car)
              .ThenInclude(t => t!.CarType)           
              .AsQueryable();
            if (!string.IsNullOrEmpty(parameters.CarTypeId.ToString()))
            {
                query = query.Where(x => x.Car!.CarTypeId == parameters.CarTypeId);
            }
            if (!string.IsNullOrEmpty(parameters.CarId.ToString()))
            {
                query = query.Where(x => x.CarId == parameters.CarId);
            }
            if (!string.IsNullOrEmpty(parameters.MaintenanceTime.ToString()))
            {
                query = query.Where(x => x.MaintenanceTime == parameters.MaintenanceTime);
            }

            query = query
                .FilterDeleted()
                .Where(x => x.Car!.LicensePlateNumber!.Contains(parameters.LicensePlateNumber ?? ""))
                .Where(x => x.Car!.SerialNumber!.Contains(parameters.SerialNumber ?? ""));
            var rows = await query.CountAsync();

            var data = await query
                //.Where(x=> parameters.CarType == null ? true:x.Car!.CarType == parameters.CarType)
                //.Where(x=>x.MaintenanceTime == null ? true:x.MaintenanceTime == parameters.MaintenanceTime)
                .ApplyPaged(parameters)
                .Select(x => new
                {
                    x.Id,
                    x.CarId,
                    x.Car!.LicensePlateNumber,
                    x.Car!.SerialNumber,
                    x.Car!.CarType!.Name,
                    x.Maintainer,
                    x.MaintainerTel,
                    x.MaintainerContent,
                    x.Remarks,
                    x.MaintenanceTime,
                    x.MaintenanceDateLength
                })
                .ToListAsync();

            return Success(data, rows);
        }

        public class CarUseRecordsMaintenanceDtoParameters : DtoParametersBase
        {
            /// <summary>
            /// 车牌号
            /// </summary>
            /// <value></value>
            public string? LicensePlateNumber { get; set; }

            /// <summary>
            /// 车辆编号
            /// </summary>
            /// <value></value>
            public string? SerialNumber { get; set; }

            /// <summary>
            /// 车辆类型
            /// </summary>
            /// <value></value>
            public string? CarType { get; set; }
            /// <summary>
            /// 车辆类型Id
            /// </summary>
            public Guid? CarTypeId { get; set; }
            /// <summary>
            /// 车辆Id
            /// </summary>
            public Guid? CarId { get; set; }
        }

        /// <summary>
        /// 获取车辆自上次维护后的使用时长
        /// </summary>
        /// <param name="parameters"> </param>
        /// <returns> </returns>
        // TODO: 车辆使用记录
        [HttpGet]
        public async Task<IActionResult> GetCarUseRecordsMaintenance([FromQuery] CarUseRecordsMaintenanceDtoParameters parameters)
        {
            #region 移除
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
            #endregion 移除

            #region 替换
            // var data = await _generalRepository.GetQueryable<Car>()
            //     .FilterDeleted()
            //     .Include(x => x.CarType)
            //     .Include(x => x.UseRecords)
            //     .Include(x => x.CarMaintenanceRecords)
            //     .ApplyPaged(parameters)
            //     .Select(x => new
            //     {
            //         //Source = x.MapTo<CarDto>(),
            //         CarId = x.Id,
            //         LicensePlateNumber = x.LicensePlateNumber,
            //         Brand = x.Brand,
            //         SerialNumber = x.SerialNumber,
            //         BuyTime = x.BuyTime,
            //         CarTypename=x.CarType!.Name,
            //         LengthOfMaintenanceTime=x.CarType!.LengthOfMaintenanceTime,
            //         LengthOfUse = x.UseRecords!.Where(
            //             t => x.CarMaintenanceRecords!.Any() ?
            //                 //.OrderByDescending(t => t.CreateDate)
            //                 //.FirstOrDefault()!.CreateDate
            //                 t.CreateDate > x.CarMaintenanceRecords!.Max(t => t.CreateDate)
            //                      : true)
            //             .Sum(t => t.LengthOfTime),
            //         MaintenanceTimes = x.CarMaintenanceRecords!.Count,
            //         LastOfMaintenanceTime = x.CarMaintenanceRecords
            //             .Max(t => t.CreateDate)
            //     })
            //     .AsSplitQuery()
            //     .ToListAsync();
            #endregion 替换

            var query = _generalRepository.GetQueryable<Car>()
                .FilterDeleted()
                .Include(x => x.CarType)
                .Include(x => x.UseRecords)
                .Include(x => x.CarMaintenanceRecords)
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.LicensePlateNumber))
            {
                query = query.Where(x => x.LicensePlateNumber!.Contains(parameters.LicensePlateNumber));
            }

            if (!string.IsNullOrEmpty(parameters.SerialNumber))
            {
                query = query.Where(x => x.SerialNumber!.Contains(parameters.SerialNumber));
            }

            if (!string.IsNullOrEmpty(parameters.CarType))
            {
                query = query.Where(x => x.CarType!.Name!.Contains(parameters.CarType));
            }
            if (!string.IsNullOrEmpty(parameters.CarTypeId.ToString()))
            {
                query = query.Where(x => x.CarTypeId==parameters.CarTypeId);
            }
            if (!string.IsNullOrEmpty(parameters.CarId.ToString()))
            {
                query = query.Where(x => x.Id == parameters.CarId);
            }

            var rows = await query.CountAsync();

            var data = await query
                .ApplyPaged(parameters)
                                .Select(x => new
                                {
                                    //Source = x.MapTo<CarDto>(),
                                    CarId = x.Id,
                                    LicensePlateNumber = x.LicensePlateNumber,
                                    Brand = x.Brand,
                                    SerialNumber = x.SerialNumber,
                                    BuyTime = x.BuyTime,
                                    CarTypename = x.CarType!.Name,
                                    LengthOfMaintenanceTime = x.CarType!.LengthOfMaintenanceTime,
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

            return Success(data, rows);
        }

        /// <summary>
        /// 获取车辆总使用时长
        /// </summary>
        /// <param name="parameters"> </param>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> GetCarUseRecordsFill([FromQuery] DtoParametersBase parameters)
        {
            #region 移除
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
            #endregion 移除

            var query = _generalRepository.GetQueryable<Car>()
                .FilterDeleted()
                .Include(x => x.CarType)
                .Include(x => x.UseRecords)
                .Include(x => x.CarMaintenanceRecords);

            var rows = await query.CountAsync();

            var data = await query
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
            return Success(data, rows);
        }
    }
}