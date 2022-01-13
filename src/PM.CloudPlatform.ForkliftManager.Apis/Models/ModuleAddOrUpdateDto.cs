using System;
using AutoMapper;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    /// <summary>
    /// 模块
    /// </summary>
    [AutoMap(typeof(Module), ReverseMap = true)]
    public class ModuleAddOrUpdateDto
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 模块Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public Guid? ParentId { get; set; }

        public ModuleDto? ParentModule { get; set; }
    }
}