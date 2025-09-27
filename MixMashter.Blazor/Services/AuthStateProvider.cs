using Blazored.LocalStorage;

namespace MixMashter.Blazor.Services
{
    public class AuthStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public event Action? OnChange;

        public AuthStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await _localStorage.GetItemAsStringAsync("authToken");
            return !string.IsNullOrEmpty(token);
        }

        public void NotifyAuthChanged()
        {
            OnChange?.Invoke();
        }
    }
}
