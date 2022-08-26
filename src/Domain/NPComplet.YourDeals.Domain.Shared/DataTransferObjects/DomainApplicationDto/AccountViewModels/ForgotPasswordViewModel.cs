// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ForgotPasswordViewModel.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using NPComplet.YourDeals.Translations.Resources;

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels
{
    #region

    #endregion

    /// <summary>
    ///     the forget password view model
    /// </summary>
    public class ForgotPasswordViewModel
    {
        /// <summary>
        ///     the model e mail
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        [EmailAddress(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Invalid_Email")]
        public string Email { get; set; }

        /// <summary>
        ///     the new password
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        /// <summary>
        ///     the confirm password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Translation), Name = "Confirm_password")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "PasswordSouldConfirm")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        ///     Client app Url for redirection
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        public string RedirectUrl { get; set; }

        /// <summary>
        ///     Gets or sets the copied token.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        public string CopiedToken { get; set; }
    }


    
   
}