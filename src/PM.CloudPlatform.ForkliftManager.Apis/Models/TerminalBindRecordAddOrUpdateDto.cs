using AutoMapper;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Models.Base;
using System;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    [AutoMap(typeof(TerminalBindRecord), ReverseMap = true)]
    public class TerminalBindRecordAddOrUpdateDto
    {
        /// <summary>
        /// 终端Id
        /// </summary>
        public Guid? TerminalId { get; set; }

        /// <summary>
        /// 车辆Id
        /// </summary>
        public Guid? CarId { get; set; }

        public string IMEI { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// </summary>
        public Terminal? Terminal { get; set; }

        /// <summary>
        /// </summary>
        public Car? Car { get; set; }

        /// <summary>
        /// 是否存在
        /// </summary>
        public bool? IsActive { get; set; }
    }
}
