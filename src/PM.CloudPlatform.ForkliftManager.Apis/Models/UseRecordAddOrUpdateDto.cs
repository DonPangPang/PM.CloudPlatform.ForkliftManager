﻿using System;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    /// <summary>
    /// 使用记录
    /// </summary>
    public class UseRecordAddOrUpdateDto
    {
        /// <summary>
        /// 车辆
        /// </summary>
        public CarDto Car { get; set; }

        /// <summary>
        /// 开始使用时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束使用时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 使用时长
        /// </summary>
        public int LengthOfTime { get; set; }
    }
}