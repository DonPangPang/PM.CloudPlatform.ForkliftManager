using AutoMapper;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Models.Base;
using System;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    [AutoMap(typeof(AlarmRecord), ReverseMap = true)]
    public class AlarmRecordDto: DtoBase
    {
        /// <summary>
        /// 终端Id
        /// </summary>
        public Guid? TerminalId { get; set; }

        /// <summary>
        /// 终端
        /// </summary>
        public Terminal Terminal { get; set; }

        /// <summary>
        /// IMEI
        /// </summary>
        public string IMEI { get; set; }

        /// <summary>
        /// 车辆Id
        /// </summary>
        public Guid? CarId { get; set; }

        /// <summary>
        /// 车辆
        /// </summary>
        public Car? Car { get; set; }

        /// <summary>
        /// 电子围栏Id
        /// </summary>
        public Guid? ElectronFenceId { get; set; }

        /// <summary>
        /// 电子围栏
        /// </summary>
        public ElectronicFence? ElectronicFence { get; set; }

        /// <summary>
        /// 是否回归围栏
        /// </summary>
        public bool IsReturn { get; set; } = false;
    }
}
