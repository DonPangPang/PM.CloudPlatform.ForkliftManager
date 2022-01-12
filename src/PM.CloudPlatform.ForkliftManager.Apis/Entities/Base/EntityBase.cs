﻿using System;

namespace PM.CloudPlatform.ForkliftManager.Apis.Entities.Base
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public Guid CreateUserId { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateUserName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyDate { get; set; }

        /// <summary>
        /// 修改人Id
        /// </summary>
        public Guid ModifyUserId { get; set; }

        /// <summary>
        /// 修改人名称
        /// </summary>

        public string ModifyUserName { get; set; }

        public virtual void Create()
        {
            CreateDate = DateTime.Now;
        }

        public virtual void Modify()
        {
            ModifyDate = DateTime.Now;
        }
    }
}