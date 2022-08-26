// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericRepository.cs" company="NPComplet">
//   Org
// </copyright>
// <summary>
//   Defines the GenericRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;

namespace NPComplet.YourDeals.Server.Infrastructure.Repositories.Classes
{
    using Data;
    using Domain.Shared.Base;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// The generic repository.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseDomain
    {
        private readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Task AddAsync(TEntity entity)
        {
            entity.InternalCreationDateTimeUtc = DateTime.UtcNow;
            entity.InternalModificationDateTimeUtc = DateTime.UtcNow;
            entity.InternalValidation = true;
            return _dbContext.Set<TEntity>().AddAsync(entity).AsTask();
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.InternalCreationDateTimeUtc = DateTime.UtcNow;
                entity.InternalModificationDateTimeUtc = DateTime.UtcNow;
                entity.InternalValidation = true;
            }

            return _dbContext.Set<TEntity>().AddRangeAsync(entities);
        }

        public Task UpdateAsync(TEntity entity)
        {
            entity.InternalModificationDateTimeUtc = DateTime.UtcNow;

            if (_dbContext.Entry(entity).State == EntityState.Detached) 
                AddAsync(entity);

            _dbContext.Entry(entity).State = EntityState.Modified;

            return Task.FromResult(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.InternalModificationDateTimeUtc = DateTime.UtcNow;
            }

            _dbContext.Set<TEntity>().UpdateRange(entities);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
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
            {
                query = query.Where(p => p.InternalIsDelete != true);
                query = query.Where(filter);
            }
            return await query.LongCountAsync().ConfigureAwait(false);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null,
            params string[] includes)
        {
            var query = _dbContext.Set<TEntity>().AsNoTracking().AsQueryable();

            foreach (var include in includes)
                query = query.Include(include);

            if (filter != null)
            {
                query = query.Where(p => p.InternalIsDelete != true);
                query = query.Where(filter);
            }
            return await query.SingleOrDefaultAsync().ConfigureAwait(false);
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().Where(p=>p.InternalIsDelete!=true).ToListAsync().ConfigureAwait(false);
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
            {
                query = query.Where(p => p.InternalIsDelete != true);
                query = query.Where(filter);
            }
            if (orderBy != null)
                query.OrderBy(orderBy);

            return await query.ToArrayAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int skip, int size,
            Expression<Func<TEntity, bool>> filter = null, string orderBy=null ,params string[] includes)
        {
            var query = _dbContext.Set<TEntity>().AsNoTracking().AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }


            if (filter != null)
            {
                query = query.Where(p => p.InternalIsDelete != true);
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(orderBy))
                query.OrderBy(orderBy);

            query = query.Skip(skip).Take(size);

            return await query.ToArrayAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<RoleClaimViewModel>> GetAllClaimsAsync()
        {
            List<RoleClaimViewModel> roleClaimViewModels = new List<RoleClaimViewModel>();
            var roleclaims =await _dbContext.RoleClaims.ToListAsync();
            foreach (var roleClaim in roleclaims)
            {
                var roleclaimviewmodel = new RoleClaimViewModel()
                {
                    Id = roleClaim.Id,
                    RoleId = roleClaim.RoleId.ToString(),
                    Type = roleClaim.ClaimType,
                    Value = roleClaim.ClaimValue
                };
                roleClaimViewModels.Add(roleclaimviewmodel);
            }
            //await roleclaims.ForEachAsync(delegate (RoleClaim roleClaim)
            //{

            //}

            return roleClaimViewModels;
        }

        public Task<RoleClaimViewModel> GetRoleClaimById(int Id)
        {
            var roleClaim = _dbContext.RoleClaims.SingleOrDefault(p => p.Id == Id);

            var roleclaimviewmodel = new RoleClaimViewModel()
            {
                Id = roleClaim.Id,
                RoleId = roleClaim.RoleId.ToString(),
                Type = roleClaim.ClaimType,
                Value = roleClaim.ClaimValue
            };

            return Task.FromResult(roleclaimviewmodel);
        }
        public async Task<List<RoleClaimViewModel>> GetAllByRoleIdAsync(string roleId)
        {
            List<RoleClaimViewModel> roleClaimViewModels = new List<RoleClaimViewModel>();
            var roleclaims =await _dbContext.RoleClaims.Where(p=>p.RoleId==Guid.Parse(roleId)).ToListAsync();
             roleclaims.ForEach(roleClaim=>
            {
                var roleclaimviewmodel = new RoleClaimViewModel()
                {
                    Id = roleClaim.Id,
                    RoleId = roleClaim.RoleId.ToString(),
                    Type = roleClaim.ClaimType,
                    Value = roleClaim.ClaimValue
                };
                roleClaimViewModels.Add(roleclaimviewmodel);
            }
             );
            return roleClaimViewModels;
        }

        public async Task<string> SaveClaimAsync(RoleClaimViewModel request,string userId)
        {
            if (request.Id == 0)
            {
                var existingRoleClaim =
                    await _dbContext.RoleClaims
                        .SingleOrDefaultAsync(x =>
                            x.RoleId == Guid.Parse(request.RoleId) && x.ClaimType == request.Type && x.ClaimValue == request.Value);
                if (existingRoleClaim != null)
                {
                    return  "Similar Role Claim already exists.";
                }
                var roleClaim = new RoleClaim()
                {
                    Id = request.Id,
                    RoleId = Guid.Parse(request.RoleId),
                    ClaimType = request.Type,
                    ClaimValue = request.Value
                };
                await _dbContext.RoleClaims.AddAsync(roleClaim).AsTask();
                await _dbContext.CommitAsync(userId);
                return  string.Format("Role Claim {0} created.", request.Value);
            }
            else
            {
                var existingRoleClaim =
                    await _dbContext.RoleClaims
                        
                        .SingleOrDefaultAsync(x => x.Id == request.Id);
                if (existingRoleClaim == null)
                {
                    return "Role Claim does not exist.";
                }
                else
                {
                    existingRoleClaim.ClaimType = request.Type;
                    existingRoleClaim.ClaimValue = request.Value;
                   
                    existingRoleClaim.RoleId = Guid.Parse(request.RoleId);
                    if (_dbContext.Entry(existingRoleClaim).State == EntityState.Detached) await _dbContext.RoleClaims.AddAsync(existingRoleClaim).AsTask();

                    _dbContext.Entry(existingRoleClaim).State = EntityState.Modified;

                    await _dbContext.CommitAsync(userId);
                    var role = _dbContext.Roles.FirstOrDefault(p => p.Id == Guid.Parse(request.RoleId));
                    return  string.Format("Role Claim {0} for Role {1} updated.", request.Value, role?.Name);
                }
            }
        }

        public async Task<string> DeleteRoleClaimAsync(int id,string userId)
        {
            var existingRoleClaim = await _dbContext.RoleClaims
                
                .FirstOrDefaultAsync(x => x.Id == id);
            var existingRole = await _dbContext.Roles.FindAsync(existingRoleClaim.RoleId);
            if (existingRoleClaim != null && existingRole!=null)
            {
                _dbContext.RoleClaims.Remove(existingRoleClaim);
                await _dbContext.CommitAsync(userId);
                return string.Format("Role Claim {0} for {1} Role deleted.", existingRoleClaim.ClaimValue, existingRole.Name);
            }
            else
            {
                return "Role Claim does not exist.";
            }
        }
    }
}