// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DealMessagesPost.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Communication
{
    #region

    using NPComplet.YourDeals.Domain.Shared.Shared;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    /// <summary>
    ///     the deal message post entity
    /// </summary>
    [Table("PROPOSITIONMESSAGESPOSTS", Schema = "DOCUMENTS")]
    public class PropositionMessagesPost : Post
    {
        /// <summary>
        ///     messages related to the deal message post
        /// </summary>
        public ICollection<Message> Messages { get; set; }
    }
}