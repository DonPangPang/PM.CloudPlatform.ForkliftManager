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

        public static async Task<IEnumerable<T>> ApplyPaged<T>(this IQueryable<T> queryable, DtoParametersBase parameters)
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
    }
}