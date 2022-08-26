// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProfilImage.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Users
{
    #region

    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Document;

    #endregion

    /// <summary>
    ///     the profile image entity
    /// </summary>
    [Table("PROFILEIMAGES", Schema = "USER")]
    public class ProfileImage : StoredFile
    {
    }
}