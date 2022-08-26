// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProfilesController.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Users
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    #region

    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.ManageViewModels;
    using NPComplet.YourDeals.Domain.Shared.Shared;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using NPComplet.YourDeals.Domain.Shared.Wrapper;
    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Data;
    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;

    #endregion

    /// <summary>
    ///     the Users controller
    /// </summary>

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]

    public class UsersController : ControllerBase
    {

        /// <summary>
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// </summary>
        protected readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// </summary>
        protected readonly UserManager<User> _userManager;

        private IHostingEnvironment _environment;



        private readonly SignInManager<User> _signInManager;

        private readonly ApplicationDbContext _context;



        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        ///     Profiles Controllers
        /// </summary>
        /// <param name="userManager">
        /// <param name="unitOfWork">
        /// </param>
        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager,
            IUnitOfWork unitOfWork,
            ILogger<UsersController> logger,
                                    IHostingEnvironment environment,
                                    ApplicationDbContext context
) 
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            Logger = logger;
            _signInManager = signInManager;
            _environment = environment;
            _context = context;


        }

        /// <summary>
        /// Add entity  this will override the OwnerId
        ///     Authenticated user
        /// </summary>
        /// <param name="value">
        /// value 
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        protected virtual async Task<IActionResult> PostProfile([FromBody] Profile value)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null && user.Profile == null)
            {
                value.Address.OwnerId = user.Id;
                value.Address.Owner = user;

                await this._unitOfWork.GetRepository<Profile>().AddAsync(value);
                await this._unitOfWork.GetRepository<Address>().AddAsync(value.Address);

                user.ProfileId = value.Id;
                await this._userManager.UpdateAsync(user);
                await this._unitOfWork.CommitAsync(user?.Id.ToString());

                return this.Ok();
            }

            return this.BadRequest();
        }

        /// <summary>
        /// Add entity  this will override the OwnerId
        ///     Authenticated user
        /// </summary>
        /// <param name="value">
        /// value 
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody] User updatedUser)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await this._userManager.FindByIdAsync(userId);

            if (user == null)
                return this.BadRequest();

            if (updatedUser.Email != user.Email)
                return this.BadRequest();

            user.FirstName= updatedUser.FirstName;
            user.LastName= updatedUser.LastName;
            user.PhoneNumber= updatedUser.PhoneNumber;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return this.Ok(await Result<User>.SuccessAsync(user));
        }


        [HttpPost]
        public async Task<IActionResult> UploadUserProfileImg()
        {
            var file = this.Request.Form.Files[0];
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await this._userManager.FindByIdAsync(userId);

            if (user == null)
                return this.BadRequest();

            await setTheProfileForTheUser(user);

            user.Profile.ProfileImage.FileExtension = "png";
            user.Profile.ProfileImage.FileName = "profilePicture" + user.Id.ToString();
            user.Profile.ProfileImage.LocalPath = "/AppData/Users/ProfilePictures/";


             using (Stream fs = new FileStream(_environment.WebRootPath + user.Profile.ProfileImage.LocalPath + user.Profile.ProfileImage.FileName + "." + user.Profile.ProfileImage.FileExtension
                , FileMode.OpenOrCreate, FileAccess.ReadWrite))
             {
                file.CopyTo(fs);
                 fs.Close();

             }

            await _unitOfWork.GetRepository<ProfileImage>().UpdateAsync(user.Profile.ProfileImage);
            await _unitOfWork.CommitAsync(userId);

            return this.Ok(await Result.SuccessAsync());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserEmailById([FromBody]string userId)
        {
            var user = await this._userManager.FindByIdAsync(userId);
            return this.Ok(await Result<string>.SuccessAsync(data:user.Email));

        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetContactWithUserId([FromBody] string userId)
        {
            var user = await this._userManager.FindByIdAsync(userId);


            if (user == null)
                return this.BadRequest();

            await setTheProfileForTheUser(user);

            ContactDto contactDto = new ContactDto();
            contactDto.UserId = userId;
            contactDto.UserEmail = user.Email;
            contactDto.Base64 = "data:image/"
                    + $"{user.Profile.ProfileImage.FileExtension};"
                    + "base64, "
                    + Convert.ToBase64String(user.Profile.ProfileImage.Data);




            return this.Ok(await Result<ContactDto>.SuccessAsync(data: contactDto));

        }

        /// <summary>
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await this._userManager.FindByIdAsync(userId);

            if (user == null)
                return this.BadRequest();

            await setTheProfileForTheUser(user);



           

            return this.Ok(await Result<User>.SuccessAsync(user));
        }

        private async Task setTheProfileForTheUser(User user)
        {
            if (user.ProfileId == Guid.Empty || user.ProfileId == null)
                await createNewProfileForNewUsers(user.Email);

            user.Profile = await this._unitOfWork.GetRepository<Profile>().GetByIdAsync(user.ProfileId);
            user.Profile.ProfileImage = await this._unitOfWork.GetRepository<ProfileImage>().GetByIdAsync(user.Profile.ProfileImageId);
            try
            {
                user.Profile.ProfileImage.Data = await convertLocalFileToArrOfByte(
                _environment.WebRootPath +
                user.Profile.ProfileImage.LocalPath +
                user.Profile.ProfileImage.FileName +
                "." + user.Profile.ProfileImage.FileExtension);
            }
            catch
            {
             
                user.Profile.ProfileImage.Data = await convertLocalFileToArrOfByte(
                _environment.WebRootPath +
                "/StaticFiles/ProfilePictures/" +
                "DefaultProfileImg" +
                "." + "jpg");
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

        private async Task<Profile> createNewProfileForNewUsers(string email)
        {
            var user = await this._userManager.FindByEmailAsync(email);


            var location = new Location()
            {
                Owner = user,
                OwnerId = user.Id,
            };

            await this._unitOfWork.GetRepository<Location>().AddAsync(location);


            var address = new Address();
            address.Owner = user;
            address.OwnerId = user.Id;
            address.LocationId = location.Id;
            address.Location = location;
            await this._unitOfWork.GetRepository<Address>().AddAsync(address);

            var profileImg = new ProfileImage()
            {
                OwnerId = user.Id,
                Owner = user,
                LocalPath = "/StaticFiles/ProfilePictures/",
                FileName = "DefaultProfileImg",
                FileExtension = "jpg"
            };

            await this._unitOfWork.GetRepository<ProfileImage>().AddAsync(profileImg);


            var profile = new Profile()
            {
                AddressId = address.Id,
                Address = address,
                ProfileImageId = profileImg.Id,
                ProfileImage = profileImg
                
                

            };

            await this._unitOfWork.GetRepository<Profile>().AddAsync(profile);

            user.Profile = profile;
            user.ProfileId = profile.Id;

            await this._userManager.UpdateAsync(user);

            return profile;

        }

        
        [HttpGet]
        public async Task<IActionResult> GetTheUserAddresses()
        {

            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await this._userManager.FindByIdAsync(userId);
            if (user == null)
                return this.BadRequest();
            await setTheProfileForTheUser(user);

            List<Address> allAddresses = (List<Address>)await this._unitOfWork.GetRepository<Address>().GetAllAsync();

            List<Address> addresses = allAddresses
                .Where(a => a.OwnerId == user.Id)
                .OrderByDescending(a => a.IsDefault)
                .ThenBy(a => a.InternalCreationDateTimeUtc)
                .ThenBy(a => a.InternalModificationDateTimeUtc).ToList();
            foreach(Address address in addresses)
            {
                address.Owner = user;
                address.Location = await _unitOfWork.GetRepository<Location>().GetByIdAsync(address.LocationId);
            }    


            return this.Ok(await Result<List<Address>>.SuccessAsync(addresses));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAddress([FromBody] Address  address)
        {

            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await this._userManager.FindByIdAsync(userId);

            if (user == null)
                return this.BadRequest();


            await setTheProfileForTheUser(user);



            if(address.OwnerId == user.Id)
            {
                detachLocalAddress(address);
                detachLocalLocation(address.Location);
                detachLocalUser(user);

                if (user.Profile.AddressId == address.Id)
                {


                    user.Profile.Address = null;
                    user.Profile.AddressId = null;


                    await this._userManager.UpdateAsync(user);
                    await this._unitOfWork.CommitAsync(user?.Id.ToString());

                    _context.Profiles.Update(user.Profile);

                    if (address.Location != null)
                    {
                        address.Location.Owner = null;
                        address.Location.OwnerId = null;
                        address.LocationId = null;
                        _context.Addresses.Update(address);
                        await _context.SaveChangesAsync();
                        _context.Locations.Remove(address.Location);
                        await _context.SaveChangesAsync();

                    }

                    address.Location = null;

                    _context.Addresses.Remove(address);
                    await _context.SaveChangesAsync();

                    return this.Ok(await Result.SuccessAsync());
                }
                else
                {
                    List<Profile> profiles = (List<Profile>)await _unitOfWork.GetRepository<Profile>().GetAllAsync();
                    List<Profile> profilesHoldThisAddress = profiles.Where(p => p.AddressId == address.Id).ToList();
                    
                    foreach(Profile profile in profilesHoldThisAddress)
                    {
                        profile.Address = null;
                        profile.AddressId = null;

                        _context.Profiles.Update(profile);
                        await _context.SaveChangesAsync();

                    }



                    if (address.Location != null && address.LocationId != null)
                    {
                        var locationId = address.LocationId;
                        address.Location.Owner = null;
                        address.Location.OwnerId = null;
                        address.LocationId = null;
                        _context.Addresses.Update(address);
                        _context.Locations.Remove(_context.Locations.Find(locationId));
                        await _context.SaveChangesAsync();

                    }

                    address.Location = null;
                    address.LocationId = null;
                    address.OwnerId = null;
                    address.Owner = null;


                    _context.Addresses.Update(address);
                    await _context.SaveChangesAsync();

                    _context.Addresses.Remove(address);
                    await _context.SaveChangesAsync();
                    return this.Ok(await Result.SuccessAsync());
                }
               
            }
            else
            {
                return this.BadRequest();

            }


        }

       

        [HttpPost]
        public async Task<IActionResult> UpdateAddress([FromBody] Address address)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await this._userManager.FindByIdAsync(userId);
            if (user == null)
                return this.BadRequest();
            await setTheProfileForTheUser(user);
            detachLocalAddress(address);
            detachLocalUser(user);
            detachLocalLocation(address.Location);

            if (address.IsDefault == true)
            {
               
                user.Profile.Address = address;
                user.Profile.AddressId = address.Id;
                _context.Profiles.Update(user.Profile);
                await _context.SaveChangesAsync();

                List<Address> allAddresses = (List<Address>)await this._unitOfWork.GetRepository<Address>().GetAllAsync();

                List<Address> addresses = allAddresses
                    .Where(a => a.OwnerId == user.Id)
                    .OrderByDescending(a => a.IsDefault)
                    .ThenBy(a => a.InternalCreationDateTimeUtc)
                    .ThenBy(a => a.InternalModificationDateTimeUtc).ToList();

                foreach (Address addressModel in addresses)
                {
                    addressModel.IsDefault = false;
                }

                address.IsDefault = true;

            }


            address.Location.InternalModificationDateTimeUtc = DateTime.UtcNow;
            _context.Locations.Update(address.Location);
            await _context.SaveChangesAsync();

            address.InternalModificationDateTimeUtc= DateTime.UtcNow;
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();


            return this.Ok(await Result<Address>.SuccessAsync(address));

        }

        [HttpPost]
        public async Task<IActionResult> AddNewAddress([FromBody] Address address)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await this._userManager.FindByIdAsync(userId);
            if (user == null)
                return this.BadRequest();
            await setTheProfileForTheUser(user);

            
            detachLocalAddress(address);


            address.OwnerId = user.Id;
            address.Owner = user;

            address.Location.OwnerId = user.Id;
            address.Location.Owner = user;

            await _unitOfWork.GetRepository<Location>().AddAsync(address.Location);

            address.LocationId = address.Location.Id;

            await _unitOfWork.GetRepository<Address>().AddAsync(address);

            await _unitOfWork.CommitAsync(Userid: userId);

            return this.Ok(await Result<Address>.SuccessAsync(address));

        }

        /// <summary>
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await this._userManager.FindByIdAsync(userId);
            user.OldEmail = user.Email;
            user.Email = "deleted_user@rightndeals.com";
            user.UserName = "deleted_user@rightndeals.com";
            user.NormalizedEmail =  "deleted_user@rightndeals.com";
            user.NormalizedUserName  = "deleted_user@rightndeals.com";
            user.IsDeletedAccount = true;
            await this._userManager.UpdateAsync(user);
            await this._signInManager.SignOutAsync();
            return this.Ok();
        }

        private void detachLocalAddress(Address address)
        {
            var local = _context.Set<Address>().Local.FirstOrDefault(entry => entry.Id.Equals(address.Id));
            if (local != null)
            {
                _context.Entry(local).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
            _context.Entry(address).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        private void detachLocalUser(User user)
        {
            var local = _context.Set<User>().Local.FirstOrDefault(entry => entry.Id.Equals(user.Id));
            if (local != null)
            {
                _context.Entry(local).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        private void detachLocalLocation(Location location)
        {
            var local = _context.Set<Location>().Local.FirstOrDefault(entry => entry.Id.Equals(location.Id));
            if (local != null)
            {
                _context.Entry(local).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
            _context.Entry(location).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }


    }
}