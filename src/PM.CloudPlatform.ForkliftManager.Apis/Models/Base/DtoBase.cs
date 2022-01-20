using System;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models.Base
{
    /// <summary>
    /// Dto基类
    /// </summary>
    public abstract class DtoBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public Guid? CreateUserId { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string? CreateUserName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyDate { get; set; }

        /// <summary>
        /// 修改人Id
        /// </summary>
        public Guid? ModifyUserId { get; set; }

        /// <summary>
        /// 修改人名称
        /// </summary>

        public string? ModifyUserName { get; set; }

        /// <summary>
        /// 启用标识
        /// </summary>
        public bool? EnableMark { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool? DeleteMark { get; set; }
    }
}