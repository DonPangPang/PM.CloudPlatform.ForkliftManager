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
    /// 车辆报警记录
    /// </summary>
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize("Identify")]
    public class AlarmRecordController : MyControllerBase<AlarmRecordRepository, AlarmRecord, AlarmRecordDto, AlarmRecordAddOrUpdateDto>
    {
        public AlarmRecordController(AlarmRecordRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
