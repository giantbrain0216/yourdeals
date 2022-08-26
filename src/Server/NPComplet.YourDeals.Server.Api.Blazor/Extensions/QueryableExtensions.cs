using System.Linq;
using Microsoft.EntityFrameworkCore;
using NPComplet.YourDeals.Domain.Shared.DataContracts;
using NPComplet.YourDeals.Domain.Shared.Specifications.Base;

namespace NPComplet.YourDeals.Server.Api.Blazor.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        public static IQueryable<T> Specify<T>(this IQueryable<T> query, ISpecification<T> spec) where T : class, IEntity
        {
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(query,
                    (current, include) => current.Include(include));
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));
            return secondaryResult.Where(spec.Criteria);
        }
    }
}
