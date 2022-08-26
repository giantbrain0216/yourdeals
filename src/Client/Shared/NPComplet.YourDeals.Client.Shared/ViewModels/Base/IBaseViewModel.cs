// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBaseViewModel.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Base
{
    /// <summary>
    /// The BaseViewModel interface.
    /// </summary>
    public interface IBaseViewModel
    {
        /// <summary>
        /// Gets a value indicating whether is busy.
        /// </summary>
        bool isBusy { get; set; }

        /// <summary>
        /// Gets a value indicating whether is logged in.
        /// </summary>
        bool isLoggedIn { get; set; }
    }
}