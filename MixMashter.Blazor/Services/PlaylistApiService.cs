using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using MixMashter.Models.DTOs; // <-- référence à tes DTOs backend

namespace MixMashter.Blazor.Services
{
    public class PlaylistApiService
    {
        private readonly HttpClient _http;

        public PlaylistApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<PlaylistReadDto>?> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<PlaylistReadDto>>("Playlist");
        }

        public async Task<PlaylistReadDto?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<PlaylistReadDto>($"Playlist/{id}");
        }
    }
}
