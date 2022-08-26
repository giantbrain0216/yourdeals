// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Address.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Shared
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Base;
    using NPComplet.YourDeals.Domain.Shared.Enumerable;

    #endregion

    /// <summary>
    ///     the payment address
    /// </summary>
    [Table("ADDRESSES", Schema = "SHARED")]
    public class Address : BaseEntityDomain
    {
        /// <summary>
        ///     the address of the payment
        /// </summary>
        public AddressType AddressType { get; set; }

        /// <summary>
        /// Is default to use in billing.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        ///     the city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     the email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     the house number
        /// </summary>
        public int HouseNumber { get; set; }

        /// <summary>
        ///     location
        /// </summary>

        public Location Location { get; set; }

        /// <summary>
        ///     the location id
        /// </summary>
        public Guid? LocationId { get; set; }

        /// <summary>
        ///     the NumCode
        ///     https://en.wikipedia.org/wiki/ISO_3166-1_numeric#Officially_assigned_code_elements
        ///     https://en.wikipedia.org/wiki/ISO_3166-1
        /// </summary>
        public string NumCountryCode { get; set; }

        /// <summary>
        ///     the phone  number of the payment
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///     the phone Coutry code
        /// </summary>
        public string PhoneCountyCode { get; set; }

        /// <summary>
        ///     the state
        /// </summary>
        public string State { get; set; }

        /// <summary>
        ///     the street
        /// </summary>

        public string Street { get; set; }

        /// <summary>
        ///     the street address
        /// </summary>
        public string StreetAddress { get; set; }

        /// <summary>
        ///     the Zip code
        /// </summary>
        public string ZipCode { get; set; }
    }
}