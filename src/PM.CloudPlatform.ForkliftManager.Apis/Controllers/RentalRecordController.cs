using System;
using System.Collections.Generic;
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
    [Authorize("Identify")]
    public class RentalRecordController : MyControllerBase<RentalRecordRepository, RentalRecord, RentalRecordDto, RentalRecordAddOrUpdateDto>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// </summary>
        /// <param name="repository">        </param>
        /// <param name="mapper">            </param>
        /// <param name="generalRepository"> </param>
        public RentalRecordController(RentalRecordRepository repository, IMapper mapper, IGeneralRepository generalRepository) : base(repository, mapper)
        {
            _mapper = mapper;
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
                .FilterDeleted()
                .Include(x => x.RentalCompany)
                .Include(x => x.Car)
                .ThenInclude(t => t!.CarType)
                .ApplyPaged(parameters)
                .Select(x => new
                {
                    //Source = x.MapTo<RentalRecord>(),
                    Id = x.Id,
                    CarId = x.Car!.Id,
                    RentalCompanyName = x.RentalCompany!.Name,
                    LicensePlateNumber = x.Car!.LicensePlateNumber,
                    Brand = x.Car!.Brand,
                    SerialNumber = x.Car!.SerialNumber,
                    CarType = x.Car.CarType!.Name,
                    RentalStartTime = x.RentalStartTime,
                    RentalEndTime = x.RentalEndTime,
                    IsNeedReturn = (x.RentalEndTime < DateTime.Now.Date),
                    x.IsReturn,
                    x.ReturnTime,
                    x.RentalEmployeeName,
                    x.RentalEmployeeTel,

                })
                .AsSplitQuery()
                .ToListAsync();

            return Success(data);
        }

        /// <summary>
        /// 添加租赁记录
        /// </summary>
        /// <param name="dtos"> </param>
        /// <returns> </returns>
        [HttpPost]
        public async Task<IActionResult> CreateRentalRecords([FromBody] IEnumerable<RentalRecordAddOrUpdateDto> dtos)
        {
            if (dtos == null) throw new ArgumentNullException(nameof(dtos));

            var entities = dtos.MapTo<RentalRecord>();

            foreach (var item in entities)
            {
                item.Create();
                var car = await _generalRepository.FindAsync<Car>(x => x.Id.Equals(item.CarId));
                car.ElectronicFenceId = item.ElectronicFenceId;
                await _generalRepository.UpdateAsync(car);
            }

            await _generalRepository.InsertAsync(entities);
            await _generalRepository.SaveAsync();

            return Success("保存成功");
        }

        ///// <summary>
        ///// 批量归还车辆
        ///// </summary>
        ///// <param name="dtos"> </param>
        ///// <returns> </returns>
        //[HttpPost]
        //[Obsolete("弃用该接口", true)]
        //public async Task<IActionResult> ReturnCars([FromBody] IEnumerable<RentalRecordAddOrUpdateDto> dtos)
        //{
        //    if (dtos == null) throw new ArgumentNullException(nameof(dtos));

        //    var entities = dtos.MapTo<RentalRecord>();

        //    foreach (var item in entities)
        //    {
        //        item.Modify();
        //        var car = await _generalRepository.FindAsync<Car>(x => x.Id.Equals(item.CarId));
        //        car.ElectronicFenceId = null;
        //        await _generalRepository.UpdateAsync(car);
        //    }

        //    await _generalRepository.UpdateAsync(entities);
        //    await _generalRepository.SaveAsync();

        //    return Success("归还成功");
        //}

        /// <summary>
        /// 归还车辆
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        public async Task<IActionResult> ReturnCars([FromBody] IEnumerable<RentalRecordReturnDto> dtos)
        {
            if(dtos is null) throw new ArgumentNullException(nameof(dtos));

            //var entities = dtos.MapTo<RentalRecord>();

            foreach (var item in dtos)
            {
                var oldEntity = await _generalRepository.GetQueryable<RentalRecord>().FirstOrDefaultAsync(x=>x.Id.Equals(item.Id));
                oldEntity.Modify();
                _mapper.Map(item, oldEntity);
                await _generalRepository.UpdateAsync(oldEntity);
                await _generalRepository.SaveAsync();
            }

            //foreach (var item in entities)
            //{
            //    var car = await _generalRepository.FindAsync<Car>(x => x.Id.Equals(item.CarId));
            //    car.ElectronicFenceId = null;
            //    await _generalRepository.UpdateAsync(car);
            //}

            //await _generalRepository.SaveAsync();

            return Success("归还成功");
        }

        /// <summary>
        /// 获取已到达归还日期的记录
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetNeedReturnCars([FromQuery]DtoParametersBase parameters)
        {
            var result = await _generalRepository.GetQueryable<RentalRecord>()
                .FilterDeleted()
                .Where(x => x.RentalEndTime < DateTime.Now.Date)
                .Include(x => x.RentalCompany)
                .Include(x => x.Car)
                .ThenInclude(t => t!.CarType)
                .ApplyPaged(parameters)
                .Select(x => new
                {
                    //Source = x.MapTo<RentalRecord>(),
                    RentalRecordId = x.Id,
                    CarId = x.Car!.Id,
                    RentalCompanyName = x.RentalCompany!.Name,
                    LicensePlateNumber = x.Car!.LicensePlateNumber,
                    Brand = x.Car!.Brand,
                    SerialNumber = x.Car!.SerialNumber,
                    CarType = x.Car.CarType!.Name,
                    RentalStartTime = x.RentalStartTime,
                    RentalEndTime = x.RentalEndTime,
                    IsNeedReturn = (x.RentalEndTime < DateTime.Now.Date)
                })
                .AsSplitQuery()
                .ToListAsync();

            return Success(result);
        }
    }
}