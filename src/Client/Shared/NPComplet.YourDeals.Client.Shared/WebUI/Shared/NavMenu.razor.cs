using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Client.Shared.WebUI.Shared
{
    public partial class NavMenu :ComponentBase
    {
        [Inject] IJSRuntime JSRuntime { get; set; }
        public bool collapseNavMenu = true;

        public string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        public void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        protected override async Task OnInitializedAsync()
        {
           // await JSRuntime.InvokeVoidAsync("Searchbar");
        }

    }
}
