using System;
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
    /// 租借公司
    /// </summary>
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize("Identify")]
    public class RentalCompanyController : MyControllerBase<RentalCompanyRepository, RentalCompany, RentalCompanyDto, RentalCompanyAddOrUpdateDto>
    {
        private readonly IGeneralRepository _generalRepository;

        /// <summary>
        /// </summary>
        /// <param name="repository">        </param>
        /// <param name="mapper">            </param>
        /// <param name="generalRepository"> </param>
        public RentalCompanyController(RentalCompanyRepository repository, IMapper mapper, IGeneralRepository generalRepository) : base(repository, mapper)
        {
            _generalRepository = generalRepository;
        }

        /// <summary>
        /// 获取租赁单位数据(带围栏)
        /// </summary>
        /// <param name="parameters"> </param>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> GetRentalCompanyIncludeElectronicFencePaged([FromQuery] DtoParametersBase parameters)
        {
            var data = await _generalRepository.GetQueryable<RentalCompany>()
                .FilterDeleted()
                .Include(x => x.ElectronicFences)
                .ToPagedAsync(parameters);

            var returnDto = data.MapTo<RentalCompanyDto>();

            return Success(returnDto);
        }

        /// <summary>
        /// 获取某一个租赁单位数据(带围栏)
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> GetRentalCompanyIncludeElectronicFence(Guid id)
        {
            var data = await _generalRepository.GetQueryable<RentalCompany>()
                .FilterDeleted()
                .Include(x => x.ElectronicFences)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var returnDto = data.MapTo<RentalCompanyDto>();

            return Success(returnDto);
        }
    }
}