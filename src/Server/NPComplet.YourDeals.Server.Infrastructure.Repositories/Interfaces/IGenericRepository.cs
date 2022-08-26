using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.Base;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;

namespace NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseDomain
    {
        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task UpdateAsync(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task<TEntity> GetByIdAsync(object id);

        Task<long> CountAsync(Expression<Func<TEntity, bool>> filter = null);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, params string[] includes);

        Task<IEnumerable<TEntity>> GetAllAsync();


        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, string orderBy = null,
            params string[] includes);

        Task<IEnumerable<TEntity>> GetAllAsync(int skip, int size, Expression<Func<TEntity, bool>> filter = null,
            string orderBy=null, params string[] includes);

        Task<IEnumerable<RoleClaimViewModel>> GetAllClaimsAsync();
        Task<RoleClaimViewModel> GetRoleClaimById(int Id);
        Task<List<RoleClaimViewModel>> GetAllByRoleIdAsync(string roleId);
        Task<string> SaveClaimAsync(RoleClaimViewModel request,string userId);
        Task<string> DeleteRoleClaimAsync(int id, string userId);
    }
}