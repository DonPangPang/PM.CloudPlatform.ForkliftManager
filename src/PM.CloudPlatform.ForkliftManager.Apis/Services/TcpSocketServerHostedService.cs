using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NbazhGPS.Protocol;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.MessageBody;
using NetTopologySuite.Geometries;
using NetTopologySuite.Index.HPRtree;
using Pang.AutoMapperMiddleware;
using PM.CloudPlatform.ForkliftManager.Apis.CorrPacket;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Extensions;
using PM.CloudPlatform.ForkliftManager.Apis.General;
using PM.CloudPlatform.ForkliftManager.Apis.Handlers;
using PM.CloudPlatform.ForkliftManager.Apis.Kafka;
using PM.CloudPlatform.ForkliftManager.Apis.Managers;
using PM.CloudPlatform.ForkliftManager.Apis.Options;
using PM.CloudPlatform.ForkliftManager.Apis.PipelineFilters;
using PM.CloudPlatform.ForkliftManager.Apis.ProtocolReqResps;
using PM.CloudPlatform.ForkliftManager.Apis.Sessions;
using SuperSocket;
using SuperSocket.Channel;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly IOptions<GpsPointFormatterOption> _gpsPointFormatterOption;
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
        /// <param name="gpsPointFormatterOption"></param>
        /// <param name="clientSessionManager">     </param>
        /// <param name="gpsTrackerSessionManager"> </param>
        /// <param name="logger">                   </param>
        /// <param name="factory">                  </param>
        public TcpSocketServerHostedService(
            IOptions<ServerOption> serverOptions,
            IOptions<KafkaOption> kafkaOptions,
            IOptions<GpsPointFormatterOption> gpsPointFormatterOption,
            ClientSessionManagers clientSessionManager,
            TerminalSessionManager gpsTrackerSessionManager,
            ILogger<TcpSocketServerHostedService> logger,
            IServiceScopeFactory factory)
        {
            _serverOptions = serverOptions ?? throw new ArgumentNullException(nameof(serverOptions));
            _kafkaOptions = kafkaOptions;
            _gpsPointFormatterOption = gpsPointFormatterOption;
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
                             var terminal = await _generalRepository.FindAsync<Terminal>(x => x.IMEI.Equals(s["TerminalId"].ToString()));

                             #region 关闭使用记录

                             // 如果使用记录不为空,则更新记录
                             var UseRecord = await _generalRepository.FindAsync<UseRecord>(x =>
                                 x.TerminalId.Equals(terminal.Id) &&
                                 x.EndTime == null);
                             if (UseRecord != null)
                             {
                                 UseRecord.EndTime = DateTime.Now;
                                 await _generalRepository.UpdateAsync(UseRecord);
                                 await _generalRepository.SaveAsync();
                             }

                             // var record = new UseRecord() { TerminalId = (Guid)terminal.Id!,
                             // StartTime = s.StartTime.DateTime, EndTime = DateTime.Now,
                             // LengthOfTime = s.StartTime.DateTime.HourDiff(DateTime.Now) };
                             // record.Create(); await _generalRepository.InsertAsync(record);

                             #endregion 关闭使用记录
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

                        // TODO: 更改UseRecord相关方法
                        /***
                         * 相关思路:
                         * 1. ACC选择 (1)心跳包 (2)定位包
                         * 2. Session和定位包作为辅助记录停止使用时间
                         * 3. 心跳包只作为关闭UseRecord的辅助记录
                        */
                        if (p.Header.MsgId.Equals(NbazhGpsMessageIds.心跳包.ToByteValue()))
                        {
                            /***
                             * 1. 检查ACC状态
                             * 2. 检查是否有未关闭的UseRecord
                             * 3. 如果有, 则关闭
                            */

                            //var UseRecord = await _generalRepository.FindAsync<UseRecord>(x =>
                            //     x.TerminalId.Equals(s["TerminalId"].ToString()) &&
                            //     x.EndTime == null);

                            var UseRecord = await _generalRepository.GetQueryable<UseRecord>()
                                .Include(x => x.Terminal)
                                .Where(x => x.Terminal != null && x.Terminal.IMEI.Equals(s["TerminalId"].ToString()) && x.EndTime == null)
                                .FirstOrDefaultAsync();

                            if (UseRecord != null)
                            {
                                UseRecord.EndTime = DateTime.Now;
                                UseRecord.LengthOfTime = UseRecord.StartTime.HourDiff(DateTime.Now);
                                await _generalRepository.UpdateAsync(UseRecord);
                            }
                            // else { var terminal = await _generalRepository.FindAsync<Terminal>(x
                            // => x.IMEI.Equals(s["TerminalId"].ToString()));

                            // var record = new UseRecord() { TerminalId = (Guid)terminal.Id!,
                            // StartTime = s.StartTime.DateTime, EndTime = DateTime.Now,
                            // LengthOfTime = s.StartTime.DateTime.HourDiff(DateTime.Now) };
                            // record.Create(); await _generalRepository.InsertAsync(record); }
                        }

                        if (p.Header.MsgId.Equals(NbazhGpsMessageIds.登陆包.ToByteValue()))
                        {
                            var terminalId = (p.Bodies as Nbazh0X01)!.TerminalId;
                            s["TerminalId"] = terminalId;
                            // 终端登录
                            var terminal = await _generalRepository.GetQueryable<Terminal>()
                                .FilterDeleted()
                                .FirstOrDefaultAsync(x => x.IMEI.Equals(terminalId));

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

                            var terminal = await _generalRepository.GetQueryable<Terminal>()
                                    .FilterDeleted()
                                    .FilterDisabled()
                                    .Include(x => x.Car)
                                    .ThenInclude(t => t.ElectronicFence)
                                    .Include(x => x.AlarmRecords.Where(t => t.IsReturn == false))
                                    .Where(x => x.IMEI.Equals(s["TerminalId"].ToString()))
                                    .FirstOrDefaultAsync();

                            #region 使用记录

                            await Task.Run(async () =>
                            {
                                var UseRecord = await _generalRepository.GetQueryable<UseRecord>()
                                .Include(x => x.Terminal)
                                .Where(x => x.Terminal != null && x.Terminal.IMEI.Equals(s["TerminalId"].ToString()) && x.EndTime == null)
                                .FirstOrDefaultAsync();

                                if (UseRecord is null && gpsPositionRecord.AccState == AccState.高)
                                {
                                    var record = new UseRecord()
                                    {
                                        TerminalId = (Guid)terminal.Id!,
                                        StartTime = s.StartTime.DateTime,
                                    };
                                    record.Create();
                                    await _generalRepository.InsertAsync(record);
                                }

                                if (UseRecord is not null && gpsPositionRecord.AccState == AccState.低)
                                {
                                    UseRecord.EndTime = DateTime.Now;
                                    UseRecord.LengthOfTime = UseRecord.StartTime.HourDiff(DateTime.Now);
                                    await _generalRepository.UpdateAsync(UseRecord);
                                    await _generalRepository.SaveAsync();
                                }
                            });

                            #endregion 使用记录

                            gpsPositionRecord.Create(terminal.Id, s["TerminalId"].ToString());

                            #region GPS转换坐标系

                            var tempPoint = gpsPositionRecord.Point;

                            gpsPositionRecord.Point = gpsPositionRecord.Point.Transform_WGS84_To_GCJ02();
                            gpsPositionRecord.Lat = (decimal)gpsPositionRecord.Point.X;
                            gpsPositionRecord.Lon = (decimal)gpsPositionRecord.Point.Y;

                            #endregion GPS转换坐标系

                            //await _generalRepository.InsertAsync<GpsPositionRecord>(gpsPositionRecord);
                            //await _generalRepository.SaveAsync();

                            // 记录定位, 超出围栏计算
                            await Task.Run(async () =>
                            {
                                try
                                {
                                    if (terminal.Car is null || terminal.Car.ElectronicFence is null)
                                    {
                                        return;
                                    }
                                    // var distance = gpsPositionRecord.Point!.ProjectTo(2855) .Distance(terminal.Car?.ElectronicFence!.Border!.ProjectTo(2855)).ShapeDistance();
                                    var border = terminal.Car?.ElectronicFence!.LngLats.ToGeometry<Polygon>();
                                    var point = new Point((double)gpsPositionRecord.Lon, (double)gpsPositionRecord.Lat)
                                    .Transform_WGS84_To_GCJ02();
                                    var distance = point
                                        .ProjectTo(2855)
                                        .Distance(border.ProjectTo(2855))
                                        .ShapeDistance();
                                    // 超出围栏计算
                                    var fence = await _generalRepository.GetQueryable<SystemConfig>()
                                        .FilterDeleted()
                                        .FilterDisabled()
                                        .OrderByDescending(x => x.CreateDate)
                                        .FirstOrDefaultAsync() ?? new SystemConfig();

                                    if (distance > fence.BeyondFenceDistance)
                                    {
                                        Console.WriteLine(@$"--------------{Environment.NewLine}距离:{distance};阈值:{fence.BeyondFenceDistance};围栏编号:{terminal.Car.ElectronicFence.Id};{Environment.NewLine}--------------");

                                        if (terminal.AlarmRecords is null || !terminal.AlarmRecords!.Any())
                                        {
                                            await Task.Run(async () =>
                                            {
                                                var alarm = new AlarmRecord()
                                                {
                                                    TerminalId = terminal.Id,
                                                    CarId = terminal.CarId,
                                                    IMEI = terminal.IMEI,
                                                    ElectronFenceId = terminal.Car.ElectronicFenceId
                                                };
                                                alarm.Create();
                                                await _generalRepository.InsertAsync(alarm);
                                                await _generalRepository.SaveAsync();
                                                _generalRepository.Context.Entry(alarm).State = EntityState.Detached;
                                                _generalRepository.Context.Entry(terminal).State = EntityState.Detached;
                                            });
                                        }

                                        foreach (var client in _clientSessionManager.Sessions)
                                        {
                                            var msg = new ClientPackage()
                                            {
                                                PackageType = PackageType.Alarm,
                                                Data = new
                                                {
                                                    // 终端Id
                                                    TerminalId = s["TerminalId"].ToString(),
                                                    // 车牌号
                                                    LicensePlateNumber = terminal.Car!.LicensePlateNumber,
                                                    // 超出距离
                                                    Distance = distance,
                                                    Fence = fence.BeyondFenceDistance,
                                                    // 提示信息
                                                    Msg = $"{terminal.Car!.LicensePlateNumber}超出围栏{distance.ToString("0.00")}米"
                                                }
                                            }.ToJson();
                                            await client.Value.SendAsync(msg);
                                        }
                                    }
                                    else
                                    {
                                        if (terminal.AlarmRecords != null && terminal.AlarmRecords!.Any())
                                        {
                                            await Task.Run(async () =>
                                            {
                                                foreach (var item in terminal.AlarmRecords)
                                                {
                                                    item.IsReturn = true;
                                                    await _generalRepository.UpdateAsync(item);
                                                    await _generalRepository.SaveAsync();
                                                    _generalRepository.Context.Entry(item).State = EntityState.Detached;
                                                    _generalRepository.Context.Entry(terminal).State = EntityState.Detached;
                                                }
                                            });
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"distance 无法计算, {ex.Message}");
                                    Console.WriteLine($"{ex}");
                                }
                            });

                            // 向客户端发送定位
                            await Task.Run(async () =>
                            {
                                var gdPoint = new Point((double)gpsPositionRecord.Lon, (double)gpsPositionRecord.Lat)
                                    .Transform_WGS84_To_GCJ02();

                                gpsPositionRecord.Point = gdPoint;
                                gpsPositionRecord.Lon = (decimal)gdPoint.X;
                                gpsPositionRecord.Lat = (decimal)gdPoint.Y;
                                gpsPositionRecord.ModifyUserName = gdPoint.ToGeoJson();

                                /*
                                    查看当前坐标和上一个坐标差距是否过大
                                    
                                    查看过去五分钟是否存在历史坐标点, 如果不存在, 直接插入, 
                                    如果存在, 对比, 过去五分钟内的最后一个坐标点和当前坐标点的距离是否在误差内
                                 */
                                await Task.Run(async () =>
                                {
                                    var thisPointDateFormat = gpsPositionRecord.DateTime.Value.AddSeconds(-(_gpsPointFormatterOption.Value.TimeInterval));
                                    var elderGpsPoint = await _generalRepository.GetQueryable<GpsPositionRecord>()
                                        .Where(x => x.CreateUserName.Equals(gpsPositionRecord.CreateUserName))
                                        .Where(x => x.DateTime > thisPointDateFormat)
                                        .OrderByDescending(x => x.DateTime).FirstOrDefaultAsync();

                                    if (elderGpsPoint is not null)
                                    {
                                        var distance = gdPoint
                                           .ProjectTo(2855)
                                           .Distance(elderGpsPoint.Point)
                                           .ShapeDistance();

                                        var timeDifference = gpsPositionRecord.DateTime.MinuteDiff(elderGpsPoint.DateTime);
                                        var thisTimeSpeed = distance / (double)timeDifference;
                                        var formatSpeed = _gpsPointFormatterOption.Value.Speed.To_m_s(_gpsPointFormatterOption.Value.Coefficient);
                                        //当前点时速小于纠正时速
                                        if (thisTimeSpeed < formatSpeed)
                                        {
                                            await _generalRepository.InsertAsync<GpsPositionRecord>(gpsPositionRecord);
                                            await _generalRepository.SaveAsync();
                                        }
                                        else
                                        {
                                            // 大于则丢弃
                                            gpsPositionRecord.DeleteMark = true;
                                            gpsPositionRecord.EnableMark = false;
                                            await _generalRepository.InsertAsync<GpsPositionRecord>(gpsPositionRecord);
                                            await _generalRepository.SaveAsync();
                                        }
                                    }
                                    else
                                    {
                                        await _generalRepository.InsertAsync<GpsPositionRecord>(gpsPositionRecord);
                                        await _generalRepository.SaveAsync();
                                    }
                                });

                                //车辆追踪
                                if (_clientSessionManager.IsTrace)
                                {
                                    if (s["TerminalId"].ToString().Equals(_clientSessionManager.TraceTerminalId))
                                    {
                                        foreach (var client in _clientSessionManager.Sessions)
                                        {
                                            var msg = new ClientPackage()
                                            {
                                                PackageType = PackageType.Gps,
                                                Data = new
                                                {
                                                    // 终端Id
                                                    TerminalId = s["TerminalId"].ToString(),
                                                    // 车牌号
                                                    LicensePlateNumber = terminal.Car!.LicensePlateNumber,
                                                    // 纬度
                                                    Lon = gdPoint.X,
                                                    // 经度
                                                    Lat = gdPoint.Y,
                                                    // 高德经纬度对象
                                                    GdPoint = gdPoint.ToGeoJson(),
                                                    // 方向
                                                    Heading = gpsPositionRecord.Heading,
                                                    // 速度
                                                    Speed = gpsPositionRecord.Speed
                                                }
                                            }.ToJson();
                                            await client.Value.SendAsync(msg);
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (var client in _clientSessionManager.Sessions)
                                    {
                                        var msg = new ClientPackage()
                                        {
                                            PackageType = PackageType.Gps,
                                            Data = new
                                            {
                                                // 终端Id
                                                TerminalId = s["TerminalId"].ToString(),
                                                // 车牌号
                                                LicensePlateNumber = terminal.Car!.LicensePlateNumber,
                                                // 纬度
                                                Lon = gdPoint.X,
                                                // 经度
                                                Lat = gdPoint.Y,
                                                // 高德经纬度对象
                                                GdPoint = gdPoint.ToGeoJson(),
                                                // 方向
                                                Heading = gpsPositionRecord.Heading,
                                                // 速度
                                                Speed = gpsPositionRecord.Speed
                                            }
                                        }.ToJson();
                                        await client.Value.SendAsync(msg);
                                    }
                                }
                            });
                        }

                        // 应答器
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
