using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PM.CloudPlatform.ForkliftManager.Apis.Data;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.General
{
    public class GeneralRepository : IGeneralRepository
    {
        private readonly ForkliftManagerDbContext _forkliftManagerDbContext;

        public GeneralRepository(ForkliftManagerDbContext forkliftManagerDbContext)
        {
            _forkliftManagerDbContext = forkliftManagerDbContext;
        }

        public DbSet<T> GetDbSet<T>() where T : EntityBase
        {
            return _forkliftManagerDbContext.Set<T>();
        }

        public IQueryable<T> GetQueryable<T>() where T : EntityBase
        {
            return _forkliftManagerDbContext.Set<T>().AsQueryable();
        }

        public async Task InsertAsync<T>(T entity) where T : EntityBase
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            await _forkliftManagerDbContext.Set<T>().AddAsync(entity);
        }

        public async Task InsertAsync<T>(IEnumerable<T> entities) where T : EntityBase
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            await _forkliftManagerDbContext.Set<T>().AddRangeAsync(entities);
        }

        public async Task DeleteAsync<T>(T entity) where T : EntityBase
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            await Task.Run(() =>
            {
                _forkliftManagerDbContext.Set<T>().Remove(entity);
            });
        }

        public async Task DeleteAsync<T>(Guid id) where T : EntityBase
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var entity = await FindAsync<T>(id);

            await Task.Run(() =>
            {
                _forkliftManagerDbContext.Remove(entity);
            });
        }

        public async Task DeleteAsync<T>(IEnumerable<T> entities) where T : EntityBase
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            await Task.Run(() =>
            {
                _forkliftManagerDbContext.Set<T>().RemoveRange(entities);
            });
        }

        public async Task DeleteAsync<T>(IEnumerable<Guid> ids) where T : EntityBase
        {
            if (ids is null)
                throw new ArgumentNullException(nameof(ids));

            var entities = await FindAsync<T>(ids);

            await Task.Run(() =>
            {
                _forkliftManagerDbContext.Set<T>().RemoveRange(entities);
            });
        }

        public async Task UpdateAsync<T>(T entity) where T : EntityBase
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            await Task.Run(() =>
            {
                _forkliftManagerDbContext.Set<T>().Update(entity);
            });
        }

        public async Task UpdateAsync<T>(IEnumerable<T> entities) where T : EntityBase
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            await Task.Run(() =>
            {
                _forkliftManagerDbContext.Set<T>().UpdateRange(entities);
            });
        }

        public async Task<T> FindAsync<T>(Guid id) where T : EntityBase
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            return await _forkliftManagerDbContext.Set<T>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<T> FindAsync<T>(Expression<Func<T, bool>> expression) where T : EntityBase
        {
            return await _forkliftManagerDbContext.Set<T>().Where(expression).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FindAsync<T>() where T : EntityBase
        {
            return await _forkliftManagerDbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync<T>(IEnumerable<Guid> ids) where T : EntityBase
        {
            if (ids is null)
                throw new ArgumentNullException(nameof(ids));

            return await _forkliftManagerDbContext.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<bool> ExistAsync<T>(T entity) where T : EntityBase
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            return await _forkliftManagerDbContext.Set<T>().AnyAsync();
        }

        public async Task<bool> ExistAsync<T>(Expression<Func<T, bool>> expression) where T : EntityBase
        {
            return await _forkliftManagerDbContext.Set<T>().AnyAsync(expression);
        }

        public async Task<bool> SaveAsync()
        {
            return await _forkliftManagerDbContext.SaveChangesAsync() >= 0;
        }
    }
}