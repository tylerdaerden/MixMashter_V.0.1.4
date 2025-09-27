using MixMashter.Blazor.Models.Auth;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using System.Threading.Tasks;

namespace MixMashter.Blazor.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;

        public AuthApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", dto);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        }

        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/register", dto);
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        }
    }
}
