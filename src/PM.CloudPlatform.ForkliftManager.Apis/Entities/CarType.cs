using System.Collections.Generic;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Entities
{
    /// <summary>
    /// 车辆类型
    /// </summary>
    public class CarType : EntityBase
    {
        public ICollection<Car>? Cars { get; set; }
    }
}