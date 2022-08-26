// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SeedData.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Init
{
    #region

    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using NPComplet.YourDeals.Domain.Shared.Users;
    using NPComplet.YourDeals.Server.Infrastructure.Repositories;
    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;

    #endregion

    /// <summary>
    /// </summary>
    public static class SeedDataInDatabase
    {
        /// <summary>
        /// </summary>
        /// <param name="serviceProvider">
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

            await IdentityDataInitializer.SeedRoles(roleManager);
            await IdentityDataInitializer.SeedUser(userManager);
            await IdentityDataInitializer.SeedOffers(unitOfWork);
            await IdentityDataInitializer.SeedDeals(unitOfWork);
            await IdentityDataInitializer.AddOtherRecords(unitOfWork);
            await unitOfWork.CommitAsync();
        }
    }
}