using System;

namespace NPComplet.YourDeals.Domain.Shared.DataContracts
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public abstract class AuditableEntity<TId> : IAuditableEntity<TId>
    {
        /// <summary>
        /// 
        /// </summary>
        public TId Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastModifiedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifiedOn { get; set; }
    }
}