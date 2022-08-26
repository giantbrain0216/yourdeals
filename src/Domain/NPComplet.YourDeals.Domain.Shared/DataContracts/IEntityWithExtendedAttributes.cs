using System.Collections.Generic;

namespace NPComplet.YourDeals.Domain.Shared.DataContracts
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TExtendedAttribute"></typeparam>
    public interface IEntityWithExtendedAttributes<TExtendedAttribute>
    {
        /// <summary>
        /// 
        /// </summary>
        public ICollection<TExtendedAttribute> ExtendedAttributes { get; set; }
    }
}