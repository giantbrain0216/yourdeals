using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using NPComplet.YourDeals.Client.Shared.WebUI.Shared;
using NPComplet.YourDeals.Domain.Shared.Deal.Offers;
using NPComplet.YourDeals.Domain.Shared.Deal.Requests;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Deals.Interfaces
{
    /// <summary>
    /// The proposition view model interface.
    /// </summary>
    public interface IPropositionViewModel
    {
        PropositionOffer PropositionOffer { get; set; }
        PropositionRequest PropositionRequest { get; set; }
        ImageUpload ImageUpload { get; set; }

        /// <summary>
        /// Create proposition offer asynchronously
        /// </summary>
        Task CreatePropositionOfferAsync(PropositionOffer propositionOffer, Guid id, Guid dealMessagePostId, EventCallback refreshComponent);

        /// <summary>
        /// Create proposition request asynchronously
        /// </summary>
        Task CreatePropositionRequestAsync(PropositionRequest propositionRequest, Guid id, Guid dealMessagePostId, EventCallback refreshComponent);

        /// <summary>
        /// Accept proposition asynchronously.
        /// </summary>
        /// <param name="isOffer">True, if the proposition si about offer, otherwise false.</param>
        /// <param name="propositionId">The proposition identifier.</param>
        /// <param name="refreshComponent">The event call back for refresh propositions.</param>
        Task AcceptPropositionAsync(bool isOffer, Guid propositionId, EventCallback refreshComponent);
    }
}
