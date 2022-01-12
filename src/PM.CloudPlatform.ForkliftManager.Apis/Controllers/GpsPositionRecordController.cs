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
    /// 车辆轨迹
    /// </summary>
    [ApiController]
    [EnableCors("any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize]
    public class GpsPositionRecordController : MyControllerBase<GpsPositionRecordRepository, GpsPositionRecord, GpsPositionRecordDto>
    {
        public GpsPositionRecordController(GpsPositionRecordRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}