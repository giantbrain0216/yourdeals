// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMapLogic.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Client.Shared.Logic.Interfaces
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NPComplet.YourDeals.Domain.Shared.Deal;

    #endregion

    /// <summary>
    /// The MapLogic interface.
    /// </summary>
    public interface IMapLogic
    {
        /// <summary>
        /// The get last update date async.
        /// </summary>
        /// <returns>
        /// The <see cref="ValueTask"/>.
        /// </returns>
        ValueTask<DateTime?> GetLastUpdateDateAsync();

        /// <summary>
        /// The get nearst deal.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<IList<Deal>> GetNearstDeal();

        /// <summary>
        /// The get outstanding local edits async.
        /// </summary>
        /// <returns>
        /// The <see cref="ValueTask"/>.
        /// </returns>
        ValueTask<Deal[]> GetOutstandingLocalEditsAsync();
    }
}