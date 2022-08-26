
using MudBlazor;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;

namespace NPComplet.YourDeals.Client.Web.Admin.Pages.Identity
{
    public partial class Forgot : ComponentBase
    {
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private readonly ForgotPasswordViewModel _emailModel = new();

        private async Task SubmitAsync()
        {
            var result = await _userManager.ForgotPasswordAsync(_emailModel);
            if (result.Succeeded)
            {
                _snackBar.Add(_localizer["Done!"], Severity.Success);
                _navigationManager.NavigateTo("/");
            }
            else
            {
                foreach (var message in result.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
    }
}