// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseViewModel.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Base
{
    #region

    using Microsoft.AspNetCore.Components.Authorization;

    using Prism.Mvvm;

    #endregion

    /// <summary>
    /// The base view model.
    /// </summary>
    public class BaseViewModel : BindableBase, IBaseViewModel
    {
        private bool _isBusy;

        private bool _isLoggedIn;

        private AuthenticationState _userState;

        /// <summary>
        /// Gets or sets a value indicating whether is busy.
        /// </summary>
        public bool isBusy
        {
            get => this._isBusy;
            set => this.SetProperty(ref this._isBusy, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether is logged in.
        /// </summary>
        public bool isLoggedIn
        {
            get => this._isLoggedIn;
            set => this.SetProperty(ref this._isLoggedIn, value);
        }

        /// <summary>
        /// Gets or sets the user state.
        /// </summary>
        public AuthenticationState UserState
        {
            get => this._userState;
            set => this.SetProperty(ref this._userState, value);
        }
    }
}