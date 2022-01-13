using System.Collections.Generic;
using AutoMapper;
using AutoMapper.Internal;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    /// <summary>
    /// 角色
    /// </summary>
    [AutoMap(typeof(Role), ReverseMap = true)]
    public class RoleAddOrUpdateDto
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 管理的模块
        /// </summary>
        public ICollection<Module>? Modules { get; set; }
    }
}