// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountsController.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;
using NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Models;
using NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Views.Emails.ForgetPassword;

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Users
{
    #region

    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Hangfire;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.AspNetCore.Hosting;

    using NPComplet.YourDeals.Domain.Shared.DataTransferObjects;
    using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.Exceptions;
    using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.ExternalLogins;
    using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.ManageViewModels;
    using NPComplet.YourDeals.Domain.Shared.Shared;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using NPComplet.YourDeals.Domain.Shared.Wrapper;
    using NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Service;
    using NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Views.Emails.ConfirmAccount;
    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;
    using NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces;
    using NPComplet.YourDeals.Translations.Resources;

    using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
    using System.Web;
    using NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Views.Emails.RegisterWithToken;

    #endregion

    /// <summary>
    ///     Account controller , manage user account
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly IEmailSender _emailSender;

        private readonly ILogger _logger;

        protected readonly IUnitOfWork _unitOfWork;

        private IHostingEnvironment _environment;


        private readonly SignInManager<User> _signInManager;

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        private readonly IStringLocalizer<Translation> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IBackgroundJobService _backgroundJobService;

        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

        private readonly IHttpClientFactory _httpClientFactory;

        /// <inheritdoc />
        public AccountsController(
           
            RoleManager<Role> roleManager,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            ILogger<AccountsController> logger,
            IUnitOfWork unitOfWork,
            IHostingEnvironment environment,
            IConfiguration configuration,
            IStringLocalizer<Translation> localizer, 
            IHttpContextAccessor httpContextAccessor,
            IBackgroundJobService backgroundJobService,
             IRazorViewToStringRenderer razorViewToStringRenderer,
             IHttpClientFactory httpClientFactory)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._emailSender = emailSender;
            this._logger = logger;
            this._unitOfWork = unitOfWork;
            this._environment = environment;
            this._configuration = configuration;
            this._localizer = localizer;
            this._httpContextAccessor = httpContextAccessor;
            this._backgroundJobService = backgroundJobService;
            this._razorViewToStringRenderer = razorViewToStringRenderer;
            this._httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Change password for Authenticated user
        /// </summary>
        /// <param name="model">
        /// The change password view model.
        /// </param>
        /// <returns>
        /// return the action 
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var errorList = this.ModelState.ToDictionary(
                    x => x.Key.Replace("model.", string.Empty),
                    x => x.Value.Errors[0].ErrorMessage);

                return this.BadRequest(errorList);
            }

            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
                throw new ApplicationCoreException(
                    ErrorMesage.UnabletoloaduserwithID,
                    $"{this._userManager.GetUserId(this.User)}");

            var changePasswordResult =
                await this._userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded) return this.BadRequest(model);

            await this._signInManager.SignInAsync(user, false);
            this._logger.LogInformation("User changed their password successfully.");

            return this.Ok();
        }

        /// <summary>
        /// Confirm email.
        /// </summary>
        /// <param name="confirmationBody">
        /// The confirmation body.
        /// </param>
        /// <returns>
        /// The confirmation result.
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmationBody confirmationBody)
        {
            string code;
            try
            {
                var base64token = WebEncoders.Base64UrlDecode(confirmationBody.codedToken);
                 code = Encoding.UTF8.GetString(base64token);
            }
            catch (Exception e)
            {
                throw new ApplicationCoreException($"Could not parse Token", e);
            }

            var user = await this._userManager.FindByIdAsync(confirmationBody.userId);
            if (user == null)
                throw new ApplicationCoreException(ErrorMesage.UnabletoloaduserwithID, $"{confirmationBody.userId}");

            var result = await this._userManager.ConfirmEmailAsync(user, code);

            return this.Ok(result.Succeeded);
        }

/// <summary>
/// 
/// </summary>
/// <param name="Token"></param>
/// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public CurrentUser CurrentUserInfo(string Token)
        {
            try
            {
                if (User?.Identity == null || !User.Identity.IsAuthenticated)
                {
                    var principal = GetPrincipalFromExpiredToken(Token);
                    var username = principal.FindFirstValue(ClaimTypes.Email);
                    return new CurrentUser
                    {
                        IsAuthenticated = true,
                        UserName = username,
                        Claims = principal.Claims.Select(item => new KeyValuePair<string, string>(item.Type, item.Value)).ToList()
                    };
                }
            }
            catch
            {
                if (User?.Identity == null)
                {
                    return new CurrentUser
                    {
                        IsAuthenticated = false,
                        UserName = string.Empty,
                        Claims = null
                    };
                }
            }

            return new CurrentUser
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                UserName = User.Claims.SingleOrDefault(p => p.Type.Contains("emailaddress"))?.Value,
                Claims = _httpContextAccessor.HttpContext?.User.Claims
                    .Select(item => new KeyValuePair<string, string>(item.Type, item.Value)).ToList()
            };
        }

     

        /// <summary>
        /// External Logins - Google, Facebook, Twitter
        /// </summary>
        /// <param name="model">
        /// The external login view model.
        /// </param>
        /// <returns>
        /// Access Token 
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalLoginViewModel model)
        {

            if(model.Provider == "Facebook")
            {
                var httpClient = _httpClientFactory.CreateClient();
                string appId = _configuration["Authentication:Facebook:AppId"];
                string appSecret = _configuration["Authentication:Facebook:AppSecret"];

                // generate an app access token
                var appAccessRequest = $"https://graph.facebook.com/oauth/access_token?client_id={appId}&client_secret={appSecret}&grant_type=client_credentials";
                var appAccessTokenResponse = await httpClient.GetFromJsonAsync<FacebookAppAccessToken>(appAccessRequest);
               
                // validate the user access token
                var userAccessValidationRequest = $"https://graph.facebook.com/debug_token?input_token={HttpUtility.UrlEncode(model.ExternalId)}&access_token={HttpUtility.UrlEncode(appAccessTokenResponse.Access_Token)}";
                var userAccessTokenValidationResponse = await httpClient.GetFromJsonAsync<FacebookUserAccessTokenValidation>(userAccessValidationRequest);

                if (!userAccessTokenValidationResponse.Data.Is_Valid)
                    return BadRequest();

                //  we've got a valid token so we can request user data from facebook

                var userDataRequest = $"https://graph.facebook.com/v11.0/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={HttpUtility.UrlEncode(model.ExternalId)}";
                var facebookUserData = await httpClient.GetFromJsonAsync<FacebookUserData>(userDataRequest);

                model.Email = facebookUserData.Email;
                model.FistName = facebookUserData.first_name;
                model.LastName = facebookUserData.last_name;
            }
            if(model.Provider == "Google")
            {
                 var httpClient = _httpClientFactory.CreateClient();
                 var client_id = _configuration["Authentication:Google:ClientId"];
                 var clien_secret = _configuration["Authentication:Google:ClientSecret"];

                 StringContent content = new StringContent(
                     "client_id=" + client_id
                     + "&client_secret=" + clien_secret
                     + "&refresh_token=" + model.ExternalId
                     + "&grant_type=" + "refresh_token"
                     ); 
                 content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                 var response = await httpClient.PostAsync("https://accounts.google.com/o/oauth2/token", content);
                 var result = await response.Content.ReadAsStringAsync();

                 var stream = new MemoryStream(Encoding.Unicode.GetBytes(result));

                 var serializer = new DataContractJsonSerializer(typeof(GoogleAppAccessToken));

                 GoogleAppAccessToken googleAppAccessToken = (GoogleAppAccessToken)serializer.ReadObject(stream);

                 var query = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + HttpUtility.UrlEncode(googleAppAccessToken.AccessToken);

                 httpClient = _httpClientFactory.CreateClient();

                 string googleUser = await httpClient.GetStringAsync(query);

                 stream = new MemoryStream(UnicodeEncoding.Unicode.GetBytes(googleUser));
                 serializer = new DataContractJsonSerializer(typeof(GoogleUser));

                 GoogleUser googleUserModel = (GoogleUser)serializer.ReadObject(stream);

                 model.Email = googleUserModel.Email;
                 model.FistName = googleUserModel.GivenName;
                 model.LastName = googleUserModel.FamilyName;

            }

            if (!this.ModelState.IsValid)
            {
                var errorList = this.ModelState.ToDictionary(
                    x => x.Key.Replace("model.", string.Empty),
                    x => x.Value.Errors[0].ErrorMessage);

                return this.BadRequest(errorList);
            }

            var externalLoginInfo = new UserLoginInfo(model.Provider, model.ExternalId, model.Provider);
            var claims = new List<Claim>
                             {
                                 new(JwtRegisteredClaimNames.Email, model.Email),

                                 // Add provider claim so that Angular app can know provider for logout
                                 new("provider", model.Provider)
                             };

            var user = await this._userManager.FindByEmailAsync(model.Email);

            // User is already registered. Link existing account to external provider return access token
            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    user.EmailConfirmed = true;
                }

                user.RefreshToken = GenerateRefreshToken();
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddSeconds(Convert.ToDouble(_configuration["Security:Tokens:ExpirationSecondsInteger"]));
                await _userManager.UpdateAsync(user);
                var response = new AuthResponseDto
                {
                    Token = this.GenerateJwtTokenFromClaims(claims),
                    RefreshToken = user.RefreshToken,
                    IsConfiremd = true,
                    userId = user.Id,
                    IsAuthSuccessful = true
                };
                return Ok(await Result<AuthResponseDto>.SuccessAsync(response));

            }

            var registrationResponse = new AuthResponseDto
            {
                Token = model.ExternalId,
                RefreshToken = "",
                IsConfiremd = true,
                userId = null,
                IsAuthSuccessful = false
            };
            return Ok(await Result<AuthResponseDto>.FailAsync(registrationResponse, "Redirecting to complete register."));
           
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetFacebookAppId() 
        {
            return Ok(await Result<string>.SuccessAsync(_configuration["Authentication:Facebook:AppId"],"The facebook app id"));

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetGoogleAppId()
        {
            return Ok(await Result<string>.SuccessAsync(_configuration["Authentication:Google:ClientId"], "The Google app id"));

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateAccessToken([FromBody] ValidateAccessTokenDto model)
        {
            if(model.Provider =="Facebook")
            {
                var httpClient = _httpClientFactory.CreateClient();
                string appId = _configuration["Authentication:Facebook:AppId"];
                string appSecret = _configuration["Authentication:Facebook:AppSecret"];

                // generate an app access token
                var appAccessRequest = $"https://graph.facebook.com/oauth/access_token?client_id={appId}&client_secret={appSecret}&grant_type=client_credentials";
                var appAccessTokenResponse = await httpClient.GetFromJsonAsync<FacebookAppAccessToken>(appAccessRequest);

                // validate the user access token
                var userAccessValidationRequest = $"https://graph.facebook.com/debug_token?input_token={HttpUtility.UrlEncode(model.AuterizationCode)}&access_token={HttpUtility.UrlEncode(appAccessTokenResponse.Access_Token)}";
                var userAccessTokenValidationResponse = await httpClient.GetFromJsonAsync<FacebookUserAccessTokenValidation>(userAccessValidationRequest);

                if (!userAccessTokenValidationResponse.Data.Is_Valid)
                    return BadRequest();

                //  we've got a valid token so we can request user data from facebook

                var userDataRequest = $"https://graph.facebook.com/v11.0/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={HttpUtility.UrlEncode(model.AuterizationCode)}";
                var facebookUserData = await httpClient.GetFromJsonAsync<FacebookUserData>(userDataRequest);

                model.Email = facebookUserData.Email;
                model.FirstName = facebookUserData.first_name;
                model.LastName = facebookUserData.last_name;

                var user = await this._userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    model.IsTheUserExist = true;
                }
                else
                {
                    model.IsTheUserExist = false;
                }

                model.IsValid = true;

                return Ok(await Result<ValidateAccessTokenDto>.SuccessAsync(model));
            }
            else if (model.Provider == "Google")
            {
                var httpClient = _httpClientFactory.CreateClient();
                var client_id = _configuration["Authentication:Google:ClientId"];
                var clien_secret = _configuration["Authentication:Google:ClientSecret"];
                var redirect_uri = _configuration["Authentication:Google:redirect_uri"];
                //var redirect_localUri = _configuration["Authentication:Google:redirect_localUri"];



                StringContent content = new StringContent(
                    "code=" + model.AuterizationCode
                    + "&client_id=" + client_id
                    + "&client_secret=" + clien_secret
                    + "&redirect_uri=" + redirect_uri
                    + "&grant_type=" + "authorization_code"
                    + "&access_type=offline&prompt=consent"
                    );
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var response = await httpClient.PostAsync("https://accounts.google.com/o/oauth2/token", content);
                var result = await response.Content.ReadAsStringAsync();

                var stream = new MemoryStream(Encoding.Unicode.GetBytes(result));

                var serializer = new DataContractJsonSerializer(typeof(GoogleAppAccessToken));

                GoogleAppAccessToken googleAppAccessToken = (GoogleAppAccessToken)serializer.ReadObject(stream);

                var query = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + HttpUtility.UrlEncode(googleAppAccessToken.AccessToken);

                httpClient = _httpClientFactory.CreateClient();
                
                string googleUser = await httpClient.GetStringAsync(query);

                stream = new MemoryStream(UnicodeEncoding.Unicode.GetBytes(googleUser));
                serializer = new DataContractJsonSerializer(typeof(GoogleUser));

                GoogleUser googleUserModel = (GoogleUser)serializer.ReadObject(stream);

                model.Email = googleUserModel.Email;
                model.FirstName = googleUserModel.GivenName;
                model.LastName = googleUserModel.FamilyName;
                model.RefreshToken = googleAppAccessToken.RefreshToken;

                var user = await this._userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    model.IsTheUserExist = true;
                }
                else
                {
                    model.IsTheUserExist = false;
                }

                model.IsValid = true;

                return Ok(await Result<ValidateAccessTokenDto>.SuccessAsync(model));

            }
            else
            {
                return Ok(await Result<ValidateAccessTokenDto>.FailAsync(model,"Missing provider"));

            }


        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterWithExternalProvider([FromBody] ValidateAccessTokenDto model)
        {

            var user = new User
            {
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
                
            };

             await this._userManager.CreateAsync(user,model.Password);


             await createNewProfileForNewUsers(user.Email);
            
            

            return Ok();

        }

        private async Task<Profile> createNewProfileForNewUsers(string email) 
        {
            var user = await this._userManager.FindByEmailAsync(email);


            var location = new Location()
            {
                Owner = user,
                OwnerId = user.Id,
            };

            await this._unitOfWork.GetRepository<Location>().AddAsync(location);


            var address = new Address();
            address.Owner = user;
            address.OwnerId = user.Id;
            address.LocationId = location.Id;
            address.Location = location;
            await this._unitOfWork.GetRepository<Address>().AddAsync(address);

            var profileImg = new ProfileImage()
            {
                OwnerId = user.Id,
                Owner = user,
                LocalPath = "/AppData/Users/ProfilePictures/",
                FileName = "DefaultProfileImg",
                FileExtension = "jpg"
            };

            await this._unitOfWork.GetRepository<ProfileImage>().AddAsync(profileImg);


            var profile = new Profile()
            {
                AddressId = address.Id,
                Address = address,
                ProfileImageId = profileImg.Id,
                ProfileImage = profileImg



            };

            await this._unitOfWork.GetRepository<Profile>().AddAsync(profile);

            user.Profile = profile;
            user.ProfileId = profile.Id;

            await this._userManager.UpdateAsync(user);

            return profile;

        }



        /// <summary>
        /// Forget password command
        /// </summary>
        /// <param name="model">
        /// The forgot password view model.
        /// </param>
        /// <returns>
        /// Success or bad request.
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            //if (!this.ModelState.IsValid)
            //{
            //    var errorList = this.ModelState.ToDictionary(
            //        x => x.Key.Replace("model.", string.Empty),
            //        x => x.Value.Errors[0].ErrorMessage);

            //    return this.BadRequest(errorList);
            //}

            var user = await this._userManager.FindByEmailAsync(model.Email);
            if (user == null || !await this._userManager.IsEmailConfirmedAsync(user)) return this.BadRequest();

            string code = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(await this._userManager.GeneratePasswordResetTokenAsync(user)));

            var callbackUrl = QueryHelpers.AddQueryString(
                model.RedirectUrl,
                new Dictionary<string, string?>
                    {
                        { "userId", user.Id.ToString() },
                        //{ "password", WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(model.NewPassword)) },
                        { "token", code }
                    });

            var subject = _localizer["ForgetPasswordTitle"];
            var resetMsg = _localizer["ForgetPasswordMsgBody"];
            var assistantMessage = _localizer["ForgetPasswordAssistantMessage"];
            var welcome = _localizer["WelcomeForgetPassword"];
            var confirmtext = _localizer["ForgetPasswordPleaseEmailConfirm"];

            IRazorHtmlModel confirmAccountModel = new ForgetPasswordEmailViewModel(user.Id.ToString(), $"{callbackUrl}", confirmtext, welcome, resetMsg, assistantMessage);

            string body = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/ForgetPassword/ForgetPasswordAccountEmail.cshtml", confirmAccountModel);


            this._backgroundJobService.Enqueue(() =>  this._emailSender.SendHtmlEmailAsync(
                model.Email,
                subject, body));

            var response = new AuthResponseDto
            {
                Token = null,
                IsConfiremd = true,
                userId = user.Id,
                IsAuthSuccessful = true
            };

            //return this.Ok();
            return Ok(await Result<AuthResponseDto>.SuccessAsync(response));
        }

        /// <summary>
        /// Login command
        /// </summary>
        /// <param name="model">
        /// login view model 
        /// </param>
        /// <returns>
        /// Success log or bad request.
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var errorList = this.ModelState.ToDictionary(
                    x => x.Key.Replace("model.", string.Empty),
                    x => x.Value.Errors[0].ErrorMessage);

                return this.BadRequest(errorList);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Ok(await Result<AuthResponseDto>.FailAsync(_localizer["User Not Found."]));
            }
           
            if (!user.EmailConfirmed)
            {
                return Ok(await Result<AuthResponseDto>.FailAsync(new AuthResponseDto { IsConfiremd = false, userId = user.Id, IsAuthSuccessful = false },_localizer["E-Mail not confirmed."]));
            }
            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                return Ok(await Result<AuthResponseDto>.FailAsync(_localizer["Invalid Credentials."]));
            }

           
           
            var claims = await GetClaimsAsync(user);
            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddSeconds(Convert.ToDouble(_configuration["Security:Tokens:ExpirationSecondsInteger"]));
            await _userManager.UpdateAsync(user);
            var response = new AuthResponseDto
            {
                Token = this.GenerateJwtTokenFromClaims(claims),
                RefreshToken= user.RefreshToken,
                IsConfiremd = true,
                userId = user.Id,
                IsAuthSuccessful = true
            };
            return Ok( await Result<AuthResponseDto>.SuccessAsync(response));
         
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto tokenDto)
        {
            if (tokenDto is null)
            {
                return BadRequest(await Result<AuthResponseDto>.FailAsync(new AuthResponseDto { IsAuthSuccessful = false, ErrorMessage = _localizer["Invalid client request"] }, _localizer["Invalid client request"]));
            }
            var principal = GetPrincipalFromExpiredToken(tokenDto.Token);
            var username = principal.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(username);
            if (user == null || user.RefreshToken != tokenDto.RefreshToken )
                return BadRequest(await Result<AuthResponseDto>.FailAsync(new AuthResponseDto { IsAuthSuccessful = false, ErrorMessage = _localizer["Invalid client request"] }, _localizer["Invalid client request"]));

           
         
            var claims = await GetClaimsAsync(user);
  
            var token = GenerateJwtTokenFromClaims(claims);

            user.RefreshToken = GenerateRefreshToken();
            await _userManager.UpdateAsync(user);
            return Ok(await Result<AuthResponseDto>.SuccessAsync(new AuthResponseDto { Token = token, RefreshToken = user.RefreshToken, IsAuthSuccessful = true }));
        }
        
      
      
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Security:Tokens:Key"])),
            
                ValidateLifetime = false,
                ValidIssuer = _configuration["Security:Tokens:Issuer"],
                ValidAudience = _configuration["Security:Tokens:Audience"],
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            var permissionClaims = new List<Claim>();
            var approles = _roleManager.Roles;
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
                var thisRole = approles.FirstOrDefault(p=>p.Name.Contains(role));
                var allPermissionsForThisRoles = await _roleManager.GetClaimsAsync(thisRole);
                permissionClaims.AddRange(allPermissionsForThisRoles);
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.FirstName),
                new(ClaimTypes.Surname, user.LastName),
                new(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty)
            }
            .Union(userClaims)
            .Union(roleClaims)
            .Union(permissionClaims);

            return claims;
        }
        /// <summary>
        ///     The log out authenticated user.
        /// </summary>
        /// <returns>Ok with empty body if user is local. Ok with provider name if user is logged in with external provider</returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await this._signInManager.SignOutAsync();
            this._logger.LogInformation("User logged out.");

            return this.Ok();
        }

        /// <summary>
        /// register new user.
        /// </summary>
        /// <param name="model">
        /// The register view model.
        /// </param>
        /// <returns>
        /// Success if user registered correctly, otherwise bad request.
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var errorList = this.ModelState.ToDictionary(
                    x => x.Key.Replace("model.", string.Empty),
                    x => x.Value.Errors[0].ErrorMessage);

                return this.BadRequest(errorList);
            }

            var user = new User
                           {
                               UserName = model.Email,
                               FirstName = model.FirstName,
                               LastName = model.LastName,
                               Email = model.Email
                           };
            var result = this._userManager.CreateAsync(user, model.Password).Result;
            if (!result.Succeeded)
                return this.BadRequest("User Already Exists With This Email :" + model.Email);
                    //new RegistrationResponseDto { IsSuccessfulRegistration = false, Errors = result.Errors });

            this._logger.LogInformation("User created a new account with password.");

            string code = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(await this._userManager.GenerateEmailConfirmationTokenAsync(user)));
            if (model.CallbackUrl.Contains("login"))
                model.CallbackUrl = model.CallbackUrl.Replace("login", "confirmation");
            var callbackUrl = QueryHelpers.AddQueryString(
                 model.CallbackUrl,
                 new Dictionary<string, string?>
                     {
                        { "Id", user.Id.ToString() },
                       
                        { "Token", code }
                     });
           
            var subject = _localizer["RegisterAccountTitle"];
            var resetMsg = _localizer["RegisterAccountMsgBody"];
            var assistantMessage = _localizer["RegisterAccountAssistantMessage"];
            var welcome = _localizer["RegisterAccountWelcome"];
            var confirmtext = _localizer["RegisterAccountPleaseEmailConfirm"];

            //IRazorHtmlModel confirmAccountModel = new RegisterWithTokenEmailViewModel(user.Id.ToString(), $"{code}", welcome, resetMsg, assistantMessage, model?.FirstName, model?.LastName);
            IRazorHtmlModel confirmAccountModel = new AccountGenericEmailViewModel(user.Id.ToString(), $"{callbackUrl}", confirmtext, welcome, resetMsg, assistantMessage);

            string body = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/RegisterWithToken/RegisterWithTokenAccountEmail.cshtml", confirmAccountModel);


            this._backgroundJobService.Enqueue(() => this._emailSender.SendHtmlEmailAsync(
                                model.Email,subject, body));

            // Rollback if email doesn't sent may change the interface of emailSender

            await createNewProfileForNewUsers(user.Email);



            return this.Ok(new RegistrationResponseDto { IsSuccessfulRegistration = true, userId = user.Id });
        }

        /// <summary>
        /// Remove the login for authenticated user
        /// </summary>
        /// <param name="model">
        /// The remove login view model
        /// </param>
        /// <returns>
        /// Ok 
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> RemoveLogin(RemoveLoginViewModel model)
        {
            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
                throw new ApplicationCoreException(
                    ErrorMesage.UnabletoloaduserwithID,
                    $"{this._userManager.GetUserId(this.User)}");

            var result = await this._userManager.RemoveLoginAsync(user, model.LoginProvider, model.ProviderKey);
            if (!result.Succeeded)
                throw new ApplicationCoreException(
                    ErrorMesage.UnexpectederroroccurredremovingloginforuserwithID,
                    $"{user.Id}");

            await this._signInManager.SignInAsync(user, false);

            return this.Ok();
        }

        /// <summary>
        /// the reset password command
        /// </summary>
        /// <param name="userId">
        /// The user Id. 
        /// </param>
        /// <param name="password">
        /// The password. 
        /// </param>
        /// <param name="token">
        /// The token. 
        /// </param>
        /// <returns>
        /// Success if reset is ok, otherwise bad request. 
        /// </returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ResetPassword(Guid userId, string password, string token)
        {
            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            //var newPassword = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(password));
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null) 
                return BadRequest();

            // the token comes from the reset password action
            var result = await _userManager.ResetPasswordAsync(user, code, password);
            await _userManager.UpdateAsync(user);
            //return result.Succeeded ? Ok() : BadRequest();
            var response = new AuthResponseDto
            {
                Token = null,
                IsConfiremd = true,
                userId = user.Id,
                IsAuthSuccessful = true
            };
            return Ok(await Result<AuthResponseDto>.SuccessAsync(response));
        }

        /// <summary>
        /// Send verification email to use.
        /// </summary>
        /// <param name="userId">
        /// The user identifier.
        /// </param>
        /// <returns>
        /// Success if email sent, otherwise Exception.
        /// </returns>
        [HttpGet("{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> SendVerificationEmail(Guid userId, string RedirectUrl)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new ApplicationCoreException(ErrorMesage.UnabletoloaduserwithID);

            string code = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(await this._userManager.GenerateEmailConfirmationTokenAsync(user)));
            
            var callbackUrl = QueryHelpers.AddQueryString(
                RedirectUrl,
                new Dictionary<string, string?>
                    {
                        { "Token", code }
                    });

            var subject = _localizer["VerificationAccountTitle"];
            var resetMsg = _localizer["VerificationAccountMsgBody"];
            var assistantMessage = _localizer["VerificationAccountMessage"];
            var welcome = _localizer["VerificationAccountWelcome"];
            var confirmtext = _localizer["VerificationAccountPleaseEmailConfirm"];

            //IRazorHtmlModel confirmAccountModel = new AccountGenericEmailViewModel(userId.ToString(), $"{code}", confirmtext, welcome, resetMsg, assistantMessage);
            IRazorHtmlModel confirmAccountModel = new AccountGenericEmailViewModel(userId.ToString(), $"{callbackUrl}", confirmtext, welcome, resetMsg, assistantMessage);

            string body = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/ResendAccountVerification/ResendAccountVerificationGenericEmail.cshtml", confirmAccountModel);

            this._backgroundJobService.Enqueue(() => this._emailSender.SendHtmlEmailAsync(user.Email, subject,body));

            return this.Ok();
        }


        /// <summary>
        /// Send verification email to use.
        /// </summary>
        /// <param name="model">
        /// The user email.
        /// </param>
        /// <returns>
        /// Success if email sent, otherwise Exception.
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendVerificationEmailByEmail([FromBody] ForgotPasswordViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return Ok(await Result<AuthResponseDto>.FailAsync("Invalid Email"));
            }

            var user = await this._userManager.FindByEmailAsync(model.Email);
            if (user == null || !await this._userManager.IsEmailConfirmedAsync(user)) return this.BadRequest();

            string code = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(await this._userManager.GeneratePasswordResetTokenAsync(user)));

            var callbackUrl = QueryHelpers.AddQueryString(
                model.RedirectUrl,
                new Dictionary<string, string?>
                    {
                        { "userId", user.Id.ToString() },
                          { "email", user.Email },
                        { "token", code }
                    });


            var subject = _localizer["VerificationAccountTitle"];
            var resetMsg = _localizer["VerificationAccountMsgBody"];
            var assistantMessage = _localizer["VerificationAccountMessage"];
            var welcome = _localizer["VerificationAccountWelcome"];
            var confirmtext = _localizer["VerificationAccountPleaseEmailConfirm"];

            IRazorHtmlModel confirmAccountModel = new AccountGenericEmailViewModel(user.Id.ToString(), $"{callbackUrl}", confirmtext, welcome, resetMsg, assistantMessage);

            string body = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/ConfirmAccount/AccountGenericEmail.cshtml", confirmAccountModel);


            this._backgroundJobService.Enqueue(() => this._emailSender.SendHtmlEmailAsync(
                user.Email, subject, body));


            this._backgroundJobService.Enqueue(() => this._emailSender.SendHtmlEmailAsync(
                model.Email, subject, body));
        
            var response = new AuthResponseDto
            {
                Token = null,
                IsConfiremd = true,
                userId = user.Id,
                IsAuthSuccessful = true
            };
            return Ok(await Result<AuthResponseDto>.SuccessAsync(response));
        }

        /// <summary>
        /// Send verification email to use.
        /// </summary>
        /// <param name="model">
        /// The user email.
        /// </param>
        /// <returns>
        /// Success if email sent, otherwise Exception.
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendVerificationEmailByEmailTwo([FromBody] ForgotPasswordViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return Ok(await Result<AuthResponseDto>.FailAsync("Invalid Email"));
            }

            var user = await this._userManager.FindByEmailAsync(model.Email);
            if (user == null || !await this._userManager.IsEmailConfirmedAsync(user)) return this.BadRequest();

            string code = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(await this._userManager.GeneratePasswordResetTokenAsync(user)));

            var callbackUrl = QueryHelpers.AddQueryString(
                model.RedirectUrl,
                new Dictionary<string, string?>
                    {
                        { "userId", user.Id.ToString() },
                          { "email", user.Email },
                        { "token", code }
                    });
            var subject = _localizer["ResetPasswordTitle"];
            var resetMsg = _localizer["ResetPasswordMsgBody"];
            var assistantMessage = _localizer["AssistantMessage"];
            var welcome = _localizer["Welcome"];
            var confirmtext =  _localizer["PleaseEmailConfirm"];

            IRazorHtmlModel confirmAccountModel = new AccountGenericEmailViewModel(user.Id.ToString(),$"{callbackUrl}", confirmtext, welcome, resetMsg, assistantMessage);

            string body = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/ConfirmAccount/AccountGenericEmail.cshtml", confirmAccountModel);


            this._backgroundJobService.Enqueue(() => this._emailSender.SendHtmlEmailAsync(
                 model.Email,
                subject,
                body));
            var response = new AuthResponseDto
            {
                Token = null,
                IsConfiremd = true,
                userId = user.Id,
                IsAuthSuccessful = true
            };
            return Ok(await Result<AuthResponseDto>.SuccessAsync(response));
        }
        /// <summary>
        /// Send verification email to use.
        /// </summary>
        /// <param name="email">
        /// The user email.
        /// </param>
        /// <returns>
        /// Success if email sent, otherwise Exception.
        /// </returns>
        [HttpGet("{email}")]
        [AllowAnonymous]
        public async Task<IActionResult> SendVerificationEmailByEmail(string email)
        {
            var user = await this._userManager.FindByEmailAsync(email);
            if (user == null) throw new ApplicationCoreException(ErrorMesage.UnabletoloaduserwithID);

            string code = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(await this._userManager.GenerateEmailConfirmationTokenAsync(user)));
            var subject = _localizer["VerificationAccountTitle"];
            var resetMsg = _localizer["VerificationAccountMsgBody"];
            var assistantMessage = _localizer["VerificationAccountMessage"];
            var welcome = _localizer["VerificationAccountWelcome"];
            var confirmtext = _localizer["VerificationAccountPleaseEmailConfirm"];

            IRazorHtmlModel confirmAccountModel = new AccountGenericEmailViewModel(user.Id.ToString(), $"{code}", confirmtext, welcome, resetMsg, assistantMessage);

            string body = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/ConfirmAccount/AccountGenericEmail.cshtml", confirmAccountModel);


            this._backgroundJobService.Enqueue(() => this._emailSender.SendHtmlEmailAsync(
                user.Email, subject, body));

            return this.Ok();
        }        
        /// <summary>
        /// Set the password command authenticated user
        /// </summary>
        /// <param name="model">
        /// The set password model 
        /// </param>
        /// <returns>
        /// Action result 
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var errorList = this.ModelState.ToDictionary(
                    x => x.Key.Replace("model.", string.Empty),
                    x => x.Value.Errors[0].ErrorMessage);

                return this.BadRequest(errorList);
            }

            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null) throw new ApplicationCoreException(ErrorMesage.UnabletoloaduserwithID);

            var addPasswordResult = await this._userManager.AddPasswordAsync(user, model.NewPassword);
            if (!addPasswordResult.Succeeded) return this.BadRequest(model);

            await this._signInManager.SignInAsync(user, false);

            return this.Ok();
        }

        private string GenerateJwtTokenFromClaims(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Security:Tokens:Key"]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                this._configuration["Security:Tokens:Issuer"],
                this._configuration["Security:Tokens:Issuer"],
                claims,
                expires: DateTime.UtcNow.AddSeconds(
                    Convert.ToDouble(this._configuration["Security:Tokens:ExpirationSecondsInteger"])),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // [HttpPost]
        // [AllowAnonymous]
        // public IActionResult ExternalLogin(string provider, string returnUrl = null)
        // {
        // // Request a redirect to the external login provider.
        // var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
        // var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        // return Challenge(properties, provider);
        // }

        // [HttpGet]
        // [AllowAnonymous]
        // public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        // {
        // if (remoteError != null)
        // {
        // ErrorMessage = $"Error from external provider: {remoteError}";
        // return RedirectToAction(nameof(Login));
        // }
        // var info = await _signInManager.GetExternalLoginInfoAsync();
        // if (info == null)
        // {
        // return RedirectToAction(nameof(Login));
        // }

        // // Sign in the user with this external login provider if the user already has a login.
        // var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        // if (result.Succeeded)
        // {
        // _logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
        // return RedirectToLocal(returnUrl);
        // }
        // if (result.IsLockedOut)
        // {
        // return RedirectToAction(nameof(Lockout));
        // }
        // else
        // {
        // // If the user does not have an account, then ask the user to create an account.
        // ViewData["ReturnUrl"] = returnUrl;
        // ViewData["LoginProvider"] = info.LoginProvider;
        // var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        // return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
        // }
        // }

        // [HttpPost]
        // [AllowAnonymous]
        // public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        // {
        // if (ModelState.IsValid)
        // {
        // // Get the information about the user from the external login provider
        // var info = await _signInManager.GetExternalLoginInfoAsync();
        // if (info == null)
        // {
        // throw new ApplicationException("Error loading external login information during confirmation.");
        // }
        // var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        // var result = await _userManager.CreateAsync(user);
        // if (result.Succeeded)
        // {
        // result = await _userManager.AddLoginAsync(user, info);
        // if (result.Succeeded)
        // {
        // await _signInManager.SignInAsync(user, isPersistent: false);
        // _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
        // return RedirectToLocal(returnUrl);
        // }
        // }
        // AddErrors(result);
        // }

        // ViewData["ReturnUrl"] = returnUrl;
        // return View(nameof(ExternalLogin), model);
        // }

        // [HttpGet]
        // public async Task<IActionResult> ExternalLogins()
        // {
        // var user = await _userManager.GetUserAsync(User);
        // if (user == null)
        // {
        // throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        // }

        // var model = new ExternalLoginsViewModel { CurrentLogins = await _userManager.GetLoginsAsync(user) };
        // model.OtherLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync())
        // .Where(auth => model.CurrentLogins.All(ul => auth.Name != ul.LoginProvider))
        // .ToList();
        // model.ShowRemoveButton = await _userManager.HasPasswordAsync(user) || model.CurrentLogins.Count > 1;
        // model.StatusMessage = StatusMessage;

        // return View(model);
        // }

        // [HttpPost]
        // public async Task<IActionResult> LinkLogin(string provider)
        // {
        // // Clear the existing external cookie to ensure a clean login process
        // await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        // // Request a redirect to the external login provider to link a login for the current user
        // var redirectUrl = Url.Action(nameof(LinkLoginCallback));
        // var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));
        // return new ChallengeResult(provider, properties);
        // }

        // [HttpGet]
        // public async Task<IActionResult> LinkLoginCallback()
        // {
        // var user = await _userManager.GetUserAsync(User);
        // if (user == null)
        // {
        // throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        // }

        // var info = await _signInManager.GetExternalLoginInfoAsync(user.Id);
        // if (info == null)
        // {
        // throw new ApplicationException($"Unexpected error occurred loading external login info for user with ID '{user.Id}'.");
        // }

        // var result = await _userManager.AddLoginAsync(user, info);
        // if (!result.Succeeded)
        // {
        // throw new ApplicationException($"Unexpected error occurred adding external login for user with ID '{user.Id}'.");
        // }

        // // Clear the existing external cookie to ensure a clean login process
        // await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        // StatusMessage = "The external login was added.";
        // return RedirectToAction(nameof(ExternalLogins));
        // }
    }

    internal class FacebookUserData
    {
        public string Email { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

    }

    internal class FacebookUserAccessTokenValidation
    {
        public Data Data { get; set; }

    }

    internal class Data
    {
        public bool Is_Valid { get; set; }
    }

    internal class FacebookAppAccessToken
    {
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
    }

    [DataContract]
    internal class GoogleAppAccessToken
    {
        [DataMember(Name = "access_token")] public string AccessToken { get; set; }
        [DataMember(Name = "token_type")] public string TokenType { get; set; }
        [DataMember(Name = "expires_in")] public string ExpiresIn { get; set; }
        [DataMember(Name = "id_token")] public string IdToken { get; set; }
        [DataMember(Name = "refresh_token")] public string RefreshToken { get; set; }
    }

    [DataContract]
    internal class GoogleUser
    {
        [DataMember(Name = "id")] public string Id { get; set; }
        [DataMember(Name = "email")] public string Email { get; set; }
        [DataMember(Name = "name")] public string Name { get; set; }
        [DataMember(Name = "given_name")] public string GivenName { get; set; }
        [DataMember(Name = "family_name")] public string FamilyName { get; set; }
        [DataMember(Name = "link")] public string Link { get; set; }
        [DataMember(Name = "picture")] public string PictureUri { get; set; }
        [DataMember(Name = "gender")] public string Gender { get; set; }
        [DataMember(Name = "locale")] public string Locale { get; set; }
    }
}