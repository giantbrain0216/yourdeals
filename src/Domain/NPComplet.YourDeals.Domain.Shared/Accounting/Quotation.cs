// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Quotation.cs" company="NPComplet">
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

    using NPComplet.YourDeals.Domain.Shared.Base;
    using NPComplet.YourDeals.Domain.Shared.Pricing;
    using NPComplet.YourDeals.Domain.Shared.Shared;

    #endregion

    /// <summary>
    ///     the quotation entity
    /// </summary>
    [Table("QUOTATIONS", Schema = "ACCOUNTING")]
    public class Quotation : BaseEntityDomain
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
        ///     the amount of the quotation
        /// </summary>
        public virtual ICollection<Amount> Amounts { get; set; }

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
        ///     the client signature for the validation
        /// </summary>
        public Signature ClientSignature { get; set; }

        /// <summary>
        ///     the signature id
        /// </summary>
        public Guid? ClientSignatureId { get; set; }

        /// <summary>
        ///     the company
        /// </summary>
        public Company Company { get; set; }

        /// <summary>
        ///     the company Id
        /// </summary>
        public Guid? CompanyId { get; set; }

        /// <summary>
        ///     the due date
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        ///     Set the unique number
        /// </summary>
        [IgnoreDataMember]
        [JsonIgnore]
        public string InternalUniqueNumber { get; set; }

        /// <summary>
        ///     the quotation duration
        /// </summary>
        public TimeSpan InvoiceDuration { get; set; }

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
        ///     the start date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///     the  term of condition
        /// </summary>

        public Post TermConditionPost { get; set; }

        /// <summary>
        ///     the  term of condition Id
        /// </summary>

        public Guid? TermConditionPostId { get; set; }

        /// <summary>
        ///     the unique number
        /// </summary>
        public string UniqueNumber => this.InternalUniqueNumber;

        /// <summary>
        ///     the  warranty post
        /// </summary>

        public Post WarrantyPost { get; set; }

        /// <summary>
        ///     the  warranty post Id
        /// </summary>
        public Guid? WarrantyPostId { get; set; }

        public string QuotationPath { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsRejected { get; set; }
        public Guid? ProposerId { get; set; }

    }
}