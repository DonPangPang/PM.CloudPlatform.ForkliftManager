#nullable enable

using System.Collections.Generic;
using AutoMapper;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Models.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    /// <summary>
    /// 角色
    /// </summary>
    [AutoMap(typeof(Role), ReverseMap = true)]
    public class RoleDto : DtoBase
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 管理的模块
        /// </summary>
        public ICollection<ModuleDto>? Modules { get; set; }
    }
}