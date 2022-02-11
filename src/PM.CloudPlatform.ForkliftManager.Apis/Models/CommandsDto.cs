using System;
using AutoMapper;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Models.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Models
{
    [AutoMap(typeof(Commands), ReverseMap =true)]
    public class CommandsDto: DtoBase
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
}
