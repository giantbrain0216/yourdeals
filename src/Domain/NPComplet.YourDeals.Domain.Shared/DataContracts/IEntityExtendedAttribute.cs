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
    public interface IEntityExtendedAttribute<TId, TEntityId, TEntity>
        : IEntityExtendedAttribute<TEntityId, TEntity>, IEntity<TId>
            where TEntity : IEntity<TEntityId>
    {
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntityId"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityExtendedAttribute<TEntityId, TEntity>
        : IEntityExtendedAttribute
            where TEntity : IEntity<TEntityId>
    {
        /// <summary>
        /// External attribute's entity id
        /// </summary>
        public TEntityId EntityId { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IEntityExtendedAttribute : IEntity
    {
        /// <summary>
        /// Extended attribute value type
        /// </summary>
        public EntityExtendedAttributeType Type { get; set; }

        /// <summary>
        /// Extended attribute key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Value for extended attribute with type 1
        /// </summary>
        public decimal? Decimal { get; set; }

        /// <summary>
        /// Value for extended attribute with type 2
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Value for extended attribute with type 3
        /// </summary>
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// Value for extended attribute with type 4
        /// </summary>
        public string? Json { get; set; }

        /// <summary>
        /// Extended attribute external id
        /// </summary>
        public string? ExternalId { get; set; }

        /// <summary>
        /// Extended attribute group
        /// </summary>
        public string? Group { get; set; }

        /// <summary>
        /// Extended attribute description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Is active
        /// </summary>
        public bool IsActive { get; set; }
    }
}