using AutoMapper;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    /// <summary>
    /// 用户添加/更新
    /// </summary>
    [AutoMap(typeof(User), ReverseMap = true)]
    public class UserAddOrUpdateDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; } = null!;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Tel { get; set; } = null!;

        /// <summary>
        /// 角色
        /// </summary>
        public ICollection<Role>? Roles { get; set; } = new List<Role>();
    }
}