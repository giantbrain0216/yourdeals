using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Deals.Interfaces
{
    /// <summary>
    /// The deal history interface.
    /// </summary>
    public interface IDealsHistoryViewModel
    {
        /// <summary>
        /// Gets the offers count asynchronously.
        /// </summary>
        /// <returns>The total count.</returns>
        /// <param name="isOffers">True,if the count is for offers otherwise false.</param>
        Task<int> GetDealsCountAsync(bool isOffers = false);

        /// <summary>
        /// Gets the deals asynchronous.
        /// </summary>
        /// <typeparam name="T">Offer or Request type</typeparam>
        /// <param name="startIndex">The start index.</param>
        /// <param name="size">The size.</param>
        /// <param name="isOffers"><c>true</c> if the deal is offer, otherwise false.</param>
        /// <returns>Offers or requests deals.</returns>
        Task<IList<T>> GetDealsAsync<T>(int startIndex, int size, bool isOffers = false);

        /// <summary>
        /// Gets the selected propositions asynchronous.
        /// </summary>
        /// <param name="id">The offer or request identifier.</param>
        /// <param name="isOffers">True,if the count is for offers otherwise false.</param>
        /// <returns>Propositions.</returns>
        Task<IList<T>> GetSelectedPropositionsAsync<T>(Guid id, bool isOffers = false);
    }
}
