// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DealDocument.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;

namespace NPComplet.YourDeals.Domain.Shared.Deal
{
    #region

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Base;
    using NPComplet.YourDeals.Domain.Shared.Document;

    #endregion

    /// <summary>
    ///     DealDocument
    /// </summary>
    [Table("DEALDOCUMENTS", Schema = "DEAL")]
    public class DealDocument : Document
    {
        /// <summary>
        /// The default constructor.
        /// </summary>
        public DealDocument()
        {
        }

        /// <summary>
        /// Instantiate deal document from files url.
        /// </summary>
        /// <param name="filesUrl">The files url.</param>
        public DealDocument(IReadOnlyCollection<string> filesUrl)
        {
            if (filesUrl == null || !filesUrl.Any())
            {
                return;
            }

            DealFiles = filesUrl.Select(fileName => new StoredFile {FileName = fileName}).ToList();
        }

        /// <summary>
        ///     the  deals files
        /// </summary>
        public ICollection<StoredFile> DealFiles { get; set; }
    }
}