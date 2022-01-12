using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using NbazhGPS.Protocol;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.Interfaces;
using NbazhGPS.Protocol.MessageBody;
using PM.CloudPlatform.ForkliftManager.Apis.Managers;
using PM.CloudPlatform.ForkliftManager.Apis.ProtocolReqResps;
using PM.CloudPlatform.ForkliftManager.Apis.Sessions;
using SuperSocket;

namespace PM.CloudPlatform.ForkliftManager.Apis.Handlers
{
    /// <summary>
    /// Tcp消息处理器基类 <remark>
    /// 作者: Powers
    /// 创始时间: 2022-01-07 描述: 通过传入的消息, 生成对应的应答消息 并返回 </remark>
    /// </summary>
    public class EV26MsgIdTcpHandlerBase
    {
        private readonly IAppSession _session;

        protected TerminalSessionManager SessionManager { get; }

        public Dictionary<byte, Func<EV26Request, EV26Response>> HandlerDict { get; protected set; }

        protected EV26MsgIdTcpHandlerBase(TerminalSessionManager sessionManager, IAppSession session)
        {
            _session = session;
            SessionManager = sessionManager;

            HandlerDict = new Dictionary<byte, Func<EV26Request, EV26Response>>()
            {
                [NbazhGpsMessageIds.登陆包.ToByteValue<NbazhGpsMessageIds>()] = new Func<EV26Request, EV26Response>(EmptyResponse),
                [NbazhGpsMessageIds.心跳包.ToByteValue<NbazhGpsMessageIds>()] = new Func<EV26Request, EV26Response>(EmptyResponse),
                [NbazhGpsMessageIds.Gps定位包.ToByteValue()] = NoResponse,
                [NbazhGpsMessageIds.Lbs多基站扩展包.ToByteValue()] = NoResponse,
                [NbazhGpsMessageIds.报警包_单围栏.ToByteValue()] = EmptyResponse,
                [NbazhGpsMessageIds.报警包_多围栏.ToByteValue()] = EmptyResponse,
                [NbazhGpsMessageIds.Gps地址请求包.ToByteValue()] = Msg0X2A,
                [NbazhGpsMessageIds.Lbs地址请求包.ToByteValue()] = Msg0X2A,
                [NbazhGpsMessageIds.校时包.ToByteValue()] = Msg0X8A,
                [NbazhGpsMessageIds.信息传输通用包.ToByteValue()] = NoResponse,
                [NbazhGpsMessageIds.终端在线指令回复.ToByteValue()] = NoResponse,
            };
        }

        public virtual EV26Response NoResponse(EV26Request request) => (EV26Response)null;

        public virtual EV26Response EmptyResponse(EV26Request request)
        {
            return new EV26Response(new NbazhGpsPackage()
            {
                Header = new NbazhGpsHeader()
                {
                    MsgId = request.Package.Header.MsgId,
                    MsgNum = request.Package.Header.MsgNum
                }
            });
        }

        // TODO:这个有问题
        public virtual EV26Response Msg0X27(EV26Request request)
        {
            var pack = request.Package.Bodies as Nbazh0X27;
            if (pack.LanguageExtensionPortStatus.Equals(LanguageExtensionPortStatus.不需要平台回复))
            {
                return NoResponse(request);
            }

            if (pack!.Alarm.Equals(Alarm0X26.SOS求救))
            {
                return new EV26Response(
                    NbazhGpsMessageIds.中文地址回复包.Create(request.Package.Header.MsgNum, new Nbazh0X17_1()
                    {
                        ServerFlagBits = (int)Alarm0X26.SOS求救,
                        ALARMSMS = "ALARMSMS",
                        AddressContent = "",
                        TelephoneNumber = "110"
                    }));
            }
            else
            {
                return EmptyResponse(request);
            }
        }

        public virtual EV26Response Msg0X2A(EV26Request request)
        {
            var pack = request.Package.Bodies as Nbazh0X2A;

            if (pack!.LanguageExtensionPortStatus.Equals(LanguageExtensionPortStatus.中文))
            {
                return new EV26Response(
                    NbazhGpsMessageIds.中文地址回复包.Create(request.Package.Header.MsgNum, new Nbazh0X17_1()
                    {
                        ServerFlagBits = (int)Alarm0X26.SOS求救,
                        ALARMSMS = "ALARMSMS",
                        AddressContent = "",
                        TelephoneNumber = pack.TelephoneNumber
                    }));
            }
            else
            {
                return new EV26Response(
                    NbazhGpsMessageIds.英文地址回复包.Create(request.Package.Header.MsgNum, new Nbazh0X17_1()
                    {
                        ServerFlagBits = (int)Alarm0X26.SOS求救,
                        ALARMSMS = "ALARMSMS",
                        AddressContent = "",
                        TelephoneNumber = pack.TelephoneNumber
                    }, PackageType.Type2));
            }
        }

        public virtual EV26Response Msg0X8A(EV26Request request)
        {
            return new EV26Response(NbazhGpsMessageIds.校时包.Create(request.Package.Header.MsgNum, new Nbazh0X8A()
            {
                DateTime = DateTime.Now
            }));
        }
    }
}