// --------------------------------------------------------------------------------------------------------------------
// <copyright company="NPComplet" file="Modal.razor.cs">
//   Org
// </copyright>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.PopupWindow
{
    #region

    using System;

    using Microsoft.AspNetCore.Components;

    using NPComplet.YourDeals.Controls.PopupWindow.Services;

    #endregion

    /// <summary>
    ///     https://www.telerik.com/blogs/creating-a-reusable-javascript-free-blazor-modal
    /// </summary>
    public class ModalBase : ComponentBase, IDisposable
    {
        /// <summary>
        /// Finalizes an instance of the <see cref="ModalBase"/> class. 
        /// </summary>
        ~ModalBase()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public RenderFragment Content { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is visible.
        /// </summary>
        protected bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        protected string Title { get; set; }

        [Inject]
        private ModalService ModalService { get; set; }

        /// <summary>
        /// The close modal.
        /// </summary>
        public void CloseModal()
        {
            this.IsVisible = false;
            this.Title = string.Empty;
            this.Content = null;

            this.StateHasChanged();
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The show modal.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        public void ShowModal(string title, RenderFragment content)
        {
            this.Title = title;
            this.Content = content;
            this.IsVisible = true;

            this.StateHasChanged();
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            this.ModalService.OnShow -= this.ShowModal;
            this.ModalService.OnClose -= this.CloseModal;
        }

        /// <summary>
        /// The on initialized.
        /// </summary>
        protected override void OnInitialized()
        {
            this.ModalService.OnShow += this.ShowModal;
            this.ModalService.OnClose += this.CloseModal;
        }
    }
}