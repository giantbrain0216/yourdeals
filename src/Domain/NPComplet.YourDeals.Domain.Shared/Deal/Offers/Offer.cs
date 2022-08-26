// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Offer.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Deal.Offers
{
    #region

    using Accounting;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    /// <summary>
    ///     the offer entity
    /// </summary>
    [Table("OFFERS", Schema = "DEAL")]
    public class Offer : Deal
    {
        /// <summary>
        ///     the quotation related to the service offer
        /// </summary>
        public Quotation Quotation { get; set; }

        /// <summary>
        ///     the quotation identifier.
        /// </summary>
        public Guid? QuotationId { get; set; }

        /// <summary>
        ///     List of propositions
        /// </summary>
        [InverseProperty("Offer")]
        public ICollection<PropositionOffer> Propositions { get; set; }

        /// <summary>
        ///     List of selected Propositions
        /// </summary>
        [InverseProperty("SelectedOffer")]
        public ICollection<PropositionOffer> SelectedPropositions { get; set; }
    }
}