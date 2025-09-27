using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

// Alias pour lever les ambiguïtés
using ApiDtos = MixMashter.Models.DTOs;
using ClientDtos = MixMashter.Blazor.Models.Mashup;
using System.Net.Http.Headers;
using System.IO;

namespace MixMashter.Blazor.Services
{
    public class MashupApiService
    {
        private readonly HttpClient _http;

        public MashupApiService(HttpClient http)
        {
            _http = http;
        }

        // --- 📌 Récupérer tous les mashups (DTO côté API) ---
        public async Task<IEnumerable<ApiDtos.MashupReadDto>?> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<ApiDtos.MashupReadDto>>("Mashup");
        }

        // --- 📌 Récupérer un mashup par ID (DTO côté API) ---
        public async Task<ApiDtos.MashupReadDto?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<ApiDtos.MashupReadDto>($"Mashup/{id}");
        }

        // --- 📌 Ajouter un mashup (depuis DTO **client** Blazor) ---
        public async Task<ApiDtos.MashupReadDto?> AddAsync(ClientDtos.MashupCreateDto clientDto)
        {
            var apiDto = new ApiDtos.MashupCreateDto
            {
                UserId = clientDto.UserId,
                Title = clientDto.Title,
                Length = clientDto.Length,
                IsExplicit = clientDto.IsExplicit,
                Description = clientDto.Description,
                Format = clientDto.Format,
                UrlLink = clientDto.UrlLink,
                CoverImage = clientDto.CoverImage
            };

            var response = await _http.PostAsJsonAsync("Mashup", apiDto);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<ApiDtos.MashupReadDto>();
        }

        // --- 📌 (Optionnel) Ajouter un mashup (si on te passe déjà le DTO **API**) ---
        public async Task<ApiDtos.MashupReadDto?> AddAsync(ApiDtos.MashupCreateDto apiDto)
        {
            var response = await _http.PostAsJsonAsync("Mashup", apiDto);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<ApiDtos.MashupReadDto>();
        }

        // --- 📌 Upload d’un fichier (cover ou audio) ---
        public async Task<string?> UploadFileAsync(Stream content, string fileName, string contentType)
        {
            using var form = new MultipartFormDataContent();
            var streamContent = new StreamContent(content);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            form.Add(streamContent, "file", fileName);

            var response = await _http.PostAsync("Mashup/upload", form);
            if (!response.IsSuccessStatusCode) return null;

            var result = await response.Content.ReadFromJsonAsync<UploadResult>();
            return result?.Url;
        }

        private class UploadResult
        {
            public string Url { get; set; } = string.Empty;
        }
    }
}
