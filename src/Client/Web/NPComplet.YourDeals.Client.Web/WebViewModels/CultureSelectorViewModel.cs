using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using NPComplet.YourDeals.Client.Shared.ViewModels.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Client.Web.WebViewModels
{
    public class CultureSelectorViewModel:Prism.Mvvm.BindableBase, ICultureSelectorViewModel
    {
        private ILocalStorageService _localStorageService;
        private NavigationManager _navman;
        public CultureSelectorViewModel(ILocalStorageService localStorageService,NavigationManager navman)
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
        
        public CultureInfo[] cultures { get; set; }
        public CultureInfo Culture
        {
            get => CultureInfo.CurrentCulture;
            set
            {
                if (CultureInfo.CurrentCulture != value)
                {
                    _localStorageService.SetItemAsync("blazorCulture", value.Name);
                    CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(value.Name);
                    CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(value.Name);
                    _navman.NavigateTo(_navman.Uri, forceLoad: true);
                }
            }
        }


        }
}
