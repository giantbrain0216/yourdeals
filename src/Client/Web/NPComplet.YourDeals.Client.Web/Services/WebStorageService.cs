using NPComplet.YourDeals.Client.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace NPComplet.YourDeals.Client.Web.Services
{
    public class WebStorageService : IStorageService
    {
        private ILocalStorageService _localStorage;
        public WebStorageService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        public async Task<bool> DeleteAsync(string key)
        {
            await _localStorage.RemoveItemAsync(key);
            return true;
        }

        public async  Task<TValue> GetAsync<TValue>(string key)
        {
            var result = await _localStorage.GetItemAsync<TValue>(key);
            return result;
        }

        public async  Task SetAsync(string key, object value)
        {
             await _localStorage.SetItemAsync(key, value);
       
        }
    }
}
