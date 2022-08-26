// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Post.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Shared
{
    #region

    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Base;

    #endregion

    /// <summary>
    ///     the post entity
    /// </summary>
    [Table("POSTS", Schema = "SHARED")]
    public class Post : BaseEntityDomain
    {
        /// <summary>
        ///     the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     the title
        /// </summary>
        public string Title { get; set; }
    }
}