﻿using System;

namespace PM.CloudPlatform.ForkliftManager.Apis.DtoParameters.Base
{
    /// <summary>
    /// 查询参数
    /// </summary>
    public class DtoParametersBase
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 每页最大项数
        /// </summary>
        private const int MaxPageSize = 100;

        /// <summary>
        /// 模糊搜索(Name, CreateUserName, ModifyUserName)
        /// </summary>
        public string? SearchTerm { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// </summary>
        private int _pageSize { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string? OrderBy { get; set; } = "Name";

        ///// <summary>
        ///// </summary>
        //public string? Fields { get; set; }

        /// <summary>
        /// 是否需要分页
        /// </summary>
        /// <value></value>
        public bool IsNeedPaged { get; set; } = true;

        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize
        {
            get => _pageSize > 5 ? _pageSize : 5;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}