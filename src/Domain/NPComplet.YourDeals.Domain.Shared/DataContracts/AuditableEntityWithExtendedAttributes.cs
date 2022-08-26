using System.Collections.Generic;

namespace NPComplet.YourDeals.Domain.Shared.DataContracts
{ 
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TEntityId"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TExtendedAttribute"></typeparam>
    public abstract class AuditableEntityWithExtendedAttributes<TId, TEntityId, TEntity, TExtendedAttribute> 
        : AuditableEntity<TEntityId>, IEntityWithExtendedAttributes<TExtendedAttribute>
            where TEntity : IEntity<TEntityId>
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<TExtendedAttribute> ExtendedAttributes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public AuditableEntityWithExtendedAttributes()
        {
            ExtendedAttributes = new HashSet<TExtendedAttribute>();
        }
    }
}