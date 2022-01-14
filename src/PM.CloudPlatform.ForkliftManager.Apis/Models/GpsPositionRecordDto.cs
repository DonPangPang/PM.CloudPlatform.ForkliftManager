using System;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Models.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    [AutoMap(typeof(GpsPositionRecord), ReverseMap = true)]
    public class GpsPositionRecordDto : DtoBase
    {
        public System.DateTime? DateTime { get; set; }

        public int GpsInfoLength { get; set; }
        public int GpsSatelliteCount { get; set; }

        public Decimal Lon { get; set; }

        public Decimal Lat { get; set; }

        public byte Speed { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEnums0X22.GpsLocatedFunc GpsLocatedFunc { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEnums0X22.IsGpsLocated IsGpsLocated { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEnums0X22.EorWLon EorWLon { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEnums0X22.SorNLat SorNLat { get; set; }

        public int Heading { get; set; }

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