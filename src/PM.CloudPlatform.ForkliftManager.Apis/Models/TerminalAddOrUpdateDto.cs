using System;
using AutoMapper;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    /// <summary>
    /// 终端
    /// </summary>
    [AutoMap(typeof(Terminal), ReverseMap = true)]
    public class TerminalAddOrUpdateDto
    {
        /// <summary>
        /// IMEI
        /// </summary>
        public string IMEI { get; set; }

        public Guid? CarId { get; set; }

        public Car? Car { get; set; }
    }
}