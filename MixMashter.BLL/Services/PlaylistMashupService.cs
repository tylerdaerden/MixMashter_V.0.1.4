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
    public class PlaylistMashupService : IPlaylistMashupService
    {
        private readonly IPlaylistMashupRepository _playlistMashupRepository;

        public PlaylistMashupService(IPlaylistMashupRepository playlistMashupRepository)
        {
            _playlistMashupRepository = playlistMashupRepository;
        }

        public async Task<Playlist_Mashup?> GetByIdsAsync(int playlistId, int mashupId)
        {
            return await _playlistMashupRepository.GetByIdsAsync(playlistId, mashupId);
        }

        public async Task<IEnumerable<Playlist_Mashup>> GetByPlaylistIdAsync(int playlistId)
        {
            return await _playlistMashupRepository.GetByPlaylistIdAsync(playlistId);
        }

        public async Task<Playlist_Mashup?> AddAsync(int playlistId, int mashupId)
        {
            var playlistMashup = new Playlist_Mashup
            {
                PlaylistId = playlistId,
                MashupId = mashupId,
                AddedDate = DateTime.UtcNow
            };

            return await _playlistMashupRepository.AddAsync(playlistMashup);
        }

        public async Task<bool> RemoveAsync(int playlistId, int mashupId)
        {
            return await _playlistMashupRepository.DeleteAsync(playlistId, mashupId);
        }
    }
}


