using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using NPComplet.YourDeals.Domain.Shared.Constant.Application;
using NPComplet.YourDeals.Domain.Shared.Constant.Storage;
using System;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Extensions
{
    public static class HubExtensions
    {
        public  static async Task<HubConnection> TryInitialize(this HubConnection hubConnection, NavigationManager navigationManager,ILocalStorageService localStorage)
        {
            if (hubConnection == null)
            {
               
                hubConnection = new HubConnectionBuilder()
                                  .WithUrl(string.Format(ApplicationConstants.ApiUrl + ApplicationConstants.SignalR.HubUrl ), async(options) =>
            {
               
                
                var savedToken =   await localStorage.GetItemAsync<string>(StorageConstants.Local.AuthToken);
               

                if (!string.IsNullOrEmpty(savedToken))
                {
                    
                    options.Headers.Add("Authorization", string.Format("Bearer {0}", savedToken));

                    //options.AccessTokenProvider = async () =>  await Task.FromResult(savedToken); 
                }
             
            }).WithAutomaticReconnect()
                                  .Build();
            }
            return hubConnection;
        }
    }
}