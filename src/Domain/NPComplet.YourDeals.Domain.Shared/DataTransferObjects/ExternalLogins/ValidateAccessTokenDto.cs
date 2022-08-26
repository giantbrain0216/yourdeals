using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects.ExternalLogins
{
    public class ValidateAccessTokenDto
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider">The external provider: Google , Facebook</param>
        public ValidateAccessTokenDto(string provider)
        {
            this.Provider = provider;
        }
        /// <summary>
        /// The Access token that the user got from facebook
        /// </summary>
        public string AuterizationCode { get; set; }

        /// <summary>
        /// The token that the user get from the api after he got autherized
        /// Required in google authentication
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// user email api got from facebook
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// FirstName api got from facebook
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// lastName api got from facebook
        /// </summary>
        public string LastName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        /// <summary>
        /// is the accessToken valid
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Is the user already registered with us before
        /// </summary>
        public bool IsTheUserExist { get; set; }

        public string Provider { get; private set; }
    }
}
