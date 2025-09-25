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
    public class ArtistRepository : IArtistRepository
    {
        private readonly MixMashterDbContext _context;

        public ArtistRepository(MixMashterDbContext context)
        {
            _context = context;
        }

        public async Task<Artist?> GetByIdAsync(int id)
        {
            return await _context.Artists.FindAsync(id);
        }

        public async Task<IEnumerable<Artist>> GetAllAsync()
        {
            return await _context.Artists.ToListAsync();
        }

        public async Task<Artist?> AddAsync(Artist artist)
        {
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();
            return artist;
        }

        public async Task<bool> UpdateAsync(Artist artist)
        {
            _context.Artists.Update(artist);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null) return false;

            _context.Artists.Remove(artist);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
