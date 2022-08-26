using System;

namespace NPComplet.YourDeals.Domain.Shared.DataContracts
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public interface IAuditableEntity<TId> : IAuditableEntity, IEntity<TId>
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IAuditableEntity : IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        string CreatedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        DateTime CreatedOn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string LastModifiedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        DateTime? LastModifiedOn { get; set; }
    }
}