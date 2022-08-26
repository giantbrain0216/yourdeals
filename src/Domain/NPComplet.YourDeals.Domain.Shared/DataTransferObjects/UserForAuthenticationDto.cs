// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserForAuthenticationDto.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects
{
    #region

    using System.ComponentModel.DataAnnotations;

    using NPComplet.YourDeals.Translations.Resources;

    #endregion

    /// <summary>
    ///     UserForAuthenticationDto
    /// </summary>
    public class UserForAuthenticationDto
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
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Password_is_required")]
        public string Password { get; set; }

        /// <summary>
        ///     RememberMe
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Remember_me")]
        public bool RememberMe { get; set; }
    }
}