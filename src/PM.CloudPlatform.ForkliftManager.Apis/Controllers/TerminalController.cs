using System;
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
using PM.CloudPlatform.ForkliftManager.Apis.Extensions;
using PM.CloudPlatform.ForkliftManager.Apis.General;
using PM.CloudPlatform.ForkliftManager.Apis.Managers;
using PM.CloudPlatform.ForkliftManager.Apis.Models;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Sessions;
using SuperSocket;

namespace PM.CloudPlatform.ForkliftManager.Apis.Controllers
{
    /// <summary>
    /// 终端管理
    /// </summary>
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize("Identify")]
    public class TerminalController : MyControllerBase<TerminalRepository, Terminal, TerminalDto, TerminalAddOrUpdateDto>
    {
        private readonly TerminalSessionManager _gpsTrackerSessionManager;
        private readonly IGeneralRepository _generalRepository;

        public TerminalController(TerminalRepository repository, IMapper mapper, TerminalSessionManager gpsTrackerSessionManager, IGeneralRepository generalRepository) : base(repository, mapper)
        {
            _gpsTrackerSessionManager = gpsTrackerSessionManager;
            _generalRepository = generalRepository;
        }

        /// <summary>
        /// 获取所有在线终端
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        public IActionResult GetOnlineTerminals()
        {
            var terminals = _gpsTrackerSessionManager.GetAllSessions().Where(x => x.Value.State == SessionState.Connected).Select(x => new { x.Key, x.Value.State });
            return Success(terminals);
        }

        /// <summary>
        /// 获取所有不在线设备
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> GetOfflineTerminals()
        {
            var onlineTerminals = _gpsTrackerSessionManager.GetAllSessions().Values
                .Select(x => x["TerminalId"].ToString());

            var offlineTerminals = await _generalRepository.GetQueryable<Terminal>()
                .Where(x => !onlineTerminals.Contains(x.IMEI)).Select(x => new { IMEI = x.IMEI, IsOnline = false }).ToListAsync();

            return Success(offlineTerminals);
        }

        /// <summary>
        /// 获取所有终端及状态
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> GetTerminals()
        {
            var onlineTerminals = _gpsTrackerSessionManager.GetAllSessions().Values
                .Select(x => x["TerminalId"].ToString());

            var allTerminals = await _generalRepository.GetQueryable<Terminal>()
                .Where(x => onlineTerminals.Contains(x.IMEI)).Select(x => new { IMEI = x.IMEI, IsOnline = true }).ToListAsync();

            var offlineTerminals = await _generalRepository.GetQueryable<Terminal>()
                .Where(x => !onlineTerminals.Contains(x.IMEI)).Select(x => new { IMEI = x.IMEI, IsOnline = false }).ToListAsync();

            if (allTerminals is null)
            {
                return Success(offlineTerminals);
            }

            allTerminals.AddRange(offlineTerminals);

            return Success(allTerminals);
        }

        /// <summary>
        /// 获取所有终端及车辆信息
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> GetTerminalsIncludeCars()
        {
            var onlineTerminals = _gpsTrackerSessionManager.GetAllSessions().Values
                .Select(x => x["TerminalId"].ToString());

            var allTerminals = await _generalRepository.GetQueryable<Terminal>()
                .FilterDeleted()
                .Include(t => t.TerminalBindRecords)
                .ThenInclude(x => x.Terminal)
                .Where(x => onlineTerminals.Contains(x.IMEI))
                .Select(x => new { IMEI = x.IMEI, CarInfo = x.TerminalBindRecords!.OrderByDescending(t => t.CreateDate).FirstOrDefault()!.Car!.MapTo<CarDto>(), IsOnline = true })
                .ToListAsync();

            var offlineTerminals = await _generalRepository.GetQueryable<Terminal>()
                .Include(t => t.TerminalBindRecords)
                .ThenInclude(x => x.Terminal)
                .Where(x => !onlineTerminals.Contains(x.IMEI))
                .Select(x => new { IMEI = x.IMEI, CarInfo = x.TerminalBindRecords!.OrderByDescending(t => t.CreateDate).FirstOrDefault()!.Car!.MapTo<CarDto>(), IsOnline = false })
                .ToListAsync();

            if (allTerminals is null)
            {
                return Success(offlineTerminals);
            }

            allTerminals.AddRange(offlineTerminals);

            return Success(allTerminals);
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

        /// <summary>
        /// 获取终端绑定车辆的状态
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTerminalStatus([FromQuery] DtoParametersBase parameters)
        {
            var terminals = await _generalRepository.GetQueryable<Terminal>()
                .FilterDeleted()
                .FilterDisabled()
                .ApplyPaged(parameters)
                .Select(x => new
                {
                    x.Id,
                    x.IMEI,
                    x.EnableMark,
                    x.DeleteMark,
                    Binded = (x.CarId == null)
                })
                .ToListAsync();

            return Success(terminals);
        }

        /// <summary>
        /// 获取终端信息以及绑定车辆
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTerminalsBindStatus([FromQuery]DtoParametersBase parameters)
        {
            var data = await _generalRepository.GetQueryable<Terminal>()
                .FilterDeleted()
                .FilterDisabled()
                .Include(x=>x.Car)
                .ApplyPaged(parameters)
                .Select(x => new
                {
                    x.Id,
                    CarId = x.Car == null ? "" : x.Car.Id.ToString(),
                    x.IMEI,
                    LicensePlateNumber = x.Car == null ? null : x.Car.LicensePlateNumber,
                    x.EnableMark,
                    x.DeleteMark,
                    Binded = !(x.CarId == null)
                })
                .ToListAsync();
            return Success(data);
        }

        /// <summary>
        /// 获取未绑定的终端
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUnbindTerminals()
        {
            var terminals = await _generalRepository.GetQueryable<Terminal>()
                .FilterDeleted()
                .FilterDisabled()
                .Where(x=>x.CarId == null)
                .ToListAsync();

            var returnDto = terminals.MapTo<TerminalDto>();

            return Success(returnDto);
        }
    }
}