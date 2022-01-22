using Microsoft.EntityFrameworkCore;
using PM.CloudPlatform.ForkliftManager.Apis.Data;
using PM.CloudPlatform.ForkliftManager.Apis.DtoParameters.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using PM.CloudPlatform.ForkliftManager.Apis.Extensions;

namespace PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"> </typeparam>
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private readonly ForkliftManagerDbContext _dbContext;

        /// <summary>
        /// </summary>
        public DbSet<T> DbSet { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="dbContext"> </param>
        public RepositoryBase(ForkliftManagerDbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<T>();
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"> </param>
        public void AddEntity(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentException(nameof(entity));
            }

            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            DbSet.Add(entity);
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"> </param>
        /// <returns> </returns>
        public async Task AddEntityAsync(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentException(nameof(entity));
            }

            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            if (await ExistAsync(x => x.Name!.Equals(entity.Name)))
            {
                throw new ArgumentException("名称不能重复");
            }

            await DbSet.AddAsync(entity);
        }

        /// <summary>
        /// </summary>
        /// <param name="entities"> </param>
        public void AddEntities(IEnumerable<T> entities)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (var entity in entities)
            {
                AddEntity(entity);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entities"> </param>
        /// <returns> </returns>
        public async Task AddEntitiesAsync(IEnumerable<T> entities)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (var entity in entities)
            {
                await AddEntityAsync(entity);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public T GetEntityById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return DbSet.FilterDeleted().FirstOrDefault(x => x.Id == id)!;
        }

        /// <summary>
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public async Task<T> GetEntityByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await DbSet.FilterDeleted().FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// </summary>
        /// <param name="ids"> </param>
        /// <returns> </returns>
        public IEnumerable<T> GetEntitiesByIds(IEnumerable<Guid> ids)
        {
            if (ids is null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            return DbSet.Where(x => ids.Contains(x.Id)).FilterDeleted().ToList();
        }

        /// <summary>
        /// </summary>
        /// <param name="ids"> </param>
        /// <returns> </returns>
        public async Task<IEnumerable<T>> GetEntitiesByIdsAsync(IEnumerable<Guid> ids)
        {
            if (ids is null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            return await DbSet.Where(x => ids.Contains(x.Id)).FilterDeleted().ToListAsync();
        }

        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public IEnumerable<T> GetEntities()
        {
            return DbSet.FilterDeleted().ToList();
        }

        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public async Task<IEnumerable<T>> GetEntitiesAsync()
        {
            try
            {
                return await DbSet.FilterDeleted().ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="parameters"> 查询条件 </param>
        /// <returns> </returns>
        public Task<PagedList<T>> GetEntities(DtoParametersBase parameters)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var queryExpression = (DbSet as IQueryable<T>).FilterDeleted();

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

            //if (!string.IsNullOrWhiteSpace(parameters.Name))
            //{
            //    parameters.Name = parameters.Name.Trim();
            //}

            return PagedList<T>.CreateAsync(queryExpression, parameters.PageNumber, parameters.PageSize);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="parameters"> 查询条件 </param>
        /// <returns> </returns>
        public async Task<PagedList<T>> GetEntitiesAsync(DtoParametersBase parameters)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var queryExpression = (DbSet as IQueryable<T>).FilterDeleted();

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
        /// </summary>
        /// <param name="entity"> </param>
        public void UpdateEntity(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            DbSet.Update(entity);
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"> </param>
        /// <returns> </returns>
        public async Task UpdateEntityAsync(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await Task.Run(() =>
            {
                DbSet.Update(entity);
            });
        }

        /// <summary>
        /// </summary>
        /// <param name="entities"> </param>
        public void UpdateEntities(IEnumerable<T> entities)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (var entity in entities)
            {
                UpdateEntity(entity);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entities"> </param>
        /// <returns> </returns>
        public async Task UpdateEntitiesAsync(IEnumerable<T> entities)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (var entity in entities)
            {
                await UpdateEntityAsync(entity);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"> </param>
        public void DeleteEntity(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            DbSet.Remove(entity);
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"> </param>
        /// <returns> </returns>
        public async Task DeleteEntityAsync(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await Task.Run(() =>
            {
                DbSet.Remove(entity);
            });
        }

        /// <summary>
        /// </summary>
        /// <param name="id"> </param>
        public void DeleteEntityById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = GetEntityById(id);
            DeleteEntity(entity);
        }

        /// <summary>
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public async Task DeleteEntityByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await GetEntityByIdAsync(id);
            await DeleteEntityAsync(entity);
        }

        /// <summary>
        /// </summary>
        /// <param name="entities"> </param>
        public void DeleteEntities(IEnumerable<T> entities)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (var entity in entities)
            {
                DeleteEntity(entity);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entities"> </param>
        /// <returns> </returns>
        public async Task DeleteEntitiesAsync(IEnumerable<T> entities)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (var entity in entities)
            {
                await DeleteEntityAsync(entity);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="ids"> </param>
        public void DeleteEntitiesByIds(IEnumerable<Guid> ids)
        {
            if (ids is null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            foreach (var id in ids)
            {
                DeleteEntityById(id);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="ids"> </param>
        /// <returns> </returns>
        public async Task DeleteEntitiesByIdsAsync(IEnumerable<Guid> ids)
        {
            if (ids is null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            foreach (var id in ids)
            {
                await DeleteEntityByIdAsync(id);
            }
        }

        /// <summary>
        /// 虚拟删除
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public async Task DeleteEntityVirtualAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await GetEntityByIdAsync(id);
            entity.EnableMark = false;
            entity.DeleteMark = true;
            entity.Modify();

            await UpdateEntityAsync(entity);
        }

        /// <summary>
        /// 虚拟删除
        /// </summary>
        /// <param name="entity"> </param>
        /// <returns> </returns>
        public async Task DeleteEntityVirtualAsync(T entity)
        {
            await DeleteEntityVirtualAsync(entity.Id);
        }

        /// <summary>
        /// 虚拟删除
        /// </summary>
        /// <param name="ids"> </param>
        /// <returns> </returns>
        public async Task DeleteEntityVirtualAsync(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
            {
                await DeleteEntityVirtualAsync(id);
            }
        }

        /// <summary>
        /// 虚拟删除
        /// </summary>
        /// <param name="entities"> </param>
        /// <returns> </returns>
        public async Task DeleteEntityVirtualAsync(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                await DeleteEntityVirtualAsync(item);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public bool EntityExistById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return DbSet.Any(x => x.Id == id);
        }

        /// <summary>
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public async Task<bool> EntityExistByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await DbSet.AnyAsync(x => x.Id == id);
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"> </param>
        /// <returns> </returns>
        public bool EntityExist(T entity)
        {
            return EntityExistById(entity.Id);
        }

        /// <summary>
        /// </summary>
        /// <param name="expression"> </param>
        /// <returns> </returns>
        public async Task<bool> ExistAsync(Expression<Func<T, bool>> expression)
        {
            return await DbSet.AnyAsync(expression);
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"> </param>
        /// <returns> </returns>
        public async Task<bool> EntityExistAsync(T entity)
        {
            return await EntityExistByIdAsync(entity.Id);
        }

        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() >= 0;
        }
    }
}