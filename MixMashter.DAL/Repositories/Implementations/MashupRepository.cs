using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MixMashter.DAL.Db;
using MixMashter.DAL.Repositories.Interfaces;
using MixMashter.Models.Entities;

namespace MixMashter.DAL.Repositories
{
    public class MashupRepository : IMashupRepository
    {
        private readonly MixMashterDbContext _context;

        public MashupRepository(MixMashterDbContext context)
        {
            _context = context;
        }

        public async Task<Mashup?> GetByIdAsync(int id)
        {
            return await _context.Mashups
                .FirstOrDefaultAsync(m => m.MashupId == id);
        }

        public async Task<IEnumerable<Mashup>> GetAllAsync()
        {
            return await _context.Mashups.ToListAsync();
        }

        public async Task<Mashup?> AddAsync(Mashup mashup)
        {
            _context.Mashups.Add(mashup);
            await _context.SaveChangesAsync();
            return mashup;
        }

        public async Task<bool> UpdateAsync(Mashup mashup)
        {
            _context.Mashups.Update(mashup);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var mashup = await _context.Mashups.FindAsync(id);
            if (mashup == null) return false;

            _context.Mashups.Remove(mashup);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Mashup?> GetByIdWithSongsAsync(int id)
        {
            return await _context.Mashups
                .Include(m => m.MashupSongs)
                    .ThenInclude(ms => ms.Song)
                .FirstOrDefaultAsync(m => m.MashupId == id);
        }
    }
}
