using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Microsoft.MobileBlazorBindings.ProtectedStorage;

namespace NPComplet.YourDeals.Client.Shared.Services
{
    public class StorageService : IStorageService
    {
        private IProtectedStorage _localStorage;

        public StorageService(IProtectedStorage localStorage)
        {
            _localStorage = localStorage;
        }
         
        public Task<bool> DeleteAsync(string key)
        {
            return _localStorage.DeleteAsync(key);
        }

        public Task<TValue> GetAsync<TValue>(string key)
        {
            return _localStorage.GetAsync<TValue>( key);
        }

        public Task SetAsync(string key, object value)
        {
            return _localStorage.SetAsync(key,value);
        }
    }
}
