// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountControllerUnitTests.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Tests.API.Tests.Controllers
{/*
    #region

    using System;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FluentAssertions;

    using Hangfire;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.AccountViewModels;
    using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.ManageViewModels;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using NPComplet.YourDeals.Server.Api.Blazor.Controllers.Users;
    using NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Service;
    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;
    using NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces;
    using NPComplet.YourDeals.Tests.TestsHelper;
    using NPComplet.YourDeals.Translations.Resources;

    using Xunit;
    using Xunit.Abstractions;

    using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

    #endregion

    /// <summary>
    /// The account controller unit tests.
    /// </summary>
    public class AccountControllerUnitTests : UnitBase
    {
        private readonly Mock<IConfiguration> _configuration;

        private readonly Mock<IEmailSender> _emailSender;

        private readonly Mock<ILogger<AccountsController>> _logger;

        private readonly Mock<FakeSignInManager> _signInManager;

        private readonly Mock<FakeRoleManager> _roleManager;

        private readonly AccountsController _sut;

        private readonly Mock<IUnitOfWork> _unitOfWork;

        private readonly Mock<FakeUserManager> _userManager;

        private readonly Mock<IStringLocalizer<Translation>> _localizer;

        private readonly Mock<IHttpContextAccessor> _httpContextAccessor;

        private readonly Mock<IBackgroundJobService> _backgroundJobService;

        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

        private readonly Mock<IHttpClientFactory> _httpClientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountControllerUnitTests"/> class.
        /// </summary>
        /// <param name="testOutputHelper">
        /// The test output helper.
        /// </param>
        public AccountControllerUnitTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            this._roleManager = new Mock<FakeRoleManager>();
            this._logger = new Mock<ILogger<AccountsController>>();
            this._unitOfWork = new Mock<IUnitOfWork>();
            this._emailSender = new Mock<IEmailSender>();
            this._userManager = new Mock<FakeUserManager>();
            this._signInManager = new Mock<FakeSignInManager>();
            this._configuration = new Mock<IConfiguration>();
            this._localizer = new Mock<IStringLocalizer<Translation>>();
            this._backgroundJobService = new Mock<IBackgroundJobService>();
            
            this._httpContextAccessor = new Mock<IHttpContextAccessor>();
            this._httpClientFactory = new Mock<IHttpClientFactory>();

            this._sut = new AccountsController(
                this._roleManager.Object,
                this._userManager.Object,
                this._signInManager.Object,
                this._emailSender.Object,
                this._logger.Object,
                this._configuration.Object,
                this._localizer.Object, 
                this._httpContextAccessor.Object,
                this._backgroundJobService.Object,
                 this._razorViewToStringRenderer,
                 this._httpClientFactory.Object
                 
                );

            _backgroundJobService.Setup(x => x.Enqueue(It.IsAny<Expression<Func<Task>>>())).Returns(string.Empty);
        }




/*
        /// <summary>
        /// The external login for existing user_ should return ok.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        [TestMethod]
        public async Task ExternalLoginForExistingUser_ShouldReturnOk()
        {
            var model = new ExternalLoginViewModel
                            {
                                ExternalId = "123",
                                FistName = "user",
                                LastName = "user",
                                Email = "mahfuzfsl@gmail.com",
                                Provider = "Google"
                            };
            this._userManager.Setup(manager => manager.FindByEmailAsync(model.Email))
                .ReturnsAsync(new Mock<User>().Object);
            this._userManager.Setup(manager => manager.AddLoginAsync(It.IsAny<User>(), It.IsAny<UserLoginInfo>()))
                .ReturnsAsync(IdentityResult.Success);
            this.SetIConfigurationMock();

            var result = await this._sut.ExternalLogin(model);

            // Assert
            var okObjectResult = result as OkObjectResult;
        }

        /// <summary>
        /// The forgor password_ should.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        [TestMethod]
        public async Task ForgorPassword_Should()
        {
            var model = new ForgotPasswordViewModel { Email = "mahfuzfsl@gmail.com", NewPassword = "test1234" };

            var result = await this._sut.ForgotPassword(model);

            // Assert
            var okObjectResult = result as OkObjectResult;
        }

        /// <summary>
        /// The login_ should.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        [TestMethod]
        public async Task Login_Should()
        {
            var model = new LoginViewModel { Email = "mahfuzfsl@gmail.com", Password = "test1234", RememberMe = false };
            this._signInManager
                .Setup(manager => manager.PasswordSignInAsync(model.Email, model.Password, false, model.RememberMe))
                .ReturnsAsync(SignInResult.Success);
            this._userManager.Setup(manager => manager.FindByEmailAsync(model.Email))
                .ReturnsAsync(new Mock<User>().Object);
            this.SetIConfigurationMock();

            var result = await this._sut.Login(model);

            // Assert
            var okObjectResult = result as OkObjectResult;
        }


        /// <summary>
        /// The logout_ should.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        public async Task Logout_Should()
        {
            this._signInManager.Setup(manager => manager.SignOutAsync()).Returns(Task.CompletedTask);

            var result = await this._sut.Logout();

            // Assert
            var okObjectResult = result as OkObjectResult;
        }

        /// <summary>
        /// The register_ should.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        [TestMethod]
        public async Task Register_Should()
        {
            // Arrange
            var model = new RegisterViewModel
                            {
                                FirstName = "test",
                                LastName = "test",
                                Email = "mahfuzfsl@gmail.com",
                                Password = "test1234",
                                ConfirmPassword = "test1234",
                                
                            };
            var x = new Mock<BackgroundJob>();
            this._userManager.Setup(manager => manager.CreateAsync(It.IsAny<User>(), model.Password))
                .ReturnsAsync(IdentityResult.Success);
            this._userManager.Setup(manager => manager.GenerateEmailConfirmationTokenAsync(It.IsAny<User>()))
                .ReturnsAsync("token123");

            // Act
            var result = await this._sut.Register(model);

            // Assert
            // Check that send email method is called
            this._backgroundJobService.Verify(e => e.Enqueue(It.IsAny<Expression<Func<Task>>>()), Times.Once);
            var okObjectResult = result as OkObjectResult;
        }

      

        /// <summary>
        /// The reset password_ should.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        [TestMethod]
        public async Task ResetPassword_Should()
        {
            var result = await this._sut.ResetPassword(Guid.NewGuid(), "test123", "123");

            // Assert
            var okObjectResult = result as OkObjectResult;
        }

        /// <summary>
        /// The send verification email_ should.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
  

        /// <summary>
 
        private void SetIConfigurationMock()
        {
            this._configuration.SetupGet(s => s["Security:Tokens:Key"]).Returns("1234567890 a very long word");
            this._configuration.SetupGet(s => s["Security:Tokens:Issuer"]).Returns(".com");
            this._configuration.SetupGet(s => s["Security:Tokens:ExpirationDaysInteger"]).Returns("500");
        }
    }*/
}
