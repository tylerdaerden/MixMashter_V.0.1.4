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
    public class SongRepository : ISongRepository
    {
        private readonly MixMashterDbContext _context;

        public SongRepository(MixMashterDbContext context)
        {
            _context = context;
        }

        public async Task<Song?> GetByIdAsync(int id)
        {
            return await _context.Songs
                .Include(s => s.Artist)
                .FirstOrDefaultAsync(s => s.SongId == id);
        }

        public async Task<IEnumerable<Song>> GetAllAsync()
        {
            return await _context.Songs
                .Include(s => s.Artist)
                .ToListAsync();
        }

        public async Task<Song?> AddAsync(Song song)
        {
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();
            return song;
        }

        public async Task<bool> UpdateAsync(Song song)
        {
            _context.Songs.Update(song);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null) return false;

            _context.Songs.Remove(song);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
