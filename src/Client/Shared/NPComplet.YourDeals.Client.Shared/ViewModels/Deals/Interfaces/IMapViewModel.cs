// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMapViewModel.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.Deal.Offers;
using NPComplet.YourDeals.Domain.Shared.Deal.Requests;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Deals.Interfaces
{
    #region

    #endregion

    /// <summary>
    ///     The MapVM interface.
    /// </summary>
    public interface IMapViewModel
    {
        /// <summary>
        ///     Gets the pins.
        /// </summary>
        IList<Offer> OfferPins { get; set; }

        /// <summary>
        ///     Gets the request pins.
        /// </summary>
        IList<Request> RequestPins { get; set; }

        /// <summary>
        ///     The get last update date async.
        /// </summary>
        /// <returns>
        ///     The <see cref="ValueTask" />.
        /// </returns>
        ValueTask<DateTime?> GetLastUpdateDateAsync();

        /// <summary>
        /// The get nearst deal.
        /// </summary>
        /// <param name="latitude">
        /// The latitude.
        /// </param>
        /// <param name="longitude">
        /// The longitude.
        /// </param>
        /// <param name="zoom">
        /// The zoom.
        /// </param>
        /// <param name="orderBy">
        /// The order By.
        /// </param>
        /// <param name="include">
        /// The include.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<IList<Offer>> GetNearstOffer(
            double latitude,
            double longitude,
            double zoom,
            string orderBy,
            string include);

        /// <summary>
        /// The get nearst request.
        /// </summary>
        /// <param name="latitude">
        /// The latitude.
        /// </param>
        /// <param name="longitude">
        /// The longitude.
        /// </param>
        /// <param name="zoom">
        /// The zoom.
        /// </param>
        /// <param name="orderBy">
        /// The order by.
        /// </param>
        /// <param name="include">
        /// The include.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<IList<Request>> GetNearstRequest(
            double latitude,
            double longitude,
            double zoom,
            string orderBy,
            string include);

        /// <summary>
        ///     The get outstanding local edits async.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        Task<Offer[]> GetOutstandingLocalofferEditsAsync();

        /// <summary>
        ///     The get outstanding localrequest edits async.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        Task<Request[]> GetOutstandingLocalrequestEditsAsync();

        /// <summary>
        ///     The load data.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        Task LoadData();
    }
}