// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChangePasswordViewModel.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects.ManageViewModels
{
    #region

    using System.ComponentModel.DataAnnotations;

    using NPComplet.YourDeals.Translations.Resources;

    #endregion

    /// <summary>
    ///     ChangePasswordViewModel
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        ///     ConfirmPassword
        /// </summary>
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Translation), Name = "Confirm_password")]
        [Compare("Password", ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "PasswordSouldConfirm")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        ///     NewPassword
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Translation),  Name = "New_password")]
        [StringLength(100, ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "PasswordLengthError", MinimumLength = 6)]
        public string NewPassword { get; set; }

        /// <summary>
        ///     OldPassword
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Translation), Name = "Current_password")]
        public string OldPassword { get; set; }

        /// <summary>
        ///     StatusMessage
        /// </summary>
        public string StatusMessage { get; set; }
    }
}