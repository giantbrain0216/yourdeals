using Microsoft.AspNetCore.Identity;
using NPComplet.YourDeals.Domain.Shared.Audit;
using NPComplet.YourDeals.Domain.Shared.Base;
using NPComplet.YourDeals.Server.Infrastructure.Repositories.Data;
using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Server.Infrastructure.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ConcurrentDictionary<Type, object> _repositoriesDic = new();

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> GetRepository<T>() where T : BaseDomain
        {
            _repositoriesDic.TryGetValue(typeof(T), out var repo);
            if (repo == null)
            {
                repo = new GenericRepository<T>(_context);
                _repositoriesDic.TryAdd(typeof(T), repo);
            }

            return (IGenericRepository<T>) repo;
        }
        public IGenericRepositoryAudit<T> GetRepositoryIdentity<T>() where T :Audit
        {
            _repositoriesDic.TryGetValue(typeof(T), out var repo);
            if (repo == null)
            {
                repo = new GenericRepositoryAudit<T>(_context);
                _repositoriesDic.TryAdd(typeof(T), repo);
            }

            return (IGenericRepositoryAudit<T>)repo;
        }
        public Task CommitAsync(string Userid=null)
        {
            return _context.CommitAsync(Userid);
        }
    }
}