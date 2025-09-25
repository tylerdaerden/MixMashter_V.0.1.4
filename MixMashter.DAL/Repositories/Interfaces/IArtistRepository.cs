using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixMashter.Models.Entities;


namespace MixMashter.DAL.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        Task<Artist?> GetByIdAsync(int id);
        Task<IEnumerable<Artist>> GetAllAsync();
        Task<Artist?> AddAsync(Artist artist);
        Task<bool> UpdateAsync(Artist artist);
        Task<bool> DeleteAsync(int id);
    }
}
