using Blazored.LocalStorage;

namespace Chat.Client.Service
{
    public class StorageService
    {
        private readonly ILocalStorageService _localStorageService;

        public StorageService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        private const string Key = "token";

        public async Task SetToken(string token)
        {
            await _localStorageService.SetItemAsStringAsync(key: Key, token);
        }
        public async Task<string?> GetToken()
        {
            var token = await _localStorageService.GetItemAsync<string>(key: Key);

            return token;
        }

        public async Task DeleteToken()
        {
            await _localStorageService.RemoveItemAsync(Key);
        }
    }
}
