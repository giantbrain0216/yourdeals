using System.ComponentModel.DataAnnotations;
using NPComplet.YourDeals.Translations.Resources;

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto
{
    /// <summary>
    /// The offer deal view model
    /// </summary>
    public class OfferDealViewModel : DealViewModel
    {
        /// <summary>
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        public string WarrantyDescription { get; set; }

        /// <summary>
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        public string TermConditionDescription { get; set; }
    }
}
