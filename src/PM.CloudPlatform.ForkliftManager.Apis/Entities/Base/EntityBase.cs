using System;
using PM.CloudPlatform.ForkliftManager.Apis.Helper;

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
        public Guid Id { get; set; }

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
        public bool EnableMark { get; set; } = true;

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool DeleteMark { get; set; } = false;

        public virtual void Create()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.Now;
            //var userInfo = LoginUserInfo.Get<User>();
            if (LoginUserInfo.Get<User>() is { } userInfo)
            {
                CreateUserId = userInfo.Id;
                CreateUserName = userInfo.Name;
            }
        }

        public virtual void Modify()
        {
            ModifyDate = DateTime.Now;
            if (LoginUserInfo.Get<User>() is { } userInfo)
            {
                ModifyUserId = userInfo.Id;
                ModifyUserName = userInfo.Name;
            }
        }
    }
}