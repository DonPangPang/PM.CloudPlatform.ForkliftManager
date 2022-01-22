﻿using System.Runtime.CompilerServices;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PM.CloudPlatform.ForkliftManager.Apis.Managers;
using PM.CloudPlatform.ForkliftManager.Apis.Options;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories;
using PM.CloudPlatform.ForkliftManager.Apis.Sessions;
using SuperSocket;
using SuperSocket.ProtoBase;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using NbazhGPS.Protocol;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.MessageBody;
using NetTopologySuite.Geometries;
using Pang.AutoMapperMiddleware;
using PM.CloudPlatform.ForkliftManager.Apis.CorrPacket;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Extensions;
using PM.CloudPlatform.ForkliftManager.Apis.General;
using PM.CloudPlatform.ForkliftManager.Apis.Handlers;
using PM.CloudPlatform.ForkliftManager.Apis.Kafka;
using PM.CloudPlatform.ForkliftManager.Apis.PipelineFilters;
using PM.CloudPlatform.ForkliftManager.Apis.ProtocolReqResps;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;
using SuperSocket.Channel;
using PackageType = PM.CloudPlatform.ForkliftManager.Apis.CorrPacket.PackageType;

#nullable disable

namespace PM.CloudPlatform.ForkliftManager.Apis.Services
{
    /// <summary>
    /// </summary>
    public class TcpSocketServerHostedService : IHostedService
    {
        private readonly IOptions<ServerOption> _serverOptions;
        private readonly IOptions<KafkaOption> _kafkaOptions;
        private readonly ClientSessionManagers _clientSessionManager;
        private readonly TerminalSessionManager _gpsTrackerSessionManager;
        private readonly ILogger<TcpSocketServerHostedService> _logger;
        private readonly IGeneralRepository _generalRepository;
        private readonly NbazhGpsSerializer _nbazhGpsSerializer = new NbazhGpsSerializer();

        private static EV26MsgIdProducer _provider = null;

        /// <summary>
        /// Tcp Server服务
        /// </summary>
        /// <param name="serverOptions">            </param>
        /// <param name="kafkaOptions">             </param>
        /// <param name="clientSessionManager">     </param>
        /// <param name="gpsTrackerSessionManager"> </param>
        /// <param name="logger">                   </param>
        /// <param name="factory">                  </param>
        public TcpSocketServerHostedService(
            IOptions<ServerOption> serverOptions,
            IOptions<KafkaOption> kafkaOptions,
            ClientSessionManagers clientSessionManager,
            TerminalSessionManager gpsTrackerSessionManager,
            ILogger<TcpSocketServerHostedService> logger,
            IServiceScopeFactory factory)
        {
            _serverOptions = serverOptions ?? throw new ArgumentNullException(nameof(serverOptions));
            _kafkaOptions = kafkaOptions;
            _clientSessionManager = clientSessionManager ?? throw new ArgumentNullException(nameof(clientSessionManager));
            _gpsTrackerSessionManager = gpsTrackerSessionManager ?? throw new ArgumentNullException(nameof(gpsTrackerSessionManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _generalRepository = factory.CreateScope().ServiceProvider.GetRequiredService<IGeneralRepository>();

            //_provider = new EV26MsgIdProducer(new ProducerConfig()
            //{
            //    BootstrapServers = kafkaOptions.Value.KafkaConfig
            //}, kafkaOptions);
        }

        /// <summary>
        /// </summary>
        /// <param name="cancellationToken"> </param>
        /// <returns> </returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //var host = SuperSocketHostBuilder.Create<byte[], JTT808PipelineFilter>()
            var host = SuperSocketHostBuilder.Create<NbazhGpsPackage, ProtocolPipelineSwitcher>()
                .ConfigureSuperSocket(opts =>
                {
                    foreach (var listener in _serverOptions.Value.TcpListeners)
                    {
                        opts.AddListener(new ListenOptions()
                        {
                            Ip = listener.Ip,
                            Port = listener.Port
                        });
                    }
                })
                .UseSession<GpsTrackerSession>()
                .UseClearIdleSession()
                .UseSessionHandler(onClosed: async (s, v) =>
                 {
                     try
                     {
                         // Session管理
                         await _gpsTrackerSessionManager.TryRemoveBySessionId(s.SessionID);

                         if (!string.IsNullOrEmpty(s["TerminalId"].ToString()))
                         {
                             var terminal =
                                 await _generalRepository.FindAsync<Terminal>(x =>
                                     x.IMEI.Equals(s["TerminalId"].ToString()));

                             var record = new UseRecord()
                             {
                                 TerminalId = (Guid)terminal.Id!,
                                 StartTime = s.StartTime.DateTime,
                                 EndTime = DateTime.Now,
                                 LengthOfTime = s.StartTime.DateTime.HourDiff(DateTime.Now)
                             };
                             record.Create();
                             await _generalRepository.InsertAsync(record);
                         }
                     }
                     catch
                     {
                         // ignored
                     }
                 })
                .UsePackageHandler(async (s, p) =>
                {
                    try
                    {
                        Console.WriteLine($"{((NbazhGpsMessageIds)p.Header.MsgId).ToString()}:{p.ToJson()}");
                        Console.WriteLine(p.OriginalPackage.ToHexString());

                        if (p.Header.MsgId.Equals(NbazhGpsMessageIds.登陆包.ToByteValue()))
                        {
                            var terminalId = (p.Bodies as Nbazh0X01)!.TerminalId;
                            s["TerminalId"] = terminalId;
                            // TODO: 终端登录
                            var terminal = await _generalRepository.GetQueryable<Terminal>()
                                .FilterDeleted()
                                .FirstOrDefaultAsync(x => x.IMEI.Equals(terminalId), cancellationToken: cancellationToken);

                            if (terminal is not null)
                            {
                                await _gpsTrackerSessionManager.TryAddOrUpdate(terminalId, (s as GpsTrackerSession)!);
                            }
                            else
                            {
                                await s.CloseAsync(CloseReason.ProtocolError);
                            }

                            var terminalLoginRecord = new TerminalLoginRecord()
                            {
                                TerminalId = terminal!.Id
                            };
                            terminalLoginRecord.Create();
                            await _generalRepository.InsertAsync(terminalLoginRecord);
                        }

                        if (p.Header.MsgId.Equals(NbazhGpsMessageIds.Gps定位包.ToByteValue()))
                        {
                            var gpsPositionRecord = (p.Bodies as Nbazh0X22)!.MapTo<GpsPositionRecordTemp>().MapTo<GpsPositionRecord>();

                            await Task.Run(async () =>
                            {
                                gpsPositionRecord.Create(Guid.NewGuid(), s["TerminalId"].ToString() ?? "Unknown Terminal.");

                                await _generalRepository.InsertAsync<GpsPositionRecord>(gpsPositionRecord);
                                await _generalRepository.SaveAsync();

                                var terminal = await _generalRepository.GetQueryable<Terminal>()
                                    .FilterDeleted()
                                    .FilterDisabled()
                                    .Include(x => x.Car)
                                    .Include(x => x.AlarmRecords.Where(t => !t.IsReturn))
                                    .Where(x => x.IMEI.Equals(s["TerminalId"].ToString()))
                                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                                if (terminal.Car is null)
                                {
                                    return;
                                }
                                var distance = gpsPositionRecord.Point!.ProjectTo(2855)
                                    .Distance(terminal.Car?.ElectronicFence!.Border!.ProjectTo(2855)).ShapeDistance();
                                // 超出围栏计算
                                var fence = await _generalRepository.GetQueryable<SystemConfig>()
                                    .FilterDeleted()
                                    .FilterDisabled()
                                    .OrderByDescending(x => x.CreateDate)
                                    .Where(x => x.EnableMark)
                                    .FirstOrDefaultAsync(cancellationToken: cancellationToken) ?? new SystemConfig();

                                if (distance > fence.BeyondFenceDistance)
                                {
                                    if (!terminal.AlarmRecords!.Any())
                                    {
                                        await Task.Run(async () =>
                                        {
                                            var alarm = new AlarmRecord()
                                            {
                                                TerminalId = terminal.Id,
                                                CarId = terminal.CarId,
                                                ElectronFenceId = terminal.Car.ElectronicFenceId
                                            };
                                            alarm.Create();
                                            await _generalRepository.InsertAsync(alarm);
                                            await _generalRepository.SaveAsync();
                                        }, cancellationToken);
                                    }

                                    foreach (var client in _clientSessionManager.Sessions)
                                    {
                                        await client.Value.SendAsync(new ClientPackage()
                                        {
                                            PackageType = PackageType.Alarm,
                                            Data = new
                                            {
                                                Msg = $"{terminal.Car!.LicensePlateNumber}超出围栏{distance}米"
                                            }
                                        }.ToJson());
                                    }
                                }
                                else
                                {
                                    if (terminal.AlarmRecords!.Any())
                                    {
                                        await Task.Run(async () =>
                                        {
                                            foreach (var item in terminal.AlarmRecords)
                                            {
                                                item.IsReturn = true;
                                                await _generalRepository.UpdateAsync(item);
                                            }

                                            await _generalRepository.SaveAsync();
                                        }, cancellationToken);
                                    }
                                }
                            }, cancellationToken);

                            await Task.Run(async () =>
                            {
                                var gdPoint = new Point((double)gpsPositionRecord.Lon, (double)gpsPositionRecord.Lat)
                                    .Transform_WGS84_To_GCJ02();

                                foreach (var client in _clientSessionManager.Sessions)
                                {
                                    await client.Value.SendAsync(new ClientPackage()
                                    {
                                        PackageType = PackageType.Gps,
                                        Data = new
                                        {
                                            TerminalId = s["TerminalId"].ToString(),
                                            Lon = gdPoint.X,
                                            Lat = gdPoint.Y,
                                            GdPoint = gdPoint,
                                            Heading = gpsPositionRecord.Heading,
                                            Speed = gpsPositionRecord.Speed
                                        }
                                    }.ToJson());
                                }
                            }, cancellationToken);
                        }

                        var handler = new EV26MsgIdTcpCustomHandler(_provider, new NullLoggerFactory(), _gpsTrackerSessionManager, s as IAppSession);

                        var receivePacket = handler.HandlerDict[p.Header.MsgId](new EV26Request(p, p.OriginalPackage));

                        if (receivePacket is not null)
                        {
                            var data = _nbazhGpsSerializer.Serialize(receivePacket.Package,
                                receivePacket.Package.PackageType);
                            await s.SendAsync(data);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        throw;
                    }
                })
                .UseInProcSessionContainer()
                .BuildAsServer();

            await host.StartAsync();

            await Task.CompletedTask;
        }

        /// <summary>
        /// </summary>
        /// <param name="cancellationToken"> </param>
        /// <returns> </returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _gpsTrackerSessionManager.TryRemoveAll();
            }
            catch
            {
                // ignored
            }

            await Task.CompletedTask;
        }
    }
}