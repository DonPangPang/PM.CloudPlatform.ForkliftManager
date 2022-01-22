using System;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Entities
{
    /// <summary>
    /// 报警记录
    /// </summary>
    public class AlarmRecord : EntityBase
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