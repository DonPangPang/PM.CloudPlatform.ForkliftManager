using System;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Entities
{
    /// <summary>
    /// 常用指令
    /// </summary>
    public class Commands: EntityBase
    {
        /// <summary>
        /// 指令
        /// </summary>
        /// <value></value>
        public string Command { get; set; }
        /// <summary>
        /// 指令描述
        /// </summary>
        /// <value></value>
        public string Descrption{get; set;}
        /// <summary>
        /// 结果
        /// </summary>
        /// <value></value>
        public string Result{get; set;}

        /// <summary>
        /// 命令类型
        /// </summary>
        /// <value></value>
        public CommandType CommandType{get; set;}
    }

    public enum CommandType
    {
        /// <summary>
        /// 查询指令
        /// </summary>
        Select = 0,
        /// <summary>
        /// 查询设置指令
        /// </summary>
        Set = 1
    }
}
