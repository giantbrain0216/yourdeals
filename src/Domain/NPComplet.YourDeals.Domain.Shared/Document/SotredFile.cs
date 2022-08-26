// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SotredFile.cs" company="NPComplet">
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
    ///     the file info properties
    /// </summary>
    [Table("STOREDFILES", Schema = "DOCUMENT")]
    public class StoredFile : BaseEntityDomain
    {
        /// <summary>
        ///     Datafiles
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        ///     the file extensions
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        ///     the file name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        ///     the local path
        /// </summary>
        public string LocalPath { get; set; }

    }
}