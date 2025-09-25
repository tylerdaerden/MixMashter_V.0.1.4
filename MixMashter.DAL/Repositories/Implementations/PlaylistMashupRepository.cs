using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MixMashter.DAL.Db;
using MixMashter.DAL.Repositories.Interfaces;
using MixMashter.Models.Entities;


namespace MixMashter.DAL.Repositories.Implementations
{
    public class PlaylistMashupRepository : IPlaylistMashupRepository
    {
        private readonly MixMashterDbContext _context;

        public PlaylistMashupRepository(MixMashterDbContext context)
        {
            _context = context;
        }

        public async Task<Playlist_Mashup?> GetByIdsAsync(int playlistId, int mashupId)
        {
            return await _context.PlaylistMashups
                .Include(pm => pm.Mashup)
                .FirstOrDefaultAsync(pm => pm.PlaylistId == playlistId && pm.MashupId == mashupId);
        }

        public async Task<IEnumerable<Playlist_Mashup>> GetByPlaylistIdAsync(int playlistId)
        {
            return await _context.PlaylistMashups
                .Include(pm => pm.Mashup)
                .Where(pm => pm.PlaylistId == playlistId)
                .ToListAsync();
        }

        public async Task<Playlist_Mashup?> AddAsync(Playlist_Mashup playlistMashup)
        {
            _context.PlaylistMashups.Add(playlistMashup);
            await _context.SaveChangesAsync();
            return playlistMashup;
        }

        public async Task<bool> DeleteAsync(int playlistId, int mashupId)
        {
            var playlistMashup = await GetByIdsAsync(playlistId, mashupId);
            if (playlistMashup == null) return false;

            _context.PlaylistMashups.Remove(playlistMashup);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

