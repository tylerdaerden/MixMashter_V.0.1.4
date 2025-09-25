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
    public class SongService : ISongService
    {
        private readonly ISongRepository _songRepository;

        public SongService(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task<Song?> GetByIdAsync(int id)
        {
            return await _songRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Song>> GetAllAsync()
        {
            return await _songRepository.GetAllAsync();
        }

        public async Task<Song?> CreateAsync(Song song)
        {
            return await _songRepository.AddAsync(song);
        }

        public async Task<bool> UpdateAsync(Song song)
        {
            return await _songRepository.UpdateAsync(song);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _songRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Song>> GetExplicitSongsAsync()
        {
            var songs = await _songRepository.GetAllAsync();
            return songs.Where(s => s.IsExplicit);
        }
    }
}
