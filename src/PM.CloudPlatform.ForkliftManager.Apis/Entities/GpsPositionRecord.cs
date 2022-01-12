using System;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.MessageBody;
using NbazhGPS.Protocol.Models;
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
        public System.DateTime? DateTime { get; set; }

        [NotMapped]
        public GpsSatelliteInfo0X22 GpsSatelliteInfo { get; set; }

        public int GpsInfoLength => GpsSatelliteInfo.GpsInfoLength;
        public int GpsSatelliteCount => GpsSatelliteInfo.GpsSatelliteCount;

        public Decimal Lon { get; set; }

        public Decimal Lat { get; set; }

        public byte Speed { get; set; }

        [NotMapped]
        public HeadingAndStatus HeadingAndStatus { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEnums0X22.GpsLocatedFunc GpsLocatedFunc => HeadingAndStatus.GpsLocatedFunc;

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEnums0X22.IsGpsLocated IsGpsLocated => HeadingAndStatus.IsGpsLocated;

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEnums0X22.EorWLon EorWLon => HeadingAndStatus.EorWLon;

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEnums0X22.SorNLat SorNLat => HeadingAndStatus.SorNLat;

        public int Heading => HeadingAndStatus.Heading;

        public ushort MCC { get; set; }

        public byte MNC { get; set; }

        public ushort LAC { get; set; }

        public int CellId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AccState AccState { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DataReportingMode DataReportingMode { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEnums0X22.GpsRealTimeHeadIn GpsRealTimeHeadIn { get; set; }

        public uint? Mileage { get; set; }
    }
}