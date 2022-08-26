// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropositionOffer.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Deal.Offers
{
    #region

    using Accounting;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text.Json.Serialization;

    #endregion

    /// <summary>
    ///     the proposition for the  offer  entity ( the proposal of the deal - offer -  also can be a simple quotation )
    /// </summary>
    [Table("PROPOSITIONOFFERS", Schema = "DEAL")]
    public class PropositionOffer : Proposition
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid? OfferId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("OfferId")]
        [JsonIgnore]
        public Offer Offer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(name: "OfferId1")]
        public Guid? SelectedOfferId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("SelectedOfferId")]
        [JsonIgnore]
        public Offer SelectedOffer { get; set; }

        /// <summary>
        ///     the Invoice related  the offer
        /// </summary>
        public Invoice Invoice { get; set; }

        /// <summary>
        ///     the Invoice identifier.
        /// </summary>

        public Guid? InvoiceId { get; set; }

        /// <summary>
        /// The total amount of proposition.
        /// </summary>
        public decimal TotalAmount => Invoice?.Amounts?.Sum(a => a.AmountValue) ?? decimal.Zero;

        /// <summary>
        ///     the Quotation related  the request
        /// </summary>
        public Quotation Quotation { get; set; }

        /// <summary>
        ///     the Quotation identifier.
        /// </summary>
        public Guid? QuotationId { get; set; }
    }
}