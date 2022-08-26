// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResetPasswordViewModel.cs" company="NPComplet">
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
    ///     ResetPasswordViewModel
    /// </summary>
    public class ResetPasswordViewModel
    {
        /// <summary>
        ///     Email
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        [EmailAddress(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Invalid_Email")]
        public string Email { get; set; }

        /// <summary>
        ///     Password
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        [StringLength(100, ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "PasswordLengthError", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        //
        [Required]
        public string Token { get; set; }
    }
}