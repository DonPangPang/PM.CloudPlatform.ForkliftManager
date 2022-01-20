using System.Collections.Generic;

namespace PM.CloudPlatform.ForkliftManager.Apis.Config
{
    /// <summary>
    /// 数据库设置集合
    /// </summary>
    public class DbSettings
    {
        /// <summary>
        /// </summary>
        public List<DbSetting> Settings { get; set; }
    }

    /// <summary>
    /// 数据库设置
    /// </summary>
    public class DbSetting
    {
        /// <summary>
        /// 数据库名
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 是否使用该数据库
        /// </summary>
        public bool IsEnable { get; set; } = false;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; } = null!;
    }
}