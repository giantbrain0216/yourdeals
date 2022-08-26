// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginVM.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using MvvmBlazor.ViewModel;
using NPComplet.YourDeals.Client.Shared.Helpers;
using NPComplet.YourDeals.Client.Shared.Logic.Interfaces;
using NPComplet.YourDeals.Client.Shared.ViewModels.Base;
using NPComplet.YourDeals.Client.Shared.ViewModels.Constant;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.ExternalLogins;
using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Domain.Shared.Wrapper;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Identity
{
    #region

    #endregion

    /// <summary>
    ///     The login vm.
    /// </summary>
    public class LoginVM : ViewModelBase
    {
        private const string V = "/";
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _options;
        private string facebookAccessToken;
        private string googleAccessToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginVM"/> class.
        /// </summary>
        /// <param name="authService">
        /// The authentication service.
        /// </param>
        /// <param name="navMan">
        /// The nav man.
        /// </param>
        /// <param name="appShellViewModel">
        /// The app shel view model.
        /// </param>
        public LoginVM(
            IAuthenticationService authService,
            NavigationManager navMan,
            AppShellViewModel appShellViewModel, HttpClient httpClient)
        {
            this.AuthenticationService = authService;
            this.NavigationManager = navMan;
            NPCompletApp.ShellViewModel = appShellViewModel;
            this._options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));


        }



        /// <summary>
        ///     Gets or sets the authentication service.
        /// </summary>
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        /// <summary>
        ///     Gets or sets the copied token.
        /// </summary>
        [Required]
        public string CopiedToken { get; set; }

        /// <summary>
        ///     Gets or sets the error.
        /// </summary>
        public string Error { get; set; }
        public bool Success { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether loading.
        /// </summary>
        public bool Loading { get; set; }

        /// <summary>
        ///     Gets or sets the navigation manager.
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether show auth error.
        /// </summary>
        public bool ShowAuthError { get; set; }

        /// <summary>
        ///     Gets or sets the user for authentication.
        /// </summary>
        public LoginViewModel UserForAuthentication { get; set; } = new LoginViewModel();

        /// <summary>
        ///     Gets or sets the user for registration.
        /// </summary>
        public RegisterViewModel UserForRegistration { get; set; } = new RegisterViewModel();

        public ValidateAccessTokenDto ValidateFacebookAccessTokenDto { get; set; } = new ValidateAccessTokenDto("Facebook");

        public ValidateAccessTokenDto ValidateGoogleAccessTokenDto { get; set; } = new ValidateAccessTokenDto("Google");

        public ExternalLoginViewModel UserForAuthenticationExternal { get; set; } = new ExternalLoginViewModel();

        /// <summary>
        /// The execute confirm.
        /// </summary>
        /// <param name="eventCallback">
        /// The event callback.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task ExecuteConfirm(EventCallback<bool> eventCallback, string id)
        {
            this.Loading = true;
            NPCompletApp.ShellViewModel.isBusy = true;
            this.ShowAuthError = false;
            var confirmationBody = new ConfirmationBody { codedToken = this.CopiedToken.Trim(), userId = id };
            var result = await this.AuthenticationService.ConfirmEmail(confirmationBody);
            if (!result)
            {
                this.Loading = false;
                await eventCallback.InvokeAsync(result);
            }
            else
            {
                this.Loading = false;


                this.NavigationManager.NavigateTo("/login", true);
                await eventCallback.InvokeAsync(result);
            }

            this.Loading = false;
            NPCompletApp.ShellViewModel.isBusy = false;
        }
        #region Paramters
       
        private string _refreshToken;

        [Parameter]
        public string RefreshToken
        {
            get { return _refreshToken; }
            set
            {
                _refreshToken = value;
                OnPropertyChanged(nameof(RefreshToken));
            }
        }

        [Parameter]
        public EventCallback<Result<ValidateAccessTokenDto>> OnRegisterWithGoogle { get; set; }

        private string _accessToken;
        [Parameter]
        public string AccessToken
        {
            get { return _accessToken; }
            set
            {
                _accessToken = value;
                OnPropertyChanged(nameof(AccessToken));
            }
        }


        
        private string _email;
        [Parameter]
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }



        private string _firstName;
        [Parameter]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }



        private string _lastName;
        [Parameter]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }



        [Parameter]
        public EventCallback<Result<ValidateAccessTokenDto>> OnRegisterWithFacebook { get; set; }
        [Parameter]
        public EventCallback<RegistrationResponseDto> OnRegister { get; set; }


        [Parameter]
        public EventCallback<Result<AuthResponseDto>> OnLoginWithGoogle { get; set; }


        [Parameter]
        public EventCallback<Result<AuthResponseDto>> OnLoginWithFacebook { get; set; }
        /// <summary>
        /// Gets or sets the on login.
        /// </summary>
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Parameter]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the on confirm.
        /// </summary>
        [Parameter]
        public EventCallback<bool> OnConfirm { get; set; }

        /// <summary>
        /// Gets or sets the back to register.
        /// </summary>
        [Parameter]
        public EventCallback BackToRegister { get; set; }
        //BackToLogin
        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public EventCallback BackToLogin { get; set; }
        [Parameter]
        public EventCallback<Result<AuthResponseDto>> OnLogin { get; set; }
        /// <summary>
        /// Gets or sets the go to register.
        /// </summary>
        [Parameter]
        public EventCallback GoToRegister { get; set; }



        /// <summary>
        /// Gets or sets the go to GoToForgetPassword.
        /// </summary>
        [Parameter]
        public EventCallback GoToForgetPassword { get; set; }
        #endregion


        /// <summary>
        /// The execute login.
        /// </summary>
        /// <param name="eventCallback">
        /// The event Callback.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task ExecuteLogin(EventCallback<Result<AuthResponseDto>> eventCallback)
        {
            NPCompletApp.ShellViewModel.isBusy = true;
            ShowAuthError = false;

            var result = await AuthenticationService.Login(UserForAuthentication);
            if (!result.Succeeded)
            {
                foreach (var msg in result.Messages)
                {
                    Error = $"{Error + msg}";
                }

                ShowAuthError = true;
                NPCompletApp.ShellViewModel.isLoggedIn = false;
                NPCompletApp.ShellViewModel.isBusy = false;
                await eventCallback.InvokeAsync(result);
                if (result.Data?.userId != null && !result.Data.IsConfiremd)
                    //NavigationManager.NavigateTo("/confirmation/" + result.Data.userId, true);
                    NavigationManager.NavigateTo("/confirmation?Id=" + result.Data.userId, true);
            }
            else
            {
                await NPCompletApp.ShellViewModel.GetUserStatus();
                NPCompletApp.ShellViewModel.isLoggedIn = true;
                NPCompletApp.ShellViewModel.isBusy = false;
                await eventCallback.InvokeAsync(result);
                NavigationManager.NavigateTo(V, true);
            }

            NPCompletApp.ShellViewModel.isBusy = false;
        }

        
        public async Task<string> GetFacebookAppId()
        {
            var result = await AuthenticationService.GetFacebookAppId();
            if (!result.Succeeded) 
            {
                foreach (var msg in result.Messages)
                {
                    Error = $"{Error+msg}";
                }

                ShowAuthError = true;
                NPCompletApp.ShellViewModel.isLoggedIn = false;
                NPCompletApp.ShellViewModel.isBusy = false;
                NavigationManager.NavigateTo("/login/", true);

                return result.Data;

            }
            else
            {
                return result.Data;


            }     
        }

        public async Task<string> GetGoogleAppId()
        {
            var result = await AuthenticationService.GetGoogleAppId();
            if (!result.Succeeded)
            {
                foreach (var msg in result.Messages)
                {
                    Error = $"{Error+msg}";
                }

                ShowAuthError = true;
                NPCompletApp.ShellViewModel.isLoggedIn = false;
                NPCompletApp.ShellViewModel.isBusy = false;
                NavigationManager.NavigateTo("/login/", true);

                return result.Data;

            }
            else
            {
                return result.Data;


            }
        }

        public  string GetTheRedirectUrl()
        {
            return Config.BaseWebAppUrl;
        }

        public async Task ValidateLoginWithFacebook(EventCallback<Result<AuthResponseDto>> eventCallback)
        {
            var result = await AuthenticationService.ValidateAccessToken(ValidateFacebookAccessTokenDto);

            if(!result.Succeeded)
            {
                foreach (var msg in result.Messages)
                {
                    Error = $"{Error + msg}";
                }

                ShowAuthError = true;
                NPCompletApp.ShellViewModel.isLoggedIn = false;
                NPCompletApp.ShellViewModel.isBusy = false;
                NavigationManager.NavigateTo("/login/", true);
            }
            else
            {
                if (!result.Data.IsTheUserExist)
                {
                    // redirect to registerFacebookUser
                    NPCompletApp.ShellViewModel.isBusy = true;

                    
                    NavigationManager.NavigateTo($"/RegisterWithFacebook/" + 
                        result.Data.AuterizationCode + V +
                        result.Data.Email + V +
                        result.Data.FirstName + V +
                        result.Data.LastName + V, true);

                }
                else
                {
                    // executeExternalLogin the user has been registered before.
                    UserForAuthenticationExternal.ExternalId = ValidateFacebookAccessTokenDto.AuterizationCode;
                    UserForAuthenticationExternal.Email = ValidateFacebookAccessTokenDto.Email;
                    UserForAuthenticationExternal.FistName = ValidateFacebookAccessTokenDto.FirstName;
                    UserForAuthenticationExternal.LastName = ValidateFacebookAccessTokenDto.LastName;
                    UserForAuthenticationExternal.Provider = "Facebook";

                    await ExecuteExternalLogin(eventCallback);

                }
            }
        }

        
        public async Task ExecuteRegisterWithFacebook(EventCallback<Result<ValidateAccessTokenDto>> eventCallback)
        {
            NPCompletApp.ShellViewModel.isBusy = true;
            ShowAuthError = false;

            var result = await AuthenticationService.RegisterWithExternalProvider(ValidateFacebookAccessTokenDto);
            if (!result.Succeeded)
            {
                foreach (var msg in result.Messages)
                {
                    Error = $"{Error + msg}";
                }

                ShowAuthError = true;
                NPCompletApp.ShellViewModel.isLoggedIn = false;
                NPCompletApp.ShellViewModel.isBusy = false;
                NavigationManager.NavigateTo("/login/" , true);
            }
            else
            {
                await NPCompletApp.ShellViewModel.GetUserStatus();
                NPCompletApp.ShellViewModel.isLoggedIn = true;
                NPCompletApp.ShellViewModel.isBusy = false;
                NavigationManager.NavigateTo(V, true);
            }

            NPCompletApp.ShellViewModel.isBusy = false;
        }


        public async Task ValidateLoginWithGoogle(EventCallback<Result<AuthResponseDto>> eventCallback)
        {
            var result = await AuthenticationService.ValidateAccessToken(ValidateGoogleAccessTokenDto);
            ValidateGoogleAccessTokenDto = result.Data;
            if (!result.Succeeded)
            {
                foreach (var msg in result.Messages)
                {
                    Error = $"{Error + msg}";
                }

                ShowAuthError = true;
                NPCompletApp.ShellViewModel.isLoggedIn = false;
                NPCompletApp.ShellViewModel.isBusy = false;
                NavigationManager.NavigateTo("/login/", true);
            }
            else
            {
                if (!result.Data.IsTheUserExist)
                {
                    // redirect to registerFacebookUser
                    NPCompletApp.ShellViewModel.isBusy = true;

                    NavigationManager.NavigateTo("/RegisterWithGoogle/" +
                         HttpUtility.UrlEncode(ValidateGoogleAccessTokenDto.RefreshToken) + V +
                        result.Data.Email + V +
                        result.Data.FirstName + V +
                        //result.Data.LastName + V, true);
                        result.Data.LastName, true);

                }
                else
                {
                    // executeExternalLogin the user has been registered before.
                    UserForAuthenticationExternal.ExternalId = ValidateGoogleAccessTokenDto.RefreshToken;
                    UserForAuthenticationExternal.Email = ValidateGoogleAccessTokenDto.Email;
                    UserForAuthenticationExternal.FistName = ValidateGoogleAccessTokenDto.FirstName;
                    UserForAuthenticationExternal.LastName = ValidateGoogleAccessTokenDto.LastName;
                    UserForAuthenticationExternal.Provider = ValidateGoogleAccessTokenDto.Provider;

                    await ExecuteExternalLogin(eventCallback);

                }
            }
        }


        public async Task ExecuteRegisterWithGoogle(EventCallback<Result<ValidateAccessTokenDto>> eventCallback)
        {
            NPCompletApp.ShellViewModel.isBusy = true;
            ShowAuthError = false;

            var result = await AuthenticationService.RegisterWithExternalProvider(ValidateGoogleAccessTokenDto);
            if (!result.Succeeded)
            {
                foreach (var msg in result.Messages)
                {
                    Error = $"{Error + msg}";
                }

                ShowAuthError = true;
                NPCompletApp.ShellViewModel.isLoggedIn = false;
                NPCompletApp.ShellViewModel.isBusy = false;
                NavigationManager.NavigateTo("/login/", true);
            }
            else
            {
                await NPCompletApp.ShellViewModel.GetUserStatus();
                NPCompletApp.ShellViewModel.isLoggedIn = true;
                NPCompletApp.ShellViewModel.isBusy = false;
                NavigationManager.NavigateTo(V, true);
            }

            NPCompletApp.ShellViewModel.isBusy = false;
        }

        public async Task ExecuteExternalLogin(EventCallback<Result<AuthResponseDto>> eventCallback)
        {
            var result = await AuthenticationService.LoginWithExternalProvider(UserForAuthenticationExternal);

            if (!result.Succeeded)
            {
                foreach (var msg in result.Messages)
                {
                    Error = $"{Error + msg}";
                }

                ShowAuthError = true;
                NPCompletApp.ShellViewModel.isLoggedIn = false;
                NPCompletApp.ShellViewModel.isBusy = false;
                await eventCallback.InvokeAsync(result);
                NavigationManager.NavigateTo("/login/" + result.Data.Token, true);

            }
            else
            {
                await NPCompletApp.ShellViewModel.GetUserStatus();
                NPCompletApp.ShellViewModel.isLoggedIn = true;
                NPCompletApp.ShellViewModel.isBusy = false;
                await eventCallback.InvokeAsync(result);
                NavigationManager.NavigateTo(V, true);
            }

            NPCompletApp.ShellViewModel.isBusy = false;
        }


        /// <summary>
        ///     The execute log out.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public async Task ExecuteLogOut()
        {
            this.ShowAuthError = false;

            await this.AuthenticationService.Logout();

            this.NavigationManager.NavigateTo(V, true);
        }

        /// <summary>
        /// The execute register.
        /// </summary>
        /// <param name="eventCallback">
        /// The event callback.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task ExecuteRegister(EventCallback<RegistrationResponseDto> eventCallback)
        {
            this.Loading = true;
            NPCompletApp.ShellViewModel.isBusy = true;
            this.ShowAuthError = false;
            try
            {
               
                string callbackUrl = this.NavigationManager.Uri;
                this.UserForRegistration.CallbackUrl = callbackUrl;
               var result = await this.AuthenticationService.Register(this.UserForRegistration);
                if (!result.IsSuccessfulRegistration)
                {
                    this.Loading = false;

                    await eventCallback.InvokeAsync(result);
                }
                else
                {
                    this.Loading = false;
                    await eventCallback.InvokeAsync(result);
                    if (result.userId != null) this.NavigationManager.NavigateTo("/confirmation?Id=" + result.userId, true); //this.NavigationManager.NavigateTo("/confirmation/" + result.userId, true);
                }

                this.Loading = false;
                NPCompletApp.ShellViewModel.isBusy = false;
            }
            catch (Exception ex)
            {

                throw;
            }
         
       
        }

        /// <summary>
        /// The execute resend verification.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task ExecuteResendVerification(string UserId)
        {
            NPCompletApp.ShellViewModel.isBusy = true;
            this.ShowAuthError = false;

            string callbackUrl = this.NavigationManager.Uri;
            await this.AuthenticationService.ResendVerification(UserId, callbackUrl);

            Success = true;

            NPCompletApp.ShellViewModel.isBusy = false;
        }
        public async Task<string> GetUserEmailById(string userId)
        {
            var bContent = JsonSerializer.Serialize(userId);
            var bodyContent = new StringContent(bContent, Encoding.UTF8, "application/json");

            var result = await _httpClient.PostAsync(Config.GetUserEmailById, bodyContent);
            var content = await result.Content.ReadAsStringAsync();
            var userEmailResult =  JsonSerializer.Deserialize<Result<string>>(content, _options);
            return  userEmailResult.Data;
        }
        public async Task FacebookJWT()
        {
            string AppId = await GetFacebookAppId();
            string redirectUrl = this.GetTheRedirectUrl();

            var accessTokenRequest = $"https://www.facebook.com/v11.0/dialog/oauth";
            accessTokenRequest += "?response_type=token";
            accessTokenRequest += $"&client_id={AppId}";
            accessTokenRequest += $"&redirect_uri={redirectUrl}login";

            NavigationManager.NavigateTo(accessTokenRequest, true);

        }

        public async Task GoogleJWT()
        {
            string AppId = await GetGoogleAppId();
            string redirectUrl = GetTheRedirectUrl();

            var accessTokenRequest = $"https://accounts.google.com/o/oauth2/v2/auth?";
            accessTokenRequest += $"scope=https://www.googleapis.com/auth/userinfo.email&";
            accessTokenRequest += "access_type=offline&prompt=consent&";
            accessTokenRequest += $"include_granted_scopes=true&";
            accessTokenRequest += $"response_type=code&";
            accessTokenRequest += $"state=state_parameter_passthrough_value&";
            accessTokenRequest += $"client_id={AppId}&";
            accessTokenRequest += $"redirect_uri={redirectUrl}login";

            NavigationManager.NavigateTo(accessTokenRequest, true);

        }
        public  override Task OnParametersSetAsync()
        {
            if (NavigationManager.Uri.Contains("facebook"))
            {
                var facebookUri = NavigationManager.Uri.Split('#');
                if (facebookUri.Length > 1 && QueryHelpers.ParseQuery(facebookUri[1]).TryGetValue("access_token", out var _accessToken))
                {
                    facebookAccessToken = _accessToken;

                    StateHasChanged();

                    ValidateFacebookAccessTokenDto.AuterizationCode = _accessToken;


                    ValidateLoginWithFacebook(OnLoginWithFacebook).Wait();


                }
            }
            if (NavigationManager.Uri.Contains("Google"))
            {
                var googleUri = NavigationManager.Uri.Split('?');
                if (googleUri.Length > 1 && QueryHelpers.ParseQuery(googleUri[1]).TryGetValue("code", out var _accessToken))
                {
                    googleAccessToken = _accessToken;


                    this.StateHasChanged();

                    ValidateGoogleAccessTokenDto.AuterizationCode = _accessToken;

                     ValidateLoginWithGoogle(OnLoginWithGoogle).Wait();


                }
            }
           

            return base.OnParametersSetAsync();

        }
        private string currentUrl;

        private string TokenValue = "";
        public string UserId = "";
        protected  async Task OnConfirmation()
        {
            currentUrl = NavigationManager.Uri;
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            if (currentUrl.Contains("Token"))
            {
                QueryHelpers.ParseQuery(uri.Query).TryGetValue("Token", out var Val);
                CopiedToken = Val;
            }
            QueryHelpers.ParseQuery(uri.Query).TryGetValue("Id", out var Id);
            UserId = Id;
            if (!string.IsNullOrEmpty(CopiedToken))
            {
                await VerifyToken();
            }
           
        }
        private async Task VerifyToken()
        {
            await ExecuteConfirm(OnConfirm, UserId);
        }
        public override async Task OnInitializedAsync()
        {
            if (AccessToken != null)
            {
                ValidateFacebookAccessTokenDto.AuterizationCode = HttpUtility.UrlDecode(AccessToken);
                ValidateFacebookAccessTokenDto.Email = Email;
                ValidateFacebookAccessTokenDto.FirstName = FirstName;
                ValidateFacebookAccessTokenDto.LastName = LastName;
            }
            if(RefreshToken!= null)
            {
                ValidateGoogleAccessTokenDto.AuterizationCode = HttpUtility.UrlDecode(RefreshToken);
                ValidateGoogleAccessTokenDto.Email = Email;
                ValidateGoogleAccessTokenDto.FirstName = FirstName;
                ValidateGoogleAccessTokenDto.LastName = LastName;
            }
           await OnConfirmation();
           
            
        }
    }
}