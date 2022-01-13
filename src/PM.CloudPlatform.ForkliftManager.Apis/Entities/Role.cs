﻿using System.Collections.Generic;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Entities
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : EntityBase
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 管理的模块
        /// </summary>
        public ICollection<Module>? Modules { get; set; }
    }
}