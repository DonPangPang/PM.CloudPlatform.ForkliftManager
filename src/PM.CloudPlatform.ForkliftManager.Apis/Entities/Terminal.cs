﻿using System;
using System.Collections.Generic;
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

        public Guid? CarId { get; set; }

        public Car? Car { get; set; }

        public ICollection<GpsPositionRecord>? GpsPositionRecords { get; set; }
    }
}