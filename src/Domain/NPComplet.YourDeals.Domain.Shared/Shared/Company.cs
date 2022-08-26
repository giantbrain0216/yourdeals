// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Company.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Shared
{
    #region

    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Base;

    #endregion

    /// <summary>
    ///     the company class
    /// </summary>
    [Table("COMPANY", Schema = "SHARED")]
    public class Company : BaseEntityDomain
    {
        /// <summary>
        ///     the company Identifier
        /// </summary>
        public string CompanyIdentifier { get; set; }

        /// <summary>
        ///     the company name
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        ///     the legal form
        /// </summary>
        public string LegalForm { get; set; }

        /// <summary>
        ///     Tax identifier Number
        /// </summary>
        public string TaxIdentifierNumber { get; set; }
    }
}