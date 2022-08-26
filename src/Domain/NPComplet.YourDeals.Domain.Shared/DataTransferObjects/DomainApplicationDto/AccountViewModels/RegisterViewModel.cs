// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterViewModel.cs" company="NPComplet">
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
    ///     the register view model
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        ///     the confirm password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Translation), Name = "Confirm_password")]
        [Compare("Password", ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "PasswordSouldConfirm")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        ///     the required mail
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        [EmailAddress(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Invalid_Email")]
        [Display(ResourceType = typeof(Translation), Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        ///     the required mail
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        [Display(ResourceType = typeof(Translation), Name = "First_name")]
        public string FirstName { get; set; }

        /// <summary>
        ///     the required mail
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        [Display(ResourceType = typeof(Translation), Name = "Last_name")]
        public string LastName { get; set; }

        /// <summary>
        ///     the password format
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        [StringLength(100, ErrorMessageResourceType = typeof(Translation),ErrorMessageResourceName = "PasswordLengthError", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Translation), Name = "Password")]
        public string Password { get; set; }
        public string CallbackUrl { get; set; }
    }
}