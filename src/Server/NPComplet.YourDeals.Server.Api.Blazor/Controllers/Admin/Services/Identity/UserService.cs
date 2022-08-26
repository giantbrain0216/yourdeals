using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces;
using NPComplet.YourDeals.Server.Api.Blazor.Controllers.Admin.Services.Identity.Interfaces;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using Hangfire;
using NPComplet.YourDeals.Domain.Shared.Constant.Role;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.Exceptions;
using NPComplet.YourDeals.Domain.Shared.Specifications;
using NPComplet.YourDeals.Server.Api.Blazor.Extensions;
using NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Models;
using NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Service;
using NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Views.Emails.ConfirmAccount;
using NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Views.Emails.ForgetPassword;
using NPComplet.YourDeals.Translations.Resources;

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Admin.Services.Identity
{
    /// <summary>
    /// 
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<Translation> _localizer;
        private readonly IExcelService _excelService;

        private readonly IBackgroundJobService _backgroundJobService;

        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="emailSender"></param>
        /// <param name="localizer"></param>
        /// <param name="excelService"></param>


        public UserService(
            UserManager<User> userManager,

            RoleManager<Role> roleManager,
            IEmailSender emailSender,
            IStringLocalizer<Translation> localizer,
            IExcelService excelService, IBackgroundJobService backgroundJobService,
            IRazorViewToStringRenderer razorViewToStringRenderer
            )
        {
            _userManager = userManager;

            _roleManager = roleManager;
            _emailSender = emailSender;
            _localizer = localizer;
            _excelService = excelService;
            this._backgroundJobService = backgroundJobService;
            _razorViewToStringRenderer = razorViewToStringRenderer;


        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<User>>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            return await Result<List<User>>.SuccessAsync(users);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public async Task<IResult> RegisterAsync(RegisterViewModel request, string origin)
        {

            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,

            };



            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleConstants.BasicRole);

                    var verificationUri = await SendVerificationEmail(user, origin);

                    var subject = _localizer["RegisterAccountTitle"];
                    var resetMsg = _localizer["RegisterAccountMsgBody"];
                    var assistantMessage = _localizer["RegisterAccountAssistantMessage"];
                    var welcome = _localizer["RegisterAccountWelcome"];
                    var confirmtext = _localizer["RegisterAccountPleaseEmailConfirm"];

                    IRazorHtmlModel confirmAccountModel = new AccountGenericEmailViewModel("", $"{verificationUri}", confirmtext, welcome, resetMsg, assistantMessage);

                    string body = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/ConfirmAccount/AccountGenericEmail.cshtml", confirmAccountModel);


                    this._backgroundJobService.Enqueue(() => _emailSender.
                        SendHtmlEmailAsync(user.Email, subject, body));


                    return await Result<string>.SuccessAsync(user.Id.ToString(), string.Format(_localizer["User {0} Registered. Please check your Mailbox to verify!"], user.UserName));

                }
                else
                {
                    return await Result.FailAsync(result.Errors.Select(a => _localizer[a.Description].ToString()).ToList());
                }
            }
            else
            {
                return await Result.FailAsync(string.Format(_localizer["Email {0} is already registered."], request.Email));
            }
        }

        private async Task<string> SendVerificationEmail(User user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/identity/user/confirm-email/";
            var endpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(endpointUri.ToString(), "userId", user.Id.ToString());
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            return verificationUri;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IResult<User>> GetAsync(string userId)
        {
            var user = await _userManager.Users.Where(u => u.Id == Guid.Parse(userId)).FirstOrDefaultAsync();

            return await Result<User>.SuccessAsync(user);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IResult> ToggleUserStatusAsync(ToggleUserStatusViewModel request)
        {
            var user = await _userManager.Users.Where(u => u.Id == Guid.Parse(request.UserId)).FirstOrDefaultAsync();
            var isAdmin = await _userManager.IsInRoleAsync(user, RoleConstants.AdministratorRole);
            if (isAdmin)
            {
                return await Result.FailAsync(_localizer["Administrators Profile's Status cannot be toggled"]);
            }
            if (user != null)
            {
                //user.IsActive = request.ActivateUser;
                var identityResult = await _userManager.UpdateAsync(user);
            }
            return await Result.SuccessAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IResult<UserRolesViewModel>> GetRolesAsync(string userId)
        {
            var viewModel = new List<UserRoleModel>();
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _roleManager.Roles.ToListAsync();

            foreach (var role in roles)
            {
                var userRolesViewModel = new UserRoleModel
                {
                    RoleName = role.Name,
                    RoleDescription = role.Description
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                viewModel.Add(userRolesViewModel);
            }
            var result = new UserRolesViewModel { UserRoles = viewModel };
            return await Result<UserRolesViewModel>.SuccessAsync(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IResult> UpdateRolesAsync(UpdateUserRolesViewModel request, string userId)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);


            var roles = await _userManager.GetRolesAsync(user);
            var selectedRoles = request.UserRoles.Where(x => x.Selected).ToList();

            var currentUser = await _userManager.FindByIdAsync(userId);
            if (!await _userManager.IsInRoleAsync(currentUser, RoleConstants.AdministratorRole))
            {
                var tryToAddAdministratorRole = selectedRoles
                    .Any(x => x.RoleName == RoleConstants.AdministratorRole);
                var userHasAdministratorRole = roles.Any(x => x == RoleConstants.AdministratorRole);
                if (tryToAddAdministratorRole && !userHasAdministratorRole || !tryToAddAdministratorRole && userHasAdministratorRole)
                {
                    return await Result.FailAsync(_localizer["Not Allowed to add or delete Administrator Role if you have not this role."]);
                }
            }

            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            result = await _userManager.AddToRolesAsync(user, selectedRoles.Select(y => y.RoleName));
            return await Result.SuccessAsync(_localizer["Roles Updated"]);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<IResult<string>> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return await Result<string>.SuccessAsync(user.Id.ToString(), string.Format(_localizer["Account Confirmed for {0}. You can now use the /api/identity/token endpoint to generate JWT."], user.Email));
            }
            else
            {
                throw new ApplicationCoreException(string.Format(_localizer["An error occurred while confirming {0}"], user.Email));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public async Task<IResult> ForgotPasswordAsync(ForgotPasswordViewModel request, string origin)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return await Result.FailAsync(_localizer["An Error has occurred!"]);
            }
            // For more information on how to enable account confirmation and password reset please
            // visit https://go.microsoft.com/fwlink/?LinkID=532713
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "account/reset-password";
            var endpointUri = new Uri(string.Concat($"{origin}/", route));
            var passwordResetURL = HtmlEncoder.Default.Encode(QueryHelpers.AddQueryString(endpointUri.ToString(), "Token", code));

            var subject = _localizer["ForgotPasswordTitle"];
            var resetMsg = _localizer["ForgotPasswordMsgBody"];
            var assistantMessage = _localizer["ForgotPasswordAssistantMessage"];
            var welcome = _localizer["ForgotPasswordAccountWelcome"];
            var confirmtext = _localizer["ForgotPasswordPleaseEmailConfirm"];

            IRazorHtmlModel confirmAccountModel = new ForgetPasswordEmailViewModel(user.Id.ToString(), $"{passwordResetURL}", confirmtext, welcome, resetMsg, assistantMessage);

            string body = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/ForgetPassword/ForgetPasswordAccountEmail.cshtml", confirmAccountModel);


            this._backgroundJobService.Enqueue(() => _emailSender.SendHtmlEmailAsync(request.Email, subject, body));
            return await Result.SuccessAsync(_localizer["Password Reset Mail has been sent to your authorized Email."]);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IResult> ResetPasswordAsync(ResetPasswordViewModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return await Result.FailAsync(_localizer["An Error has occured!"]);
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (result.Succeeded)
            {
                return await Result.SuccessAsync(_localizer["Password Reset Successful!"]);
            }
            else
            {
                return await Result.FailAsync(_localizer["An Error has occured!"]);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCountAsync()
        {
            var count = await _userManager.Users.CountAsync();
            return count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public async Task<string> ExportToExcelAsync(string searchString = "")
        {
            var userSpec = new UserFilterSpecification(searchString);
            var users = await _userManager.Users
                .Specify(userSpec)
                .OrderByDescending(a => a.Id)
                .ToListAsync();
            var result = await _excelService.ExportAsync(users, sheetName: _localizer["Users"],
                mappers: new Dictionary<string, Func<User, object>>
                {
                    { _localizer["Id"], item => item.Id },
                    { _localizer["FirstName"], item => item.FirstName },
                    { _localizer["LastName"], item => item.LastName },
                    //{ _localizer["UserName"], item => item.UserName },
                    { _localizer["Email"], item => item.Email },
                    { _localizer["EmailConfirmed"], item => item.EmailConfirmed },
                    //{ _localizer["PhoneNumber"], item => item.PhoneNumber },
                    //{ _localizer["PhoneNumberConfirmed"], item => item.PhoneNumberConfirmed },
                   
                   
                });

            return result;
        }
    }
}