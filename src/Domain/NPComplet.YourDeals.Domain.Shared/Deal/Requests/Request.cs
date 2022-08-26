// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Request.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Deal.Requests
{
    #region

    using Accounting;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    /// <summary>
    ///     the request entity
    /// </summary>
    [Table("REQUESTS", Schema = "DEAL")]
    public class Request : Deal
    {
        /// <summary>
        ///     the Invoice related of the deliver service
        /// </summary>
        public Invoice Invoice { get; set; }

        /// <summary>
        ///     the Invoice identifier.
        /// </summary>
        public Guid? InvoiceId { get; set; }

        /// <summary>
        ///     List of propositions
        /// </summary>
        [InverseProperty("Request")]
        public ICollection<PropositionRequest> Propositions { get; set; }

        /// <summary>
        ///     List of selected Propositions
        /// </summary>
        [InverseProperty("SelectedRequest")]
        public ICollection<PropositionRequest> SelectedPropositions { get; set; }
    }
}