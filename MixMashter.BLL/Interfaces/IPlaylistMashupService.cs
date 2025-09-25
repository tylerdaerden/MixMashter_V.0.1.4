using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixMashter.Models.Entities;

namespace MixMashter.BLL.Interfaces
{
    public interface IPlaylistMashupService
    {
        Task<Playlist_Mashup?> GetByIdsAsync(int playlistId, int mashupId);
        Task<IEnumerable<Playlist_Mashup>> GetByPlaylistIdAsync(int playlistId);
        Task<Playlist_Mashup?> AddAsync(int playlistId, int mashupId);
        Task<bool> RemoveAsync(int playlistId, int mashupId);
    }
}

