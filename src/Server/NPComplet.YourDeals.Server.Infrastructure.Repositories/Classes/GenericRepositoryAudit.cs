using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NPComplet.YourDeals.Domain.Shared.Audit;
using NPComplet.YourDeals.Domain.Shared.DataContracts;
using NPComplet.YourDeals.Server.Infrastructure.Repositories.Data;
using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Server.Infrastructure.Repositories.Classes
{
    public class GenericRepositoryAudit<TEntity> : IGenericRepositoryAudit<TEntity> where TEntity : Audit
    {
        private readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public GenericRepositoryAudit(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Task AddAsync(TEntity entity)
        {
           
            return _dbContext.Set<TEntity>().AddAsync(entity).AsTask();
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            

            return _dbContext.Set<TEntity>().AddRangeAsync(entities);
        }

        public Task UpdateAsync(TEntity entity)
        {
          

            if (_dbContext.Entry(entity).State == EntityState.Detached) AddAsync(entity);

            _dbContext.Entry(entity).State = EntityState.Modified;

            return Task.FromResult(entity);
        }

        public Task DeleteAsync(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached) AddAsync(entity);

            _dbContext.Entry(entity).State = EntityState.Deleted;
            _dbContext.Set<TEntity>().Remove(entity);

            return Task.FromResult(entity);
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            var query = _dbContext.Set<TEntity>().AsNoTracking().AsQueryable();

            if (filter != null)
                query = query.Where(filter);
            return await query.LongCountAsync().ConfigureAwait(false);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null,
            params string[] includes)
        {
            var query = _dbContext.Set<TEntity>().AsNoTracking().AsQueryable();

            foreach (var include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);
            return await query.SingleOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null,
            string orderBy = null, params string[] includes)
        {
            var query = _dbContext.Set<TEntity>().AsNoTracking().AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query.OrderBy(orderBy);

            return await query.ToArrayAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int skip, int size,
            Expression<Func<TEntity, bool>> filter = null, string orderBy = null, params string[] includes)
        {
            var query = _dbContext.Set<TEntity>().AsNoTracking().AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }


            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query.OrderBy(orderBy);

            query = query.Skip(skip).Take(size);

            return await query.ToArrayAsync().ConfigureAwait(false);
        }
    }
}
