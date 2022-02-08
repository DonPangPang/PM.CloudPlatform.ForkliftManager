using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Confluent.Kafka;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
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
    /// 车辆轨迹
    /// </summary>
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    public class GpsPositionRecordController : MyControllerBase<GpsPositionRecordRepository, GpsPositionRecord, GpsPositionRecordDto, Null>
    {
        private readonly IGeneralRepository _generalRepository;

        public GpsPositionRecordController(GpsPositionRecordRepository repository, IMapper mapper, IGeneralRepository generalRepository) : base(repository, mapper)
        {
            _generalRepository = generalRepository;
        }

        /// <summary>
        /// 获取所有的定位数据
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> GetGpsPositionRecords([FromQuery] DtoParametersBase parameters)
        {
            var data = await _generalRepository.GetQueryable<GpsPositionRecord>()
                                                           .FilterDeleted()
                                                           .Include(x => x.Terminal)
                                                           .ToPagedAsync(parameters);

            var res = data.MapTo<GpsPositionRecordDto>();

            return Success(res);
        }

        /// <summary>
        /// 获取指定设备的定位数据
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> GetGpsPositionRecordsByTerminal(Guid terminalId, [FromQuery] DtoParametersBase parameters)
        {
            var data = await _generalRepository.GetQueryable<GpsPositionRecord>()
                .FilterDeleted()
                .Include(x => x.Terminal)
                .Where(x => x.TerminalId.Equals(terminalId)).ToPagedAsync(parameters);

            var res = data.MapTo<GpsPositionRecordDto>();

            return Success(res);
        }

        /// <summary>
        /// 获取指定设备的定位数据(点集合)
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> GetGpsPositionRecordsPointsByTerminal(Guid terminalId, [FromQuery] DtoParametersBase parameters)
        {
            var data = await _generalRepository.GetQueryable<GpsPositionRecord>()
                .FilterDeleted()
                .Include(x => x.Terminal)
                .Where(x => x.TerminalId.Equals(terminalId))
                .Select(x=> new { x.Lon, x.Lat, x.CreateDate, x.Speed })
                .OrderBy(t=>t.CreateDate)
                .ToListAsync();

            //var res = data.MapTo<GpsPositionRecordDto>();

            return Success(data);
        }

        /// <summary>
        /// 获取指定设备的定位数据(点集合)
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> GetGpsPositionRecordsPointsByIMEI(string imei, [FromQuery] DtoParametersBase parameters)
        {
            var data = await _generalRepository.GetQueryable<GpsPositionRecord>()
                .FilterDeleted()
                .Include(x => x.Terminal)
                .Where(x => x.Terminal!.IMEI.Equals(imei))
                .ApplyPaged(parameters)
                .Select(x=> new { x.Lon, x.Lat, x.CreateDate, x.Speed })
                .OrderBy(t=>t.CreateDate)
                .ToListAsync();

            //var res = data.MapTo<GpsPositionRecordDto>();

            return Success(data);
        }
    }
}