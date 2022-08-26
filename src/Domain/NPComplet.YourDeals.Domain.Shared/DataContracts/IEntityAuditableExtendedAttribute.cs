namespace NPComplet.YourDeals.Domain.Shared.DataContracts
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TEntityId"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityAuditableExtendedAttribute<TId, TEntityId, TEntity>
        : IEntityExtendedAttribute<TId, TEntityId, TEntity>, IAuditableEntity<TId>
            where TEntity : IEntity<TEntityId>
    {
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntityId"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityAuditableExtendedAttribute<TEntityId, TEntity>
        : IEntityExtendedAttribute<TEntityId, TEntity>, IAuditableEntity
            where TEntity : IEntity<TEntityId>
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IEntityAuditableExtendedAttribute : IEntityExtendedAttribute, IAuditableEntity
    {
    }
}