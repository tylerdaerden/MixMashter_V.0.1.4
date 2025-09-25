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
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;

        public PlaylistService(IPlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        public async Task<Playlist?> GetByIdAsync(int id)
        {
            return await _playlistRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Playlist>> GetAllAsync()
        {
            return await _playlistRepository.GetAllAsync();
        }

        public async Task<Playlist?> CreateAsync(Playlist playlist)
        {
            return await _playlistRepository.AddAsync(playlist);
        }

        public async Task<bool> UpdateAsync(Playlist playlist)
        {
            return await _playlistRepository.UpdateAsync(playlist);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _playlistRepository.DeleteAsync(id);
        }

        //bonus chokotoff
        public async Task<int> GetSongCountAsync(int playlistId)
        {
            var playlist = await _playlistRepository.GetByIdWithMashupsAsync(playlistId);
            if (playlist == null) return 0;

            return playlist.PlaylistMashups.Count;
        }
    }
}
