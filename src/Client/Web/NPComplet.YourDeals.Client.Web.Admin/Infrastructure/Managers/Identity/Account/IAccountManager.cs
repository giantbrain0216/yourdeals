using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.ManageViewModels;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Managers.Identity.Account
{
    public interface IAccountManager : IManager
    {
        Task<IResult> ChangePasswordAsync(ChangePasswordViewModel model);

        //Task<IResult> UpdateProfileAsync(UpdateOrifl model);

        Task<IResult<string>> GetProfilePictureAsync(string userId);

       // Task<IResult<string>> UpdateProfilePictureAsync(UpdateProfilePictureRequest request, string userId);
    }
}
