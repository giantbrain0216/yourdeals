#nullable enable
using NPComplet.YourDeals.Domain.Shared.Enumerable;
using System;


namespace NPComplet.YourDeals.Domain.Shared.DataContracts
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TEntityId"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class AuditableEntityExtendedAttribute<TId, TEntityId, TEntity>
        : AuditableEntity<TId>, IEntityAuditableExtendedAttribute<TId, TEntityId, TEntity>
            where TEntity : IEntity<TEntityId>
    {
        /// <summary>
        /// 
        /// </summary>
        public TEntityId EntityId { get; set; }

        /// <summary>
        /// Extended attribute's related entity
        /// </summary>
        public virtual TEntity Entity { get; set; }

        /// <inheritdoc/>
        public EntityExtendedAttributeType Type { get; set; }

        /// <inheritdoc/>
        public string Key { get; set; }

        /// <inheritdoc/>
        public string? Text { get; set; }

        /// <inheritdoc/>
        public decimal? Decimal { get; set; }

        /// <inheritdoc/>
        public DateTime? DateTime { get; set; }

        /// <inheritdoc/>
        public string? Json { get; set; }

        /// <inheritdoc/>
        public string? ExternalId { get; set; }

        /// <inheritdoc/>
        public string? Group { get; set; }

        /// <inheritdoc/>
        public string? Description { get; set; }

        /// <inheritdoc/>
        public bool IsActive { get; set; } = true;
    }
}