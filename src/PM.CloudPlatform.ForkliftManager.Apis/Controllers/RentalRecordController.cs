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
    /// 租借记录
    /// </summary>
    [ApiController]
    [EnableCors("any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize]
    public class RentalRecordController : MyControllerBase<RentalRecordRepository, RentalRecord, RentalRecordDto>
    {
        /// <summary>
        /// </summary>
        /// <param name="repository"> </param>
        /// <param name="mapper">     </param>
        public RentalRecordController(RentalRecordRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}