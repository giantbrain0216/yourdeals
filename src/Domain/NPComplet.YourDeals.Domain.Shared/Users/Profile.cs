// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Profile.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Users
{
    #region

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using NPComplet.YourDeals.Domain.Shared.Base;
    using NPComplet.YourDeals.Domain.Shared.Finance;
    using NPComplet.YourDeals.Domain.Shared.Shared;

    #endregion

    /// <summary>
    ///     the profile  entity
    /// </summary>
    [Table("PROFILES", Schema = "USER")]
    public class Profile : BaseDomain
    {
        /// <summary>
        ///     the address
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        ///     the address id
        /// </summary>
        public Guid? AddressId { get; set; }

        /// <summary>
        ///     the list of  payment support
        /// </summary>
        public UserPreferenceFinance UserPreferenceFinance { get; set; }

        /// <summary>
        ///     the image profile
        /// </summary>
        public ProfileImage ProfileImage { get; set; }

        /// <summary>
        ///     the address id
        /// </summary>
        public Guid? ProfileImageId { get; set; }
    }
}