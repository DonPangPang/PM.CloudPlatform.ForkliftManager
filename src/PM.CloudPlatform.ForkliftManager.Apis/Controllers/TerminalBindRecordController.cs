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
    /// 车辆绑定记录
    /// </summary>
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize("Identify")]
    public class TerminalBindRecordController : MyControllerBase<TerminalBindRecordsRepository, TerminalBindRecord, TerminalBindRecordDto, TerminalBindRecordAddOrUpdateDto>
    {
        private readonly IGeneralRepository _generalRepository;
        public TerminalBindRecordController(TerminalBindRecordsRepository repository, IMapper mapper, IGeneralRepository generalRepository) : base(repository, mapper)
        {
            _generalRepository = generalRepository;
        }

        public class BindRecordDtoParameters: DtoParametersBase
        {
            /// <summary>
            /// imei
            /// </summary>
            /// <value></value>
            public string? IMEI{get; set;}
            /// <summary>
            /// 车牌号
            /// </summary>
            /// <value></value>
            public string? LicensePlateNumber{get; set;}
        }

        /// <summary>
        /// 获取车辆绑定记录
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="imei"></param>
        /// <param name="licensePlateNumber"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetBindRecord([FromQuery]DtoParametersBase parameters, string? imei, string? licensePlateNumber)
        {
            var records = await _generalRepository.GetQueryable<TerminalBindRecord>()
                .ApplyPaged(parameters)
                .Include(x=>x.Car)
                .Include(x=>x.Terminal)
                .Where(x=>x.Terminal!.IMEI.Contains(imei ?? ""))
                .Where(x=>x.Car!.LicensePlateNumber!.Contains(licensePlateNumber ?? ""))
                .Select(x=>new 
                {
                    Id = x.Id,
                    IMEI = x.Terminal!.IMEI,
                    LicensePlateNumber = x.Car!.LicensePlateNumber,
                    IsActive = x.IsActive,
                    Description=x.Description
                })
                .ToListAsync();

                return Success(records);
        }
    }
}
