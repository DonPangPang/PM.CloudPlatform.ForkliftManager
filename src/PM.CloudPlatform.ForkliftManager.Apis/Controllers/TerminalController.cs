using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NbazhGPS.Protocol;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.MessageBody;
using PM.CloudPlatform.ForkliftManager.Apis.Managers;
using PM.CloudPlatform.ForkliftManager.Apis.Sessions;
using SuperSocket;

namespace PM.CloudPlatform.ForkliftManager.Apis.Controllers
{
    [ApiController]
    [EnableCors("any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize]
    public class TerminalController : ControllerBase
    {
        private readonly TerminalSessionManager _gpsTrackerSessionManager;

        public TerminalController(TerminalSessionManager gpsTrackerSessionManager)
        {
            _gpsTrackerSessionManager = gpsTrackerSessionManager;
        }

        [HttpGet]
        public IActionResult GetTerminals()
        {
            var terminals = _gpsTrackerSessionManager.GetAllSessions().Select(x => new { x.Key, x.Value.State });
            return Ok(terminals);
        }

        [HttpGet]
        public async Task<IActionResult> SendCommand(string emei, string command)
        {
            var session = _gpsTrackerSessionManager.Sessions.Values.FirstOrDefault(x => x["TerminalId"].Equals(emei));

            var packet = NbazhGpsMessageIds.在线指令.Create(new Nbazh0X80()
            {
                ServerFlagBits = 1,
                CommandContext = command,
                LanguageExtensionPortStatus = LanguageExtensionPortStatus.中文
            });

            NbazhGpsSerializer nbazhGpsSerializer = new NbazhGpsSerializer();
            var buffer = nbazhGpsSerializer.Serialize(packet);

            await ((IAppSession)session)!.SendAsync(new ReadOnlyMemory<byte>(buffer));

            return Ok();
        }
    }
}