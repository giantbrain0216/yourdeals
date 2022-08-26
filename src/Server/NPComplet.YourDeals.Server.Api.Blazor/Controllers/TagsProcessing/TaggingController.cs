// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaggingController.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.TagsProcessing
{
    #region

    using System.IO;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    using OpenNLP.Tools.Chunker;
    using OpenNLP.Tools.PosTagger;
    using OpenNLP.Tools.Tokenize;

    #endregion

    /// <summary>
    ///     Process Tags
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [AllowAnonymous]
    public class TaggingController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaggingController"/> class. 
        /// The tagging controller.
        /// </summary>
        /// <param name="environment">
        /// The environment.
        /// </param>
        public TaggingController(IWebHostEnvironment environment)
        {
            this._environment = environment;
            this.ModelPath = this._environment.ContentRootPath + @"\wwwroot\TagRes\Models";
        }

        /// <summary>
        ///     The model path.
        /// </summary>
        public string ModelPath { get; set; }

        /// <summary>
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult<string> GetPath()
        {
            return this._environment.ContentRootPath + @"\wwwroot\TagRes\Models";
        }

        /// <summary>
        /// Get tags by description.
        /// </summary>
        /// <param name="description">
        /// The description
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpGet("{description}")]
        public IActionResult GetTags(string description)
        {
            if (string.IsNullOrEmpty(description)) return this.Ok(string.Empty);

            var modelPath = Path.Combine(this.ModelPath, "EnglishTok.nbin");
            var tokenizer = new EnglishMaximumEntropyTokenizer(modelPath);
            var tokens = tokenizer.Tokenize(description);

            var chunkModelPath = Path.Combine(this.ModelPath, "EnglishChunk.nbin");
            var chunker = new EnglishTreebankChunker(chunkModelPath);

            var posTagModelPath = Path.Combine(this.ModelPath, "EnglishPOS.nbin");
            var tagDictDir = Path.Combine(this.ModelPath, "Parser", "tagdict");
            var posTagger = new EnglishMaximumEntropyPosTagger(posTagModelPath, tagDictDir);

            return this.Ok(chunker.GetChunks(tokens, posTagger.Tag(tokens)));
        }
    }
}