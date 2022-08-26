// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExternalLoginViewModel.cs" company="NPComplet">
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
    ///     ExternalLoginViewModel
    /// </summary>
    public class ExternalLoginViewModel
    {
        /// <summary>
        ///     Email Addres of the User
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     The unique identifier for this user provided by the login provider.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        public string ExternalId { get; set; }

        /// <summary>
        ///     First Name
        /// </summary>
        public string FistName { get; set; }

        /// <summary>
        ///     Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Name of the provider e.g Google
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        public string Provider { get; set; }
    }
}