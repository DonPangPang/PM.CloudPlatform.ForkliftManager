using System.Collections;
using System.Collections.Generic;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Entities
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : EntityBase
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public ICollection<Role> Roles { get; set; }
    }
}