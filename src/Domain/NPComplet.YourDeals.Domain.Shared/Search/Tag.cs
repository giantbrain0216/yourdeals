// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tag.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Search
{
    #region

    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Base;

    #endregion

    /// <summary>
    /// </summary>
    [Table("TAGS", Schema = "SEARCH")]
    public class Tag : BaseEntityDomain
    {
        /// <summary>
        /// </summary>
        public string TagString { get; set; }
    }
}