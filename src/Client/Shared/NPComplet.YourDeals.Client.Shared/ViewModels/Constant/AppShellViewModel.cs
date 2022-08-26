// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppShellViewModel.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using NPComplet.YourDeals.Client.Shared.Services;
using NPComplet.YourDeals.Client.Shared.ViewModels.Base;
using Radzen;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Constant
{
    #region

    #endregion

    /// <summary>
    ///     The app shell view model.
    /// </summary>
    public class AppShellViewModel : BaseViewModel
    {
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly NotificationService _notificationService;
        private readonly IStorageService _storageservice;
        /// <summary>
        /// Initializes a new instance of the <see cref="AppShellViewModel"/> class.
        /// </summary>
        /// <param name="_authenticationStateProvider">
        /// The _authentication state provider.
        /// </param>
        public AppShellViewModel(IStorageService storageservice,AuthenticationStateProvider _authenticationStateProvider, NotificationService notificationService)
        {
            this.authenticationStateProvider = _authenticationStateProvider;
            this._notificationService = notificationService;
            _storageservice = storageservice;
        }

        /// <summary>
        ///     The get user status.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public async Task GetUserStatus()
        {
            this.UserState = await this.authenticationStateProvider.GetAuthenticationStateAsync();

            if (this.UserState.User.Identity.IsAuthenticated) this.isLoggedIn = true;
            else this.isLoggedIn = false;
        }

        public async Task SetStorage(string key, object value)
        {
           await  _storageservice.SetAsync(key,value);
        }
        public async Task<TValue> GetStorage<TValue>(string key)
        {
            return await _storageservice.GetAsync<TValue>(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Severity"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task ShowNotification(NotificationSeverity Severity,string title,string message)
        {
            _notificationService.Notify(new NotificationMessage { Severity = Severity, Summary = title, Detail = message, Duration = 4000 });

        }
    }
}