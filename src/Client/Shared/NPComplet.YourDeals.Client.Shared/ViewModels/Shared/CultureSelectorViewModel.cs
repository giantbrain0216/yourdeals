using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.ProtectedStorage;
using NPComplet.YourDeals.Client.Shared.ViewModels.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using NPComplet.YourDeals.Client.Shared.ViewModels.Base;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Shared
{
    public class CultureSelectorViewModel : BaseViewModel , ICultureSelectorViewModel
    {

        private IProtectedStorage _localStorageService;
        private NavigationManager _navman;
        public CultureSelectorViewModel(IProtectedStorage localStorageService,NavigationManager navman)
        {
            _localStorageService = localStorageService;
            _navman = navman;
            cultures = new[]
                                 {
                                     new CultureInfo("en"),
                                     new CultureInfo("fr"),
                                     new CultureInfo("ar")
                                 };
        }

        public CultureInfo[] cultures { get ; set ; }
        public CultureInfo Culture
        {
            get => CultureInfo.DefaultThreadCurrentCulture;
            set
            {
               
                    _localStorageService.SetAsync("blazorCulture", value.Name).Wait();
                    CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(value.Name);
                    CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(value.Name);
                    _navman.NavigateTo(_navman.Uri, forceLoad: true);
            
            }
        }

       
    }
}
