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
    /// 车辆报警记录
    /// </summary>
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize("Identify")]
    public class AlarmRecordController : MyControllerBase<AlarmRecordRepository, AlarmRecord, AlarmRecordDto, AlarmRecordAddOrUpdateDto>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IMapper _mapper;
        public AlarmRecordController(AlarmRecordRepository repository, IMapper mapper, IGeneralRepository generalRepository) : base(repository, mapper)
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
        public async Task<IActionResult> GetPageList([FromQuery] DtoParametersBase parameters)
        {
            var data = await _generalRepository.GetQueryable<AlarmRecord>()
                .FilterDeleted()
                .Include(x => x.Car)
                .ApplyPaged(parameters)
                .Select(x => new
                {
                    //Source = x.MapTo<RentalRecord>(),
                    Id = x.Id,
                    CarId = x.Car!.Id,
                    LicensePlateNumber = x.Car!.LicensePlateNumber,
                    Brand = x.Car!.Brand,
                    x.IMEI,
                    x.IsReturn,                
                })
                .ToListAsync();

            return Success(data);
        }
    }
}
