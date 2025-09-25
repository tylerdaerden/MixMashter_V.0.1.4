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
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<Artist?> GetByIdAsync(int id)
        {
            return await _artistRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Artist>> GetAllAsync()
        {
            return await _artistRepository.GetAllAsync();
        }

        public async Task<Artist?> CreateAsync(Artist artist)
        {
            return await _artistRepository.AddAsync(artist);
        }

        public async Task<bool> UpdateAsync(Artist artist)
        {
            return await _artistRepository.UpdateAsync(artist);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _artistRepository.DeleteAsync(id);
        }
    }
}

