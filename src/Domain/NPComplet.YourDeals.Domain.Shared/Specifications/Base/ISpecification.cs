using NPComplet.YourDeals.Domain.Shared.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NPComplet.YourDeals.Domain.Shared.Specifications.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T> where T : class, IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        Expression<Func<T, bool>> Criteria { get; }
        /// <summary>
        /// 
        /// </summary>
        List<Expression<Func<T, object>>> Includes { get; }
        /// <summary>
        /// 
        /// </summary>
        List<string> IncludeStrings { get; }
    }
}
