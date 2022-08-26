// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthResponseDto.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects
{
    #region

    using System;

    #endregion

    /// <summary>
    ///     Auth Response
    /// </summary>
    public class AuthResponseDto
    {
        /// <summary>
        ///     Error Message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     Is Successfull
        /// </summary>
        public bool IsAuthSuccessful { get; set; }

        /// <summary>
        /// </summary>
        public bool IsConfiremd { get; set; }

        /// <summary>
        ///     Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// </summary>
        public Guid? userId { get; set; }
    }
}