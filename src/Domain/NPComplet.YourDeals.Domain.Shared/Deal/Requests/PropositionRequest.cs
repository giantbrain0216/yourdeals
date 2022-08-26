// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropositionRequest.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Deal.Requests
{
    #region

    using Accounting;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text.Json.Serialization;

    #endregion

    /// <summary>
    ///     the proposition  for the request  entity ( the proposal of the deal - request -  also can be a simple invoice )
    /// </summary>
    [Table("PROPOSITIONREQUESTS", Schema = "DEAL")]
    public class PropositionRequest : Proposition
    {
        /// <summary>
        /// The request identifier.
        /// </summary>
        public Guid? RequestId { get; set; }

        /// <summary>
        /// The request.
        /// </summary>
        [ForeignKey("RequestId")]
        [JsonIgnore]
        public Request Request { get; set; }

        /// <summary>
        /// The selected request identifier.
        /// </summary>
        [Column(name: "RequestId1")]
        public Guid? SelectedRequestId { get; set; }

        /// <summary>
        /// The selected request.
        /// </summary>
        [ForeignKey("SelectedRequestId")]
        [JsonIgnore]
        public Request SelectedRequest { get; set; }

        /// <summary>
        ///     the Quotation related  the request
        /// </summary>
        public Quotation Quotation { get; set; }

        /// <summary>
        ///     the Quotation identifier.
        /// </summary>
        public Guid? QuotationId { get; set; }

        /// <summary>
        ///     the  document
        /// </summary>
        public DealDocument Document { get; set; }

        /// <summary>
        ///     the  document Id
        /// </summary>
        public Guid? DocumentId { get; set; }

        /// <summary>
        /// The total amount of proposition.
        /// </summary>
        public decimal TotalAmount => Quotation?.Amounts?.Sum(a => a.AmountValue) ?? decimal.Zero;
    }
}