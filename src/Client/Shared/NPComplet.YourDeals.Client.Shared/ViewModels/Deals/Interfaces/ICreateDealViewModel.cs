// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICreateDealVM.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using NPComplet.YourDeals.Client.Shared.WebUI.Pages.LocationsUI;
using NPComplet.YourDeals.Client.Shared.WebUI.Shared;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Deals.Interfaces
{
    #region

    #endregion

    /// <summary>
    ///     The CreateDealVM interface.
    /// </summary>
    public interface ICreateDealViewModel
    {
        /// <summary>
        ///     Gets the image upload.
        /// </summary>
        ImageUpload ImageUpload { get; set; }

        /// <summary>
        ///     Gets a value indicating whether is offer.
        /// </summary>
        bool IsOffer { get; set; }

        /// <summary>
        ///     Gets a value indicating whether loading.
        /// </summary>
        bool Loading { get; set; }

        /// <summary>
        ///     Gets the offer.
        /// </summary>
        OfferDealViewModel Offer { get; set; }

        /// <summary>
        ///     Gets the request.
        /// </summary>
        DealViewModel Request { get; set; }

        /// <summary>
        ///     Gets the set position.
        /// </summary>
        SetPostion SetPosition { get; set; }

        /// <summary>
        ///     Gets the tag builder control.
        /// </summary>
        TagBuilderControl TagBuilderControl { get; set; }

        /// <summary>
        ///     Gets the token.
        /// </summary>
        string Token { get; set; }

        /// <summary>
        ///     The deal offer.
        /// </summary>
        void DealOffer();

        /// <summary>
        ///     The deal request.
        /// </summary>
        void DealRequest();

        /// <summary>
        ///     The handle smart tagging.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        Task HandleSmartTagging();

        /// <summary>
        /// The handle submit.
        /// </summary>
        /// <param name="onSubmit">
        /// The on submit.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task HandleSubmit(EventCallback<bool> onSubmit);

        /// <summary>
        ///     The open set pos screen.
        /// </summary>
        void OpenSetPosScreen();
    }
}