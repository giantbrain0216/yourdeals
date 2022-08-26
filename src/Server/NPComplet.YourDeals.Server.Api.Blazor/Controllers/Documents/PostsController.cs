// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostsController.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Documents
{
    #region

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using NPComplet.YourDeals.Domain.Shared.Shared;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;

    #endregion

    /// <summary>
    ///     the posts controller
    /// </summary>
    [ApiVersion("1.0")]
    public class PostsController : TypeBaseController<Post>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostsController"/> class.
        ///     the post controller
        /// </summary>
        /// <param name="userManager">
        /// </param>
        /// <param name="unitOfWork">
        /// </param>
        /// <param name="logger">
        /// </param>
        public PostsController(UserManager<User> userManager, IUnitOfWork unitOfWork, ILogger<PostsController> logger)
            : base(userManager, unitOfWork, logger)
        {
        }
    }
}