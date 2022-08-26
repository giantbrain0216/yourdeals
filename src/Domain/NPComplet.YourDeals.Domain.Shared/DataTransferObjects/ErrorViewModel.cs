// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorViewModel.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects
{
    /// <summary>
    ///     ErrorViewModel
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        ///     RequestId
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        ///     ShowRequestId
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
    }
}