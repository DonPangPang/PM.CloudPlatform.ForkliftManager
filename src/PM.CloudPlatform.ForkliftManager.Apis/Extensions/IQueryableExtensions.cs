using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using PM.CloudPlatform.ForkliftManager.Apis.DtoParameters.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Helper;

namespace PM.CloudPlatform.ForkliftManager.Apis.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IQueryableExtensions
    {
        /// <summary>
        /// 进行排序
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="source">  </param>
        /// <param name="orderBy"> </param>
        /// <returns> </returns>
        public static IQueryable<T> ApplySort<T>(
            this IQueryable<T> source, string orderBy)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return source;
            }

            var orderByAfterSplit = orderBy.Split(",");

            foreach (var orderByClause in orderByAfterSplit.Reverse())
            {
                var trimedOrderByClause = orderByClause.Trim();

                var orderDescending = trimedOrderByClause.EndsWith(" desc");

                var indexOfFirstSpace = trimedOrderByClause.IndexOf(" ", StringComparison.Ordinal);

                var propertyName = indexOfFirstSpace == -1
                    ? trimedOrderByClause
                    : trimedOrderByClause.Remove(indexOfFirstSpace);

                source = source.OrderBy(propertyName
                                        + (orderDescending ? " descending" : " ascending"));
            }

            return source;
        }

        /// <summary>
        /// 进行分页以及排序, 模糊查询等
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="queryable">      </param>
        /// <param name="parameters">     </param>
        /// <param name="isAsIQueryable"> </param>
        /// <returns> </returns>
        public static async Task<IEnumerable<T>> ToPagedAsync<T>(this IQueryable<T> queryable, DtoParametersBase parameters, bool isAsIQueryable = false)
            where T : EntityBase
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var queryExpression = queryable;

            if (parameters.StartTime is not null && parameters.EndTime is not null)
            {
                queryExpression = queryExpression.Where(x =>
                    x.CreateDate >= parameters.StartTime && x.CreateDate <= parameters.EndTime);
            }

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                parameters.SearchTerm = parameters.SearchTerm.Trim();

                queryExpression = queryExpression.Where(x =>
                    x.Name!.Contains(parameters.SearchTerm) ||
                    x.CreateUserName!.Contains(parameters.SearchTerm) ||
                    x.ModifyUserName!.Contains(parameters.SearchTerm));
            }

            if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
            {
                queryExpression = queryExpression.ApplySort(parameters.OrderBy);
            }

            return await PagedList<T>.CreateAsync(queryExpression, parameters.PageNumber, parameters.PageSize);
        }

        /// <summary>
        /// 进行分页以及排序, 模糊查询等
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="queryable">      </param>
        /// <param name="parameters">     </param>
        /// <param name="isAsIQueryable"> </param>
        /// <returns> </returns>
        public static IQueryable<T> ApplyPaged<T>(this IQueryable<T> queryable, DtoParametersBase parameters, bool isAsIQueryable = false)
            where T : EntityBase
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var queryExpression = queryable;

            if (parameters.StartTime is not null && parameters.EndTime is not null)
            {
                queryExpression = queryExpression.Where(x =>
                    x.CreateDate >= parameters.StartTime && x.CreateDate <= parameters.EndTime);
            }

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                parameters.SearchTerm = parameters.SearchTerm.Trim();

                queryExpression = queryExpression.Where(x =>
                    x.Name!.Contains(parameters.SearchTerm) ||
                    x.CreateUserName!.Contains(parameters.SearchTerm) ||
                    x.ModifyUserName!.Contains(parameters.SearchTerm));
            }

            if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
            {
                queryExpression = queryExpression.ApplySort(parameters.OrderBy);
            }

            return PagedList<T>.ApplyPaged(queryExpression, parameters.PageNumber, parameters.PageSize);
        }

        /// <summary>
        /// 过滤掉已删除的数据
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="queryable"> </param>
        /// <returns> </returns>
        public static IQueryable<T> FilterDeleted<T>(this IQueryable<T> queryable) where T : EntityBase
        {
            return queryable.Where(x => !x.DeleteMark);
        }

        /// <summary>
        /// 过滤掉未删除的数据
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="queryable"> </param>
        /// <returns> </returns>
        public static IQueryable<T> FilterAlive<T>(this IQueryable<T> queryable) where T : EntityBase
        {
            return queryable.Where(x => x.DeleteMark);
        }

        /// <summary>
        /// 过滤启用的数据
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="queryable"> </param>
        /// <returns> </returns>
        public static IQueryable<T> FilterEnabled<T>(this IQueryable<T> queryable) where T : EntityBase
        {
            return queryable.Where(x => !x.EnableMark);
        }

        /// <summary>
        /// 过滤未启用的数据
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="queryable"> </param>
        /// <returns> </returns>
        public static IQueryable<T> FilterDisabled<T>(this IQueryable<T> queryable) where T : EntityBase
        {
            return queryable.Where(x => x.EnableMark);
        }
    }
}