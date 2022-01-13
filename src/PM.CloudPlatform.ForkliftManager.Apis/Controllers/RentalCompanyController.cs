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
    /// 租借公司
    /// </summary>
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize]
    public class RentalCompanyController : MyControllerBase<RentalCompanyRepository, RentalCompany, RentalCompanyDto>
    {
        /// <summary>
        /// </summary>
        /// <param name="repository"> </param>
        /// <param name="mapper">     </param>
        public RentalCompanyController(RentalCompanyRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}