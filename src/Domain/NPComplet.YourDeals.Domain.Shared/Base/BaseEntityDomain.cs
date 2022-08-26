// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseEntityDomain.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Base
{
    #region

    using System;

    using NPComplet.YourDeals.Domain.Shared.Users;

    #endregion

    /// <summary>
    ///     the base domain entity
    /// </summary>
    public class BaseEntityDomain : BaseDomain
    {
        /// <summary>
        ///     Owner
        /// </summary>
        public User Owner { get; set; }

        /// <summary>
        ///     the deal Owner Id
        /// </summary>
        public Guid? OwnerId { get; set; }
    }
}