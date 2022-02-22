using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PM.CloudPlatform.ForkliftManager.Apis.Controllers.Base;
using PM.CloudPlatform.ForkliftManager.Apis.General;
using System.Threading.Tasks;

namespace PM.CloudPlatform.ForkliftManager.Apis.Controllers.Test
{
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    public class TestController: ControllerBase
    {
        private readonly IGeneralRepository _generalRepository;

        public TestController(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }
    }
}
