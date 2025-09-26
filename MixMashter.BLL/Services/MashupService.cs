using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MixMashter.BLL.Interfaces;
using MixMashter.DAL.Repositories.Interfaces;
using MixMashter.Models.Entities;

namespace MixMashter.BLL.Services
{
    public class MashupService : IMashupService
    {
        private readonly IMashupRepository _mashupRepository;

        public MashupService(IMashupRepository mashupRepository)
        {
            _mashupRepository = mashupRepository;
        }

        public async Task<Mashup?> GetByIdAsync(int id)
        {
            return await _mashupRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Mashup>> GetAllAsync()
        {
            return await _mashupRepository.GetAllAsync();
        }

        public async Task<Mashup?> CreateAsync(Mashup mashup)
        {
            return await _mashupRepository.AddAsync(mashup);
        }

        public async Task<bool> UpdateAsync(Mashup mashup)
        {
            return await _mashupRepository.UpdateAsync(mashup);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _mashupRepository.DeleteAsync(id);
        }

        // Exemple de logique métier propre au Mashup (optionnel , piste de dév pour plus tard , je la garde pour plus tard , pê le TFE ? )
        public async Task<int> CalculateLengthAsync(int mashupId)
        {
            var mashup = await _mashupRepository.GetByIdWithSongsAsync(mashupId);
            if (mashup == null) return 0;

            // On additionne la durée de toutes les chansons du mashup
            return mashup.MashupSongs.Sum(ms => ms.Song.Length);
        }


        // méthodes de validation
        public bool IsValidFormat(string format)
        {
            if (string.IsNullOrWhiteSpace(format))
                return false;

            var allowedFormats = new[] { "mp3", "wav", "flac", "aac" };

            return allowedFormats.Any(f =>
                string.Equals(format.Trim(), f, StringComparison.OrdinalIgnoreCase));
        }


        public bool IsValidUrlLink(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            // Accepte http, https et ftp
            return Uri.TryCreate(url, UriKind.Absolute, out var uri)
                   && (uri.Scheme == Uri.UriSchemeHttp
                       || uri.Scheme == Uri.UriSchemeHttps
                       || uri.Scheme == Uri.UriSchemeFtp);
        }

        public bool IsValidCoverImage(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            // Vérifie que c’est une URL absolue valide
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uriResult))
                return false;

            // Extensions autorisées
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

            return allowedExtensions.Any(ext =>
                uriResult.AbsolutePath.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }

    }
}