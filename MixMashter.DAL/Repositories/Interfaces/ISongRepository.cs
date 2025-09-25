using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixMashter.Models.Entities;

namespace MixMashter.DAL.Repositories.Interfaces
{
    public interface ISongRepository
    {
        Task<Song?> GetByIdAsync(int id);
        Task<IEnumerable<Song>> GetAllAsync();
        Task<Song?> AddAsync(Song song);
        Task<bool> UpdateAsync(Song song);
        Task<bool> DeleteAsync(int id);
    }
}
