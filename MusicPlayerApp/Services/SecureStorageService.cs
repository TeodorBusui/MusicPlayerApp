using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerApp.Services
{
    public class SecureStorageService : ISecureStorageService
    {
        public async Task<bool> Contains(string key)
        {
            var savedValue = await SecureStorage.Default.GetAsync(key);

            if(savedValue == null) 
            {
                return false;
            }

            return true;
        }

        public async Task<string> Get(string key)
        {
            var savedValue = await SecureStorage.Default.GetAsync(key);

            if (savedValue == null)
            {
                throw new KeyNotFoundException();
            }

            return savedValue;
        }

        public async Task Save(string key, string value)
        {
            await SecureStorage.Default.SetAsync(key, value);
        }
    }
}
