using Microsoft.AspNetCore.Identity;
using NPComplet.YourDeals.Domain.Shared.Audit;
using NPComplet.YourDeals.Domain.Shared.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces
{
    public interface  IGenericRepositoryAudit<TEntity> where TEntity : Audit
    {
        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task<TEntity> GetByIdAsync(object id);

        Task<long> CountAsync(Expression<Func<TEntity, bool>> filter = null);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, params string[] includes);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, string orderBy = null,
            params string[] includes);

        Task<IEnumerable<TEntity>> GetAllAsync(int skip, int size, Expression<Func<TEntity, bool>> filter = null,
            string orderBy = null, params string[] includes);
    }
}
