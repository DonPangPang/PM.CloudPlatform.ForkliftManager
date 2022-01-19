using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Entities
{
    /// <summary>
    /// 终端
    /// </summary>
    public class Terminal : EntityBase
    {
        /// <summary>
        /// IMEI
        /// </summary>
        public string IMEI { get; set; } = null!;

        /// <summary>
        /// 绑定车辆Id
        /// </summary>
        public Guid? CarId { get; set; }

        /// <summary>
        /// 绑定车辆
        /// </summary>
        public Car? Car { get; set; }

        /// <summary>
        /// 终端绑定记录
        /// </summary>
        public ICollection<TerminalBindRecord>? TerminalBindRecords { get; set; }

        /// <summary>
        /// 终端GPS定位数据
        /// </summary>
        public ICollection<GpsPositionRecord>? GpsPositionRecords { get; set; }
    }
}