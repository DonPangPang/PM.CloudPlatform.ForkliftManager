using Microsoft.Extensions.Logging;
using PM.CloudPlatform.ForkliftManager.Apis.Kafka;
using PM.CloudPlatform.ForkliftManager.Apis.Managers;
using PM.CloudPlatform.ForkliftManager.Apis.ProtocolReqResps;
using PM.CloudPlatform.ForkliftManager.Apis.Sessions;
using SuperSocket;

namespace PM.CloudPlatform.ForkliftManager.Apis.Handlers
{
    public class EV26MsgIdTcpCustomHandler : EV26MsgIdTcpHandlerBase
    {
        private readonly IEV26Producer _ev26Producer;
        private readonly IAppSession _session;

        private readonly ILogger<EV26MsgIdTcpCustomHandler> _logger;

        public EV26MsgIdTcpCustomHandler(
            IEV26Producer ev26Producer,
            ILoggerFactory loggerFactory,
            TerminalSessionManager sessionManager,
            IAppSession session) : base(sessionManager, session)
        {
            _ev26Producer = ev26Producer;
            _session = session;
            _logger = loggerFactory.CreateLogger<EV26MsgIdTcpCustomHandler>();
        }

        public override EV26Response NoResponse(EV26Request request)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug(string.Join(" ", request.OriginalPackage));
                _logger.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(request.Package));
            }
            _ev26Producer.ProduceAsync(request.Package.Header.MsgId.ToString(), _session["TerminalId"].ToString(), request.OriginalPackage);

            return base.NoResponse(request);
        }

        public override EV26Response EmptyResponse(EV26Request request)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug(string.Join(" ", request.OriginalPackage));
                _logger.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(request.Package));
            }
            _ev26Producer.ProduceAsync(request.Package.Header.MsgId.ToString(), _session["TerminalId"].ToString(), request.OriginalPackage);

            return base.EmptyResponse(request);
        }

        public override EV26Response Msg0X27(EV26Request request)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug(string.Join(" ", request.OriginalPackage));
                _logger.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(request.Package));
            }
            _ev26Producer.ProduceAsync(request.Package.Header.MsgId.ToString(), _session["TerminalId"].ToString(), request.OriginalPackage);

            return base.Msg0X27(request);
        }

        public override EV26Response Msg0X2A(EV26Request request)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug(string.Join(" ", request.OriginalPackage));
                _logger.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(request.Package));
            }
            _ev26Producer.ProduceAsync(request.Package.Header.MsgId.ToString(), _session["TerminalId"].ToString(), request.OriginalPackage);

            return base.Msg0X2A(request);
        }

        public override EV26Response Msg0X8A(EV26Request request)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug(string.Join(" ", request.OriginalPackage));
                _logger.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(request.Package));
            }
            _ev26Producer.ProduceAsync(request.Package.Header.MsgId.ToString(), _session["TerminalId"].ToString(), request.OriginalPackage);

            return base.Msg0X8A(request);
        }
    }
}