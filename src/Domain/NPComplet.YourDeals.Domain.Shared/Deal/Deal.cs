// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Deal.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Deal
{
    #region

    using Base;
    using Communication;
    using Finance;
    using Pricing;
    using Search;
    using Shared;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    /// <summary>
    ///     the deal base entity ( can be offer / request )
    /// </summary>
    [Table("DEALS", Schema = "DEAL")]
    public class Deal : BaseEntityDomain
    {
        /// <summary>
        ///     the address of the deal
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        ///     the address Id
        /// </summary>
        public Guid? AddressId { get; set; }

        /// <summary>
        ///     the  document
        /// </summary>
        
        public DealDocument DealDocument { get; set; }

        /// <summary>
        ///     the  document Id
        /// </summary>
        public Guid? DealDocumentId { get; set; }

        /// <summary>
        ///     the deal post message
        /// </summary>
        public DealMessagesPost DealMessagesPost { get; set; }

        /// <summary>
        ///     the deal message post  Id
        /// </summary>
        public Guid? DealMessagesPostId { get; set; }

        /// <summary>
        ///     the deal price reference of the deal
        /// </summary>
        public DealPriceReference DealPriceReference { get; set; }

        /// <summary>
        ///     the deal price reference  Id
        /// </summary>
        public Guid? DealPriceReferenceId { get; set; }

        /// <summary>
        ///     define if the deal is done or not
        /// </summary>
        public bool? IsDone { get; set; }

        /// <summary>
        ///     define if the deal is locked or not
        /// </summary>
        public bool? IsLocked { get; set; }

        /// <summary>
        ///     Define is the offer is selected
        /// </summary>
        public bool? IsSelected { get; set; }

        /// <summary>
        ///     Search tags
        /// </summary>
        public ICollection<Tag> SearchTags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? DealsFinanceOpertationId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DealsFinanceOpertation DealFinanceOpreation { get; set; }
    }
}