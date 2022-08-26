// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDealViewModel.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.Deal;
using NPComplet.YourDeals.Domain.Shared.Deal.Offers;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Deals.Interfaces
{
    #region

    #endregion

    /// <summary>
    ///     The DealViewModel interface.
    /// </summary>
    public interface IDealViewModel
    {
        /// <summary>
        ///     Gets the deal.
        /// </summary>
        Deal Deal { get; set; }

        /// <summary>
        /// Get request deal by id.
        /// </summary>
        /// <param name="id"> The id. </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<Deal> GetRequestDealById(string id);

        /// <summary>
        /// Get offer deal.
        /// </summary>
        /// <param name="id">The offer identifier.</param>
        /// <returns>The offer deal.</returns>
        Task<Offer> GetOfferDealById(string id);

        /// <summary>
        /// Get offer or request propositions
        /// </summary>
        /// <typeparam name="T">PropositionOffer or PropositionRequest</typeparam>
        /// <param name="id">The offer or request identifier.</param>
        /// <param name="userId">The proposition owner identifier.</param>
        /// <param name="isOwner">true if the user owner.</param>
        /// <param name="isOffers">true, if the propositions are for offer, otherwise false.</param>
        /// <returns>Get offer or request propositions.</returns>
        Task<IList<T>> GetPropositions<T>(string id, string userId, bool isOwner, bool isOffers = false);
    }
}