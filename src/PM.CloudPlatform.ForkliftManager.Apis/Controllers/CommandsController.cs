using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NbazhGPS.Protocol;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.MessageBody;
using Pang.AutoMapperMiddleware;
using PM.CloudPlatform.ForkliftManager.Apis.Controllers.Base;
using PM.CloudPlatform.ForkliftManager.Apis.DtoParameters.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Enums;
using PM.CloudPlatform.ForkliftManager.Apis.Extensions;
using PM.CloudPlatform.ForkliftManager.Apis.General;
using PM.CloudPlatform.ForkliftManager.Apis.Managers;
using PM.CloudPlatform.ForkliftManager.Apis.Models;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories;
using SuperSocket;

namespace PM.CloudPlatform.ForkliftManager.Apis.Controllers
{
    /// <summary>
    /// 终端命令
    /// </summary>
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize("Identify")]
    public class CommandsController : MyControllerBase<CommandsRepository, Commands, CommandsDto, CommandsAddOrUpdateDto>
    {
        private readonly TerminalSessionManager _gpsTrackerSessionManager;
        private readonly IGeneralRepository _generalRepository;

        public CommandsController(CommandsRepository repository, IMapper mapper, TerminalSessionManager gpsTrackerSessionManager, IGeneralRepository generalRepository) : base(repository, mapper)
        {
            _gpsTrackerSessionManager = gpsTrackerSessionManager;
            _generalRepository = generalRepository;
        }

        /// <summary>
        /// 发送终端控制指令
        /// </summary>
        /// <param name="emei">    </param>
        /// <param name="command"> </param>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> SendCommand(string emei, string command)
        {
            var session = _gpsTrackerSessionManager.Sessions.Values.FirstOrDefault(x => x["TerminalId"].Equals(emei));

            if (session is null)
            {
                return Fail("设备不存在或不在线.");
            }

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