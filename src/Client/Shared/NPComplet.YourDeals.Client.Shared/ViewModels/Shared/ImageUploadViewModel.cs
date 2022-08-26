// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageUploadViewModel.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using NPComplet.YourDeals.Client.Shared.Helpers;
using Prism.Mvvm;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Shared
{
    #region

    #endregion

    /// <summary>
    ///     The image upload view model.
    /// </summary>
    public class ImageUploadViewModel : BindableBase
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageUploadViewModel"/> class.
        /// </summary>
        /// <param name="httpClient">
        /// The http client.
        /// </param>
        public ImageUploadViewModel(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        /// <summary>
        ///     Gets or sets the img url.
        /// </summary>
        public List<string> ImgUrl { get; set; } = new List<string>();

        /// <summary>
        ///     Gets or sets the input.
        /// </summary>
        public ElementReference Input { get; set; }

        /// <summary>
        ///     Gets or sets the on change.
        /// </summary>
        public EventCallback<List<string>> OnChange { get; set; }

        /// <summary>
        ///     Gets or sets the token.
        /// </summary>
        [Parameter]
        public string Token { get; set; }

        /// <summary>
        /// The handle selected.
        /// </summary>
        /// <param name="files">
        /// The files.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task HandleSelected(IFileListEntry[] files)
        {
            foreach (var file in files)
            {
                if (file == null) continue;

                var resizedFile = await file.ToImageFileAsync("image/png", 800, 600);
                await using (var ms = await resizedFile.ReadAllAsync(Convert.ToInt32(resizedFile.Size)))
                {
                    var content = new MultipartFormDataContent();
                    content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                    content.Add(new StreamContent(ms, Convert.ToInt32(resizedFile.Size)), "image", file.Name);

                    this.ImgUrl.Add(await this.UploadProductImage(content));
                }

                await this.OnChange.InvokeAsync(this.ImgUrl);
            }
        }

        /// <summary>
        /// The upload product image.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="HttpRequestException">
        /// </exception>
        public async Task<string> UploadProductImage(MultipartFormDataContent content)
        {
            this._httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", NPCompletApp.Token);

            var postResult = await this._httpClient.PostAsync(Config.UploadFile, content);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode) throw new HttpRequestException(postContent);

            return postContent;
        }
    }
}