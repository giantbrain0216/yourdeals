// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginViewModel.cs" company="NPComplet">
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
    ///     Login view model
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        ///     Email for the login
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        [EmailAddress(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Invalid_Email")]
        public string Email { get; set; }

        /// <summary>
        ///     The user passwword
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        ///     the remeber me for the user account
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}