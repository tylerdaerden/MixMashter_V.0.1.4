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
    public class FavoritesRepository : IFavoritesRepository
    {
        private readonly MixMashterDbContext _context;

        public FavoritesRepository(MixMashterDbContext context)
        {
            _context = context;
        }

        public async Task<Favorites?> GetByIdsAsync(int userId, int mashupId)
        {
            return await _context.Favorites
                .Include(f => f.Mashup)
                .FirstOrDefaultAsync(f => f.UserId == userId && f.MashupId == mashupId);
        }

        public async Task<IEnumerable<Favorites>> GetByUserIdAsync(int userId)
        {
            return await _context.Favorites
                .Include(f => f.Mashup)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        public async Task<Favorites?> AddAsync(Favorites favorite)
        {
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();
            return favorite;
        }

        public async Task<bool> DeleteAsync(int userId, int mashupId)
        {
            var favorite = await GetByIdsAsync(userId, mashupId);
            if (favorite == null) return false;

            _context.Favorites.Remove(favorite);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

