using MixMashter.BLL.Interfaces;
using MixMashter.DAL.Repositories.Interfaces;
using MixMashter.Models.Entities;
using System.Text.RegularExpressions;

namespace MixMashter.BLL.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<IEnumerable<Artist>> GetAllAsync()
        {
            return await _artistRepository.GetAllAsync();
        }

        public async Task<Artist?> GetByIdAsync(int id)
        {
            return await _artistRepository.GetByIdAsync(id);
        }

        public async Task<Artist> AddAsync(Artist artist)
        {
            if (!IsValidArtistName(artist.ArtistName))
                throw new ArgumentException("Nom d'artiste invalide");
            if (!IsValidBand(artist.Band))
                throw new ArgumentException("Nom de groupe invalide");
            if (!IsValidWebsiteLink(artist.WebsiteLink))
                throw new ArgumentException("Lien de site web invalide");

            return await _artistRepository.AddAsync(artist);
        }

        public async Task<bool> UpdateAsync(Artist artist)
        {
            if (!IsValidArtistName(artist.ArtistName)) return false;
            if (!IsValidBand(artist.Band)) return false;
            if (!IsValidWebsiteLink(artist.WebsiteLink)) return false;

            return await _artistRepository.UpdateAsync(artist);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _artistRepository.DeleteAsync(id);
        }

        // mes méthodes de validation pour le service Artist
        public bool IsValidArtistName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Length <= 100;
        }

        public bool IsValidBand(string? band)
        {
            return string.IsNullOrEmpty(band) || band.Length <= 100;
        }

        public bool IsValidWebsiteLink(string? websiteLink)
        {
            if (string.IsNullOrEmpty(websiteLink)) return true;
            return Regex.IsMatch(websiteLink, @"^https?:\/\/[\w\-]+(\.[\w\-]+)+[/#?]?.*$");
        }
    }
}
