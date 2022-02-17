using AutoMapper;
using PM.CloudPlatform.ForkliftManager.Apis.Controllers.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Models;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories;

namespace PM.CloudPlatform.ForkliftManager.Apis.Controllers
{
    /// <summary>
    /// 车辆绑定记录
    /// </summary>
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize("Identify")]
    public class TerminalBindRecordController : MyControllerBase<TerminalBindRecordsRepository, TerminalBindRecord, TerminalBindRecordDto, TerminalBindRecordAddOrUpdateDto>
    {
        public TerminalBindRecordController(TerminalBindRecordsRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
