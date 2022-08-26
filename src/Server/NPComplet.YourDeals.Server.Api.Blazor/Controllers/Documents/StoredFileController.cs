// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StoredFileController.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Documents
{
    #region

    using System;
    using System.IO;
    using System.Net.Http.Headers;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using NPComplet.YourDeals.Domain.Shared.Document;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;

    #endregion

    /// <summary>
    ///     the store file controller
    /// </summary>
    [ApiVersion("1.0")]
    public class StoredFileController : TypeBaseController<StoredFile>
    {
        private readonly IWebHostEnvironment _environment;

        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredFileController"/> class.
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="environment">
        /// The web host environment.
        /// </param>
        public StoredFileController(
            UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            ILogger<StoredFileController> logger,
            IWebHostEnvironment environment)
            : base(userManager, unitOfWork, logger)
        {
            this._logger = logger;
            this._environment = environment;
        }

        /// <summary>
        ///     Upload Files.
        /// </summary>
        /// <returns>Upload result.</returns>
        [HttpPost]
        public IActionResult Upload()
        {

            var file = this.Request.Form.Files[0];
            var pathToSave = this._environment.ContentRootPath + @"\wwwroot\StaticFiles\Images";

            if (!Directory.Exists(pathToSave)) Directory.CreateDirectory(pathToSave);

            if (file.Length <= 0)
            {
                this._logger.LogError("BadRequest On Uploading Image");
                return this.BadRequest();
            }

            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
            if (fileName == null)
            {
                this._logger.LogError("BadRequest file does not have name.");
                return this.BadRequest();
            }

            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = @"StaticFiles\Images\" + fileName;
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            this._logger.LogInformation("file saved successfully");

            return this.Ok(dbPath);

        }
    }
}