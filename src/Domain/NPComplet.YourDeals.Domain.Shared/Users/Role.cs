// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Role.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Users
{
    #region

    using System;

    using Microsoft.AspNetCore.Identity;

    #endregion

    /// <summary>
    ///     the role entity
    /// </summary>
    public class Role : IdentityRole<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="roleDescription"></param>
       
        /// <summary>
        ///     the role description
        /// </summary>
        public string Description { get; set; }
    }
}