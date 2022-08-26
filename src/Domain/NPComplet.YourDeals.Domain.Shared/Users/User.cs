// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Users
{
    #region

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Identity;
    using NPComplet.YourDeals.Domain.Shared.DataContracts;

    #endregion

    /// <summary>
    ///     the user entity
    /// </summary>
   // [Table("AspNetUsers", Schema = "rightnde_sa")]
    public class User : IdentityUser<Guid>,IEntity
    {
        /// <summary>
        /// </summary>

        [NotMapped]
        public string ConfirmPassword { get; set; }

        /// <summary>
        ///     the first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     the last name
        /// </summary>
        public string LastName { get; set; }


        /// <summary>
        /// the old email account
        /// </summary>
        [ProtectedPersonalData]
        public string OldEmail { get; set; }


        /// <summary>
        ///  is the account is deleted 
        /// </summary>
        public bool IsDeletedAccount { get; set; }
        /// <summary>
        ///     the  profile
        /// </summary>
        public Guid? ProfileId { get; set; }

        /// <summary>
        ///     the  profile
        /// </summary>
        public Profile Profile { get; set; }

  
        /// <summary>
        /// 
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime RefreshTokenExpiryTime { get; set; }
        /// <summary>
        /// </summary>
        [NotMapped]
        public IEnumerable<string> RoleNames { get; set; }

        /// <summary>
        ///     role name
        /// </summary>

        [NotMapped]
        public IEnumerable<Role> Roles { get; set; }
    }
}