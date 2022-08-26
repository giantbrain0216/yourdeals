// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Document.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Document
{
    #region

    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Base;

    #endregion

    /// <summary>
    /// </summary>
    [Table("DOCUMENTS", Schema = "DOCUMENT")]
    public class Document : BaseEntityDomain
    {
        /// <summary>
        ///     the Description
        /// </summary>

        public string Description { get; set; }

        /// <summary>
        ///     the  title
        /// </summary>

        public string Title { get; set; }
    }
}