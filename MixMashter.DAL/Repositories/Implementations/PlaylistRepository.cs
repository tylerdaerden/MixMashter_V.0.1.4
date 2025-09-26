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
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly MixMashterDbContext _context;

        public PlaylistRepository(MixMashterDbContext context)
        {
            _context = context;
        }

        public async Task<Playlist?> GetByIdAsync(int id)
        {
            return await _context.Playlists
                .Include(p => p.User)
                .Include(p => p.PlaylistMashups)
                    .ThenInclude(pm => pm.Mashup)
                .FirstOrDefaultAsync(p => p.PlaylistId == id);
        }


        public async Task<IEnumerable<Playlist>> GetAllAsync()
        {
            return await _context.Playlists
                .Include(p => p.User)
                .Include(p => p.PlaylistMashups)
                    .ThenInclude(pm => pm.Mashup)
                .ToListAsync();
        }


        public async Task<Playlist?> AddAsync(Playlist playlist)
        {
            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();
            return playlist;
        }

        public async Task<bool> UpdateAsync(Playlist playlist)
        {
            _context.Playlists.Update(playlist);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null) return false;

            _context.Playlists.Remove(playlist);
            return await _context.SaveChangesAsync() > 0;
        }

        //fonction test pour bonus pê .
        public async Task<Playlist?> GetByIdWithMashupsAsync(int id)
        {
            return await _context.Playlists
                .Include(p => p.PlaylistMashups)
                    .ThenInclude(pm => pm.Mashup)
                .FirstOrDefaultAsync(p => p.PlaylistId == id);
        }
    }
}
