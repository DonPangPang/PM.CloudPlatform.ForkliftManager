using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.MessageBody;
using NbazhGPS.Protocol.Models;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Entities
{
    /// <summary>
    /// Gps定位数据
    /// </summary>
    [AutoMap(typeof(Nbazh0X22), ReverseMap = true)]
    public class GpsPositionRecord : EntityBase
    {
        /// <summary>
        /// 终端Id
        /// </summary>
        public Guid? TerminalId { get; set; }

        /// <summary>
        /// 终端
        /// </summary>
        public Terminal? Terminal { get; set; }

        public System.DateTime? DateTime { get; set; }

        public int? GpsInfoLength { get; set; }

        public int? GpsSatelliteCount { get; set; }

        public Decimal Lon { get; set; }

        public Decimal Lat { get; set; }

        /// <summary>
        /// 坐标点
        /// </summary>
        public Point? Point { get; set; }

        public byte Speed { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEnums0X22.GpsLocatedFunc? GpsLocatedFunc { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEnums0X22.IsGpsLocated? IsGpsLocated { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEnums0X22.EorWLon EorWLon { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEnums0X22.SorNLat SorNLat { get; set; }

        public int? Heading { get; set; }

        public ushort MCC { get; set; }

        public byte MNC { get; set; }

        public ushort LAC { get; set; }

        public int CellId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AccState? AccState { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DataReportingMode? DataReportingMode { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEnums0X22.GpsRealTimeHeadIn? GpsRealTimeHeadIn { get; set; }

        public uint? Mileage { get; set; }

        public override void Create(Guid terminalId, string IMEI)
        {
            TerminalId = terminalId;
            CreateUserName = IMEI;
            base.Create();
        }
    }

    [AutoMap(typeof(Nbazh0X22), ReverseMap = true)]
    [AutoMap(typeof(GpsPositionRecord), ReverseMap = true)]
    public class GpsPositionRecordTemp
    {
        public System.DateTime? DateTime { get; set; }

        [NotMapped]
        public GpsSatelliteInfo0X22? GpsSatelliteInfo { get; set; }

        public int? GpsInfoLength
        {
            get
            {
                if (GpsSatelliteInfo is not null)
                {
                    return GpsSatelliteInfo.GpsInfoLength;
                }
                else
                {
                    return GpsInfoLength;
                }
            }
            set => GpsInfoLength = value ?? default;
        }

        public int? GpsSatelliteCount
        {
            get
            {
                if (GpsSatelliteInfo != null)
                    return GpsSatelliteInfo.GpsSatelliteCount;
                else
                    return GpsSatelliteCount;
            }

            set => GpsSatelliteCount = value ?? default;
        }

        public Decimal Lon { get; set; }

        public Decimal Lat { get; set; }

        public Point Point
        {
            get => new Point((double)Lon, (double)Lat) { SRID = 4326 };
        }

        public byte Speed { get; set; }

        [NotMapped]
        public HeadingAndStatus? HeadingAndStatus { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEnums0X22.GpsLocatedFunc? GpsLocatedFunc
        {
            get
            {
                if (HeadingAndStatus != null)
                    return HeadingAndStatus.GpsLocatedFunc;
                else
                {
                    return GpsLocatedFunc;
                }
            }

            set => GpsLocatedFunc = value ?? default;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEnums0X22.IsGpsLocated? IsGpsLocated
        {
            get
            {
                if (HeadingAndStatus != null)
                    return HeadingAndStatus.IsGpsLocated;
                else
                {
                    return IsGpsLocated;
                }
            }

            set => IsGpsLocated = value ?? default;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEnums0X22.EorWLon EorWLon
        {
            get
            {
                if (HeadingAndStatus != null)
                    return HeadingAndStatus.EorWLon;
                else
                {
                    return EorWLon;
                }
            }
            set => EorWLon = value;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEnums0X22.SorNLat SorNLat
        {
            get
            {
                if (HeadingAndStatus != null)
                    return HeadingAndStatus.SorNLat;
                else
                {
                    return SorNLat;
                }
            }

            set => SorNLat = value;
        }

        public int? Heading
        {
            get
            {
                if (HeadingAndStatus != null)
                    return HeadingAndStatus.Heading;
                else
                {
                    return Heading;
                }
            }

            set => Heading = value ?? default;
        }

        public ushort MCC { get; set; }

        public byte MNC { get; set; }

        public ushort LAC { get; set; }

        public int CellId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AccState? AccState { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DataReportingMode? DataReportingMode { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEnums0X22.GpsRealTimeHeadIn? GpsRealTimeHeadIn { get; set; }

        public uint? Mileage { get; set; }
    }
}