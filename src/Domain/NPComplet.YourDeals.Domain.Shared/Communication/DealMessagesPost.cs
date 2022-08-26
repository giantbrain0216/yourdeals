// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DealMessagesPost.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Communication
{
    using NPComplet.YourDeals.Domain.Shared.Shared;
    #region

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    /// <summary>
    ///     the deal message post entity
    /// </summary>
    [Table("DEALMESSAGESPOSTS", Schema = "DOCUMENTS")]
    public class DealMessagesPost : Post
    {
        /// <summary>
        ///     messages related to the deal message post
        /// </summary>
        public ICollection<Message> Messages { get; set; }
    }
}