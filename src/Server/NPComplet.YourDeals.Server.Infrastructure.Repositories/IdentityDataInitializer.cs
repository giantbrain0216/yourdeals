using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NPComplet.YourDeals.Domain.Shared.Constant.Permission;
using NPComplet.YourDeals.Domain.Shared.Deal;
using NPComplet.YourDeals.Domain.Shared.Deal.Offers;
using NPComplet.YourDeals.Domain.Shared.Enumerable;
using NPComplet.YourDeals.Domain.Shared.Markets;
using NPComplet.YourDeals.Domain.Shared.Shared;
using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;

using System.Security.Claims;
using NPComplet.YourDeals.Domain.Shared.Constant.Role;

namespace NPComplet.YourDeals.Server.Infrastructure.Repositories
{
    public static class IdentityDataInitializer
    {
        public static async Task SeedRoles(RoleManager<Role> roleManager)
        {
            Task.Run(async () =>
            {
                //Check if Role Exists
                var adminRole = new Role { Name = RoleConstants.AdministratorRole, Description = "Administrator role with full permissions" };
                var adminRoleInDb = await roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
                if (adminRoleInDb == null)
                {
                    await roleManager.CreateAsync(adminRole);
                    adminRoleInDb = await roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
                   
                }
                foreach (var permission in Permissions.GetRegisteredPermissions())
                {
                    await roleManager.AddPermissionClaimSeeder(adminRoleInDb, permission);
                }

                var basicRole = new Role { Name = RoleConstants.BasicRole, Description = "Basic role with default permissions" };
                var basicRoleInDb = await roleManager.FindByNameAsync(RoleConstants.BasicRole);
                if (basicRoleInDb == null)
                {
                    await roleManager.CreateAsync(basicRole);
                    
                }
                //Check if User Exists

            }).GetAwaiter().GetResult();
        }

        public static async Task SeedUser(UserManager<User> userManager)
        {
            if (userManager.Users.Count(u => u.Id == new Guid("DD963DC8-05C4-49CC-29C9-08D63B407BCC")) != 1)
            {
                var user1 = new User
                {
                    Id = new Guid("DD963DC8-05C4-49CC-29C9-08D63B407BCC"),
                    FirstName = "Manjider",
                    LastName = "Singh",
                    PhoneNumber = " ",
                    UserName = "manjinder.impinge@gmail.com",
                    NormalizedUserName = "SAEDBFD",
                    Email = "manjinder.impinge@gmail.com",
                    NormalizedEmail = "MANJINDER.IMPINGE@GMAIL.COM",
                    // gender = 1,
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEFfx25XNC1Ds9icqbOs5/VjesQJDcc3YM0h/5JrWTaU7HMMEGuy/ye2Jm7e0cNey5A==",
                    LockoutEnabled = true,
                    SecurityStamp = "6QCXDQYZCPY4HWCQUYGWN2XXQSPT5LKM",
                    ConcurrencyStamp = "20114bbb-3900-460e-800d-1f08d2524395",
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = false
                };
                await userManager.CreateAsync(user1);

                var user2 = new User
                {
                    Id = new Guid("1B56B118-20D7-4A59-29CA-08D63B407BCC"),
                    FirstName = "Ashu",
                    LastName = "Singh",
                    PhoneNumber = " ",
                    UserName = "ashu.impinge@gmail.com",
                    NormalizedUserName = "SAEDBFD",
                    Email = "ashu.impinge@gmail.com",
                    NormalizedEmail = "ASHU.IMPINGE@GMAIL.COM",
                    // gender = 1,
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEFfx25XNC1Ds9icqbOs5/VjesQJDcc3YM0h/5JrWTaU7HMMEGuy/ye2Jm7e0cNey5A==",
                    LockoutEnabled = true,
                    SecurityStamp = "6QCXDQYZCPY4HWCQUYGWN2XXQSPT5LKM",
                    ConcurrencyStamp = "20114bbb-3900-460e-800d-1f08d2524395",
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = false
                };
                await userManager.CreateAsync(user2);
              
            }
            var superUser = new User
            {
                FirstName = "Amr",
                LastName = "Elsherif",
                Email = "amr.elsherif83@outlook.com",
                UserName = "amr.elsherif83@outlook.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

            };
            var superUserInDb = await userManager.FindByEmailAsync(superUser.Email);
            if (superUserInDb == null)
            {
                await userManager.CreateAsync(superUser, "string");
                var result = await userManager.AddToRoleAsync(superUser, RoleConstants.AdministratorRole);
             
            }
       
        }

        public static async Task AddOtherRecords(IUnitOfWork unitOfWork)
        {
            if (unitOfWork.GetRepository<Markets>().GetByIdAsync(new Guid("5B56B120-20D7-4A59-29CA-08D63B407BFD"))
                .Result == null)
                await unitOfWork.GetRepository<Markets>().AddAsync(new Markets
                {
                    Id = new Guid("5B56B120-20D7-4A59-29CA-08D63B407BFD"),
                    OwnerId = new Guid("1B56B118-20D7-4A59-29CA-08D63B407BCC"),
                    InternalCreationDateTimeUtc = DateTime.UtcNow,
                    InternalModificationDateTimeUtc = DateTime.UtcNow,
                    InternalValidation = true
                });
            if (unitOfWork.GetRepository<Market>().GetByIdAsync(new Guid("5B57C120-20D7-4A59-29CA-08D63B407BFD"))
                .Result == null)
                await unitOfWork.GetRepository<Market>().AddAsync(new Market
                {
                    Id = new Guid("5B57C120-20D7-4A59-29CA-08D63B407BFD"),
                    MarketsId = new Guid("5B56B120-20D7-4A59-29CA-08D63B407BFD"),
                    OwnerId = new Guid("1B56B118-20D7-4A59-29CA-08D63B407BCC"),
                    InternalCreationDateTimeUtc = DateTime.UtcNow,
                    InternalModificationDateTimeUtc = DateTime.UtcNow,
                    InternalValidation = true,
                    MarketStartTimeUtc = DateTime.UtcNow.Date.Add(TimeSpan.Parse("10:00")),
                    MarketEndTimeUtc = DateTime.UtcNow.Date.Add(TimeSpan.Parse("20:00")),
                    OpeningTimeSpan = TimeSpan.Parse("10:00")
                });
            if (unitOfWork.GetRepository<Company>().GetByIdAsync(new Guid("5B57C120-20D7-4A78-29CA-08D63B407BFD"))
                .Result == null)
                await unitOfWork.GetRepository<Company>().AddAsync(new Company
                {
                    Id = new Guid("5B57C120-20D7-4A78-29CA-08D63B407BFD"),
                    OwnerId = new Guid("1B56B118-20D7-4A59-29CA-08D63B407BCC"),
                    InternalCreationDateTimeUtc = DateTime.UtcNow,
                    InternalModificationDateTimeUtc = DateTime.UtcNow,
                    InternalValidation = true,
                    CompanyName = "Sharma Interprises Pvt Ltd",
                    CompanyIdentifier = "Company Identifier test test",
                    TaxIdentifierNumber = "XVRSD56644SFGD78",
                    LegalForm = "Legal form is downloaded from website."
                });
        }

        public static async Task SeedOffers(IUnitOfWork unitOfWork)
        {
            try
            {
                if (unitOfWork.GetRepository<Offer>().GetByIdAsync(new Guid("9B56B118-20D7-4A59-29CA-08D63B407BFD"))
                    .Result == null)
                    await unitOfWork.GetRepository<Offer>().AddAsync(new Offer
                    {
                        Id = new Guid("9B56B118-20D7-4A59-29CA-08D63B407BFD"),
                        InternalCreationDateTimeUtc = DateTime.UtcNow,
                        InternalModificationDateTimeUtc = DateTime.UtcNow,
                        InternalValidation = true,
                        OwnerId = new Guid("1B56B118-20D7-4A59-29CA-08D63B407BCC"),
                        Quotation = null,
                        AddressId = new Guid("7B56C118-20D7-4A59-29CA-08D63B407BCC")
                    });
            }
            catch (Exception)
            {
            }
        }

        public static async Task SeedDeals(IUnitOfWork unitOfWork)
        {
            try

            {
                if (unitOfWork.GetRepository<Location>().GetByIdAsync(new Guid("5B56B118-20D7-4A59-29CA-08D63B407BFD"))
                    .Result == null)
                    await unitOfWork.GetRepository<Location>().AddAsync(new Location
                    {
                        Id = new Guid("5B56B118-20D7-4A59-29CA-08D63B407BFD"),
                        CountryCode = "IND",
                        OwnerId = new Guid("1B56B118-20D7-4A59-29CA-08D63B407BCC"),
                        InternalCreationDateTimeUtc = DateTime.UtcNow,
                        Latitude = 48.9566,
                        Longitude = 2.3522,
                        StateCode = "PB"
                    });
                if (unitOfWork.GetRepository<Address>().GetByIdAsync(new Guid("7B56C118-20D7-4A59-29CA-08D63B407BCC"))
                    .Result == null)
                    await unitOfWork.GetRepository<Address>().AddAsync(new Address
                    {
                        Id = new Guid("7B56C118-20D7-4A59-29CA-08D63B407BCC"),
                        AddressType = AddressType.Home,
                        City = "Abc",
                        HouseNumber = 123,
                        //Location=null,
                        LocationId = new Guid("5B56B118-20D7-4A59-29CA-08D63B407BFD"),
                        Phone = "1234567890",
                        PhoneCountyCode = "91",
                        OwnerId = new Guid("1B56B118-20D7-4A59-29CA-08D63B407BCC"),
                        NumCountryCode = "91",
                        State = "PB",
                        Street = "abc",
                        StreetAddress = "#123",
                        InternalCreationDateTimeUtc = DateTime.UtcNow,
                        InternalModificationDateTimeUtc = DateTime.UtcNow,
                        //Owner=null,
                        InternalValidation = true,
                        ZipCode = "25632"
                    });
                if (unitOfWork.GetRepository<Deal>().GetByIdAsync(new Guid("8B56C118-20D7-4A59-29CA-08D63B407BCC"))
                        .Result == null)
                    //IGenericRepository<Deal> repository;
                    await unitOfWork.GetRepository<Deal>().AddAsync(new Deal
                    {
                        Id = new Guid("8B56C118-20D7-4A59-29CA-08D63B407BCC"),
                        InternalValidation = true,
                        AddressId = new Guid("7B56C118-20D7-4A59-29CA-08D63B407BCC"),
                        // AskedPriceId= new System.Guid("8B56B118-20D7-4A59-29CA-08D63B407BFD"),
                        DealDocument = new DealDocument {Title = "Good Deal"},

                        OwnerId = new Guid("1B56B118-20D7-4A59-29CA-08D63B407BCC"),

                        InternalCreationDateTimeUtc = DateTime.UtcNow,
                        InternalModificationDateTimeUtc = DateTime.UtcNow,
                        //Owner = null,
                        IsDone = false,
                        IsLocked = false
                    });
            }
            catch (Exception)
            {
            }
        }
    }
    public static class ClaimsHelper
    {
        public static async Task<IdentityResult> AddPermissionClaimSeeder(this RoleManager<Role> roleManager, Role role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == ApplicationClaimTypes.Permission && a.Value == permission))
            {
                return await roleManager.AddClaimAsync(role, new Claim(ApplicationClaimTypes.Permission, permission));
            }

            return IdentityResult.Failed();
        }
    }
}
