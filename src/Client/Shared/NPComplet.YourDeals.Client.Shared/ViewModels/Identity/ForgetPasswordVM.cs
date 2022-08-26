// --------------------------------------------------------------------------------------------------------------------
// <copyright file="cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Primitives;
using MvvmBlazor.ViewModel;
using NPComplet.YourDeals.Client.Shared.Logic.Interfaces;
using NPComplet.YourDeals.Client.Shared.ViewModels.Base;
using NPComplet.YourDeals.Client.Shared.ViewModels.Constant;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using NPComplet.YourDeals.Translations.Resources;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Identity
{
    #region

    #endregion
    public enum ResetPasswordSteps { startPasswordChangeProcess, hasSentToken, newPassword,submitNewPassword }
    
    /// <summary>
    ///     The login vm.
    /// </summary>
    public class ForgetPasswordVM : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForgetPasswordVM"/> class.
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
        IStringLocalizer<Translation> _localize;
        /// <summary>
        /// 
        /// </summary>
      public  bool isCallBack = false;
        public ForgetPasswordVM(
            IStringLocalizer<Translation> localize,
            IAuthenticationService authService,
            NavigationManager navMan,
            AppShellViewModel appShellViewModel)
        {
            this.AuthenticationService = authService;
            this.NavigationManager = navMan;
            NPCompletApp.ShellViewModel = appShellViewModel;
            _localize = localize;
        }
        #region Parameters
        /// <summary>
        /// Gets or sets the on login.
        /// </summary>
        [Parameter]
        public EventCallback<Result<AuthResponseDto>> OnForgotPassword { get; set; }

        /// <summary>
        /// Gets or sets the go to BackToLogin.
        /// </summary>
        [Parameter]
        public EventCallback BackToLogin { get; set; }

        [Parameter]
        public bool IsMobile { get; set; } = false;

        /// <summary>
        /// Gets or sets the on login.
        /// </summary>
        [Parameter]
        public EventCallback<Result<AuthResponseDto>> OnChangedPassword { get; set; }



       
        #endregion
        /// <summary>
        ///     Gets or sets the authentication service.
        /// </summary>
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

         

        /// <summary>
        ///     Gets or sets the error.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        ///     Gets or sets the success message.
        /// </summary>
        public string SuccessMessage { get; set; }
        public string UserId { get; set; }
        public Uri CallBackUri { get; set; }

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
        ///     Gets or sets a value indicating whether to show success confirmation.
        /// </summary>
        public bool EmailIsConfirmedSuccessfully { get; set; }


        /// <summary>
        ///     Gets or sets a value ResetPasswordSteps.
        /// </summary>
        public ResetPasswordSteps ChangePasswordSteps { get; set; } = ResetPasswordSteps.startPasswordChangeProcess;

        /// <summary>
        ///     Gets or sets the user for authentication.
        /// </summary>
        public ForgotPasswordViewModel UserForForgotPassword { get; set; } = new ForgotPasswordViewModel();


        /// <summary>
        /// The execute ForgotPassword.
        /// </summary>
        /// <param name="eventCallback">
        /// The event Callback.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>

        private string ReplaceFirstOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.IndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }

        public async Task ExecuteSendPasswordResetTokenAsync(EventCallback<Result<AuthResponseDto>> eventCallback, bool IsMobile)
        {
            string callbackUrl = "";
            if (!IsMobile)
                callbackUrl = ReplaceFirstOccurrence(this.NavigationManager.Uri, "login", "changeyourpassword");
            else
                callbackUrl = "/MobileChangeYourPassword";

            this.Loading = true;
            NPCompletApp.ShellViewModel.isBusy = true;
            this.ShowAuthError = false;
            Result<AuthResponseDto> result = new Result<AuthResponseDto>();

            result = await this.AuthenticationService.ForgotPassword(new ForgotPasswordViewModel
            {
                Email = this.UserForForgotPassword.Email,
                NewPassword = string.Empty,
                ConfirmPassword = string.Empty,
                RedirectUrl = callbackUrl,
                CopiedToken = string.Empty
            });

            if (!result.Succeeded)
            {
                foreach (var msg in result.Messages)
                {
                    this.Error += msg + Environment.NewLine;
                }
                this.Loading = false;
                this.ShowAuthError = true;
                SuccessMessage = null;
            }
            else
            {
                SuccessMessage = _localize["ChangePasswordLink"]; 
            }
            await eventCallback.InvokeAsync(result);
             NPCompletApp.ShellViewModel.isBusy = false;
            this.Loading = false;
        }
        private string GetParam(string key)
        {
            StringValues value;
            if (QueryHelpers.ParseQuery(CallBackUri.Query).TryGetValue(key, out value))
                return value;
            else
                return null;
        }
        public async Task ExecuteChangePassword(EventCallback<Result<AuthResponseDto>> eventCallback)
        {
            NPCompletApp.ShellViewModel.isBusy = true;
            this.ShowAuthError = false;
            Result<AuthResponseDto> result = new Result<AuthResponseDto>();
            string userId = GetParam("userId");
          //  string email = GetParam("email");
            string token = GetParam("token");
            if (!string.IsNullOrEmpty(UserForForgotPassword.NewPassword) && !string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(userId))
            {
                result = await this.AuthenticationService.ResetPassword(new ForgotPasswordViewModel
                {
                    CopiedToken = token,
                    Email = UserForForgotPassword.Email,
                    NewPassword = UserForForgotPassword.NewPassword
                }, userId);

                if (!result.Succeeded)
                {
                    foreach (var msg in result.Messages)
                    {
                        this.Error += msg + "<br/>";
                    }
                    this.Loading = false;
                    this.ShowAuthError = true;
                    SuccessMessage = null;
                    await eventCallback.InvokeAsync(result);
                }
                else
                {
                    NavigationManager.NavigateTo("/login");
                }
            }
            else
            {
                this.Error = _localize["ChangePasswordError"];   
            }
            await eventCallback.InvokeAsync(result);
            NPCompletApp.ShellViewModel.isBusy = false;
            this.Loading = false;
        }
        public override async Task OnParametersSetAsync()
        {
            StringValues token;
            StringValues email;

            CallBackUri = NavigationManager.ToAbsoluteUri(this.NavigationManager.Uri);
            if (QueryHelpers.ParseQuery(CallBackUri.Query).TryGetValue("token", out token) &&
                QueryHelpers.ParseQuery(CallBackUri.Query).TryGetValue("email", out email))
            {

                UserForForgotPassword.CopiedToken = token;
                UserForForgotPassword.RedirectUrl = CallBackUri.ToString();
                UserForForgotPassword.Email = email;
                if (string.IsNullOrEmpty(token))
                    ChangePasswordSteps = ResetPasswordSteps.hasSentToken;
                isCallBack = true;

            }
            UserForForgotPassword.ConfirmPassword = "null";
            UserForForgotPassword.CopiedToken = "null";
            UserForForgotPassword.NewPassword = "null";
            UserForForgotPassword.RedirectUrl = "null";
        }
       
        public override void OnParametersSet()
        {
            base.OnParametersSet();
        }
    }
  
}