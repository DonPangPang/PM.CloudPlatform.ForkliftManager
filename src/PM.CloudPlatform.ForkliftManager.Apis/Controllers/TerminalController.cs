using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NbazhGPS.Protocol;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.MessageBody;
using PM.CloudPlatform.ForkliftManager.Apis.Controllers.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Managers;
using PM.CloudPlatform.ForkliftManager.Apis.Models;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Sessions;
using SuperSocket;

namespace PM.CloudPlatform.ForkliftManager.Apis.Controllers
{
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize]
    public class TerminalController : MyControllerBase<TerminalRepository, Terminal, TerminalDto, TerminalAddOrUpdateDto>
    {
        private readonly TerminalSessionManager _gpsTrackerSessionManager;

        public TerminalController(TerminalRepository repository, IMapper mapper, TerminalSessionManager gpsTrackerSessionManager) : base(repository, mapper)
        {
            _gpsTrackerSessionManager = gpsTrackerSessionManager;
        }

        [HttpGet]
        public IActionResult GetTerminals()
        {
            var terminals = _gpsTrackerSessionManager.GetAllSessions().Select(x => new { x.Key, x.Value.State });
            return Success(terminals);
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

            return Success();
        }
    }
}