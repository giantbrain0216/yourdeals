// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistrationResponseDto.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects
{
    #region

    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    #endregion

    /// <summary>
    /// </summary>
    public class RegistrationResponseDto
    {
        /// <summary>
        /// </summary>
        public IEnumerable<IdentityError> Errors { get; set; }

        /// <summary>
        /// </summary>
        public bool IsSuccessfulRegistration { get; set; }

        /// <summary>
        /// </summary>
        public Guid? userId { get; set; }
    }
}