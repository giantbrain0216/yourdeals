// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Signature.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Shared
{
    #region

    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Document;

    #endregion

    /// <summary>
    /// </summary>
    [Table("SIGNATURE", Schema = "SHARED")]
    public class Signature : StoredFile
    {
    }
}