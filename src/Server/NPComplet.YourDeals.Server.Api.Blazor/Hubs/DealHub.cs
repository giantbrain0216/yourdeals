// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DealHub.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Hubs
{
    #region

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;

    using NPComplet.YourDeals.Domain.Shared.Constant.Application;
    using NPComplet.YourDeals.Domain.Shared.Deal.Offers;
    using NPComplet.YourDeals.Domain.Shared.Deal.Requests;

    #endregion

    /// <summary>
    /// </summary>
    public class DealHub : Hub
    {
        /// <summary>
        /// </summary>
        /// <param name="offer">
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task SendOfferAsync(IList<Offer> offer)
        {
            await this.Clients.All.SendAsync(ApplicationConstants.SignalR.RealTimeOffer, offer);
        }

        /// <summary>
        /// </summary>
        /// <param name="request">
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task SendRequestAsync(IList<Request> request)
        {
            await this.Clients.All.SendAsync(ApplicationConstants.SignalR.RealTimeRequest, request);
        }
    }
}