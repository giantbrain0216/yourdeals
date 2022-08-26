using Microsoft.AspNetCore.Identity;
using NPComplet.YourDeals.Domain.Shared.Audit;
using NPComplet.YourDeals.Domain.Shared.Base;
using System;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> GetRepository<T>() where T : BaseDomain;
       
        IGenericRepositoryAudit<T> GetRepositoryIdentity<T>() where T : Audit;
        Task CommitAsync(string Userid = null);
    }
}