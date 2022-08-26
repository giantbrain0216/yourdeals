// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Invoice.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Accounting
{
    #region

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;
    using System.Linq;
    using NPComplet.YourDeals.Domain.Shared.Base;
    using NPComplet.YourDeals.Domain.Shared.Pricing;
    using NPComplet.YourDeals.Domain.Shared.Shared;

    #endregion

    /// <summary>
    ///     the invoice entity
    /// </summary>
    [Table("INVOICES", Schema = "ACCOUNTING")]
    public class Invoice : BaseEntityDomain
    {
        /// <summary>
        ///     the address of the invoice
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        ///     the address id
        /// </summary>
        public Guid? AddressId { get; set; }

        /// <summary>
        ///     the list of amounts of the invoice
        /// </summary>
        public ICollection<Amount> Amounts { get; set; }

        /// <summary>
        ///     the address of the invoice
        /// </summary>
        public Address ClientAddress { get; set; }

        /// <summary>
        ///     the address id
        /// </summary>
        public Guid? ClientAddressId { get; set; }

        /// <summary>
        ///     the client Name
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        ///     the company
        /// </summary>
        public Company Company { get; set; }

        /// <summary>
        ///     the company Id
        /// </summary>
        public Guid? CompanyId { get; set; }

        /// <summary>
        ///     Set the unique number
        /// </summary>
        [IgnoreDataMember]
        [JsonIgnore]
        public string InternalUniqueNumber { get; set; }

        /// <summary>
        ///     the  description of the invoice
        /// </summary>

        public Post Post { get; set; }

        /// <summary>
        ///     the description of the post Id
        /// </summary>
        public Guid? PostId { get; set; }

        /// <summary>
        ///     the print date
        /// </summary>
        public DateTime PrintDate { get; set; }

        /// <summary>
        ///     the unique number
        /// </summary>
        public string UniqueNumber => this.InternalUniqueNumber;
        /// <summary>
        /// Invoice file Path for Download
        /// </summary>
        public string InvoicePath { get; set; }
        public Guid? PurchaserId { get; set; }
        public bool IsPaid { get; set; }

        [NotMapped]
        public decimal TotalAmount => Amounts?.Sum(a => a.AmountValue) ?? decimal.Zero;
    }
}