using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixMashter.Models.Entities;


namespace MixMashter.DAL.Repositories.Interfaces
{
    public interface IPlaylistMashupRepository
    {
        Task<Playlist_Mashup?> GetByIdsAsync(int playlistId, int mashupId);
        Task<IEnumerable<Playlist_Mashup>> GetByPlaylistIdAsync(int playlistId);
        Task<Playlist_Mashup?> AddAsync(Playlist_Mashup playlistMashup);
        Task<bool> DeleteAsync(int playlistId, int mashupId);
    }
}

