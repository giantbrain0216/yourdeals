// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestsController.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Deals.Requests
{
    #region

    #region

    using BaseController;
    using Infrastructure.Repositories.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using NPComplet.YourDeals.Domain.Shared.Deal.Requests;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using System.IO;
    using NPComplet.YourDeals.Domain.Shared.Shared;

    #endregion

    #endregion

    /// <summary>
    ///     the Deals Manager
    /// </summary>
    [ApiVersion("1.0")]
    public class RequestsController : DealsController<Request>
    {

        protected readonly IUnitOfWork _unitOfWork;

        protected readonly UserManager<User> _userManager;

        private IHostingEnvironment _environment;

        private readonly SignInManager<User> _signInManager;


        /// <inheritdoc />
        public RequestsController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IUnitOfWork unitOfWork,
             IHostingEnvironment environment,
            ILogger<RequestsController> logger)
            : base(userManager, unitOfWork, logger)
        {
            this._environment = environment;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpGet("{Id}")]
        [AllowAnonymous]
        public virtual async Task<ActionResult<Request>> GetDealById(Guid id)
        {
            Request request = await this.UnitOfWork.GetRepository<Request>().SingleOrDefaultAsync(
                    p => p.Id == id,
                    "Propositions",
                    "DealDocument",
                    "DealDocument.DealFiles",
                    "DealPriceReference",
                    "DealPriceReference.PaymentManors");

            var owner = await this._userManager.FindByIdAsync(request.OwnerId.ToString());

            if (owner == null)
                return this.BadRequest();

            await setTheProfileForTheUser(owner);

            request.Owner = owner;

            return this.Ok(request);
        }

        /// <summary>
        /// Get requests for authenticated user with pagination.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="size">The size to get.</param>
        /// <returns>Request list.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests(int offset, int size)
        {


            /* - setting the user foreach request

            var userId = UserManager.GetUserId(HttpContext.User);


            var user = await this._userManager.FindByIdAsync(userId);

            if (user == null)
                return this.BadRequest();

            await setTheProfileForTheUser(user);

            var result = await GetDealsWithPaginationAsync(offset,
                size,
                request => request.OwnerId.ToString() == userId
                         && request.InternalValidation
                         && (request.SelectedPropositions == null || !request.SelectedPropositions.Any() || request.SelectedPropositions.Any(sp => sp.IsSelected == true)));

            if(result.Value != null)
            {
                foreach (Request requst in result.Value.ToList())
                {
                    requst.Owner = user;
                }
            }
            return result;

            */

            var userId = UserManager.GetUserId(HttpContext.User);

            return await GetDealsWithPaginationAsync(offset,
                size,
                request => request.OwnerId.ToString() == userId
                         && request.InternalValidation
                         && (request.SelectedPropositions == null || !request.SelectedPropositions.Any() || request.SelectedPropositions.Any(sp => sp.IsSelected == true))); ;



        }


        private async Task setTheProfileForTheUser(User user)
        {
            

            user.Profile = await this._unitOfWork.GetRepository<Profile>().GetByIdAsync(user.ProfileId);
            user.Profile.ProfileImage = await this._unitOfWork.GetRepository<ProfileImage>().GetByIdAsync(user.Profile.ProfileImageId);
            try
            {
                string path = System.IO.Path.Combine(_environment.WebRootPath, user.Profile.ProfileImage.LocalPath, user.Profile.ProfileImage.FileName +"." + user.Profile.ProfileImage.FileExtension);
                var result= await convertLocalFileToArrOfByte(path);
                user.Profile.ProfileImage.Data = result;
            }
            catch
            {
                string path = System.IO.Path.Combine(_environment.WebRootPath, "AppData", "Users", "ProfilePictures", "DefaultProfileImg.jpg");
                user.Profile.ProfileImage.Data = await convertLocalFileToArrOfByte(path);
            }


            if (user.Profile.AddressId != null)
            {
                user.Profile.Address = await this._unitOfWork.GetRepository<Address>().GetByIdAsync(user.Profile.AddressId);
                user.Profile.Address.Location = await this._unitOfWork.GetRepository<Location>().GetByIdAsync(user.Profile.Address.LocationId);
            }
        }

        private async Task<byte[]> convertLocalFileToArrOfByte(string localPath)
        {
            using (FileStream fs = new FileStream(localPath, FileMode.Open, FileAccess.Read))
            {
                // Create a byte array of file stream length
                byte[] bytes = System.IO.File.ReadAllBytes(localPath);
                //Read block of bytes from stream into the byte array
                fs.Read(bytes, 0, System.Convert.ToInt32(fs.Length));
                //Close the File Stream
                fs.Close();
                return bytes; //return the byte data

            }
        }
    }
}