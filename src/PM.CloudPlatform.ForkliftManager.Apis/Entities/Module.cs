using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Entities
{
    /// <summary>
    /// 模块管理
    /// </summary>
    public class Module : EntityBase
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 模块Url
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 父模块
        /// </summary>
        public Module? ParentModule { get; set; }

        /// <summary>
        /// 对应的角色
        /// </summary>
        public ICollection<Role>? Roles { get; set; }
    }
}