// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModalService.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.PopupWindow.Services
{
    #region

    using System;

    using Microsoft.AspNetCore.Components;

    #endregion

    /// <summary>
    /// The modal service.
    /// </summary>
    public class ModalService
    {
        /// <summary>
        /// The on close.
        /// </summary>
        public event Action OnClose;

        /// <summary>
        /// The on show.
        /// </summary>
        public event Action<string, RenderFragment> OnShow;

        /// <summary>
        /// The close.
        /// </summary>
        public void Close()
        {
            this.OnClose?.Invoke();
        }

        /// <summary>
        /// The show.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="contentType">
        /// The content type.
        /// </param>
        /// <exception cref="ArgumentException">
        /// </exception>
        public void Show(string title, Type contentType)
        {
            if (contentType.BaseType != typeof(ComponentBase))
                throw new ArgumentException($"{contentType.FullName} must be a Blazor Component");

            var content = new RenderFragment(
                x =>
                    {
                        x.OpenComponent(1, contentType);
                        x.CloseComponent();
                    });

            this.OnShow?.Invoke(title, content);
        }
    }
}