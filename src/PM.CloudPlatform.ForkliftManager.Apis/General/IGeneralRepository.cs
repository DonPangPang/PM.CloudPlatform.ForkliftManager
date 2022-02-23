using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.General
{
    public interface IGeneralRepository
    {
        DatabaseFacade Database { get; }
        DbSet<T> GetDbSet<T>() where T : EntityBase;

        IQueryable<T> GetQueryable<T>() where T : EntityBase;

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task InsertAsync<T>(T entity) where T : EntityBase;

        Task InsertAsync<T>(IEnumerable<T> entities) where T : EntityBase;

        Task DeleteAsync<T>(T entity) where T : EntityBase;

        Task DeleteAsync<T>(Guid id) where T : EntityBase;

        Task DeleteAsync<T>(IEnumerable<T> entities) where T : EntityBase;

        Task DeleteAsync<T>(IEnumerable<Guid> ids) where T : EntityBase;

        Task UpdateAsync<T>(T entity) where T : EntityBase;

        Task UpdateAsync<T>(IEnumerable<T> entities) where T : EntityBase;

        Task<T> FindAsync<T>(Guid id) where T : EntityBase;

        Task<T> FindAsync<T>(Expression<Func<T, bool>> expression) where T : EntityBase;

        Task<IEnumerable<T>> FindAsync<T>() where T : EntityBase;

        Task<IEnumerable<T>> FindAsync<T>(IEnumerable<Guid> ids) where T : EntityBase;

        Task<bool> ExistAsync<T>(T entity) where T : EntityBase;

        Task<bool> ExistAsync<T>(Expression<Func<T, bool>> expression) where T : EntityBase;

        Task<bool> SaveAsync();
    }
}