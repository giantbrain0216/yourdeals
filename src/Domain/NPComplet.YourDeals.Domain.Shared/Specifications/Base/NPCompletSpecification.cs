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
    public abstract class NPCompletSpecification<T> : ISpecification<T> where T : class, IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public Expression<Func<T, bool>> Criteria { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Expression<Func<T, object>>> Includes { get; } = new();
        /// <summary>
        /// 
        /// </summary>
        public List<string> IncludeStrings { get; } = new();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeExpression"></param>

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeString"></param>
        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }
}
