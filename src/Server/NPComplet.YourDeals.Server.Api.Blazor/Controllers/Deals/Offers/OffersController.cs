// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OffersController.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Deals.Offers
{
    #region
    using BaseController;
    using Infrastructure.Repositories.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using NPComplet.YourDeals.Domain.Shared.Deal.Offers;
    using NPComplet.YourDeals.Domain.Shared.Shared;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    ///     the Offer controller
    /// </summary>
    [ApiVersion("1.0")]
    public class OffersController : DealsController<Offer>
    {

        protected readonly IUnitOfWork _unitOfWork;

        protected readonly UserManager<User> _userManager;

        private IHostingEnvironment _environment;

        private readonly SignInManager<User> _signInManager;

        /// <inheritdoc />
        public OffersController(UserManager<User> userManager,
            IUnitOfWork unitOfWork,
                        SignInManager<User> signInManager,
                                     IHostingEnvironment environment,

            ILogger<OffersController> logger)
            : base(userManager, unitOfWork, logger)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._environment = environment;
            this._unitOfWork = unitOfWork;

        }

        /// <summary>
        /// Get deal by identifier.
        /// </summary>
        /// <param name="id">
        /// The identifier.
        /// </param>
        /// <returns>
        /// The deal.
        /// </returns>
        [HttpGet("{Id}")]
        [AllowAnonymous]
        public virtual async Task<ActionResult<Offer>> GetDealById(Guid id)
        {
            Offer offer = await UnitOfWork.GetRepository<Offer>().SingleOrDefaultAsync(
                 p => p.Id == id,
                 "Quotation",
                 "Quotation.TermConditionPost",
                 "Quotation.WarrantyPost",
                 "Propositions",
                 "DealDocument",
                 "DealDocument.DealFiles",
                 "DealPriceReference",
                 "DealPriceReference.PaymentManors");

            var owner = await this._userManager.FindByIdAsync(offer.OwnerId.ToString());

            if (owner == null)
                return this.BadRequest();

            await setTheProfileForTheUser(owner);

            offer.Owner = owner;

            return this.Ok(offer);


        }

        /// <summary>
        /// Get offers for authenticated user with pagination.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="size">The size to get.</param>
        /// <returns>Offer list.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Offer>>> GetOffers([FromQuery]int offset, [FromQuery] int size)
        {
            var userId = UserManager.GetUserId(HttpContext.User);
            return await GetDealsWithPaginationAsync(offset,
                size,
                offer => offer.OwnerId.ToString() == userId
                           && offer.InternalValidation
                           && (offer.SelectedPropositions == null || !offer.SelectedPropositions.Any() || offer.SelectedPropositions.Any(sp => sp.IsSelected == true)));
        }


        private async Task setTheProfileForTheUser(User user)
        {


            user.Profile = await this._unitOfWork.GetRepository<Profile>().GetByIdAsync(user.ProfileId);
            user.Profile.ProfileImage = await this._unitOfWork.GetRepository<ProfileImage>().GetByIdAsync(user.Profile.ProfileImageId);
            try
            {
                string path = System.IO.Path.Combine(_environment.WebRootPath, user.Profile.ProfileImage.LocalPath, user.Profile.ProfileImage.FileName + "." + user.Profile.ProfileImage.FileExtension);
                user.Profile.ProfileImage.Data = await convertLocalFileToArrOfByte(path);
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