using System.Threading.Tasks;
using AutoMapper;
using Confluent.Kafka;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Pang.AutoMapperMiddleware;
using PM.CloudPlatform.ForkliftManager.Apis.Controllers.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
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
        public async Task<IActionResult> GetGpsPositionRecords()
        {
            var data = await _generalRepository.FindAsync<GpsPositionRecord>();

            var res = data.MapTo<GpsPositionRecordDto>();

            return Success(res);
        }
    }
}