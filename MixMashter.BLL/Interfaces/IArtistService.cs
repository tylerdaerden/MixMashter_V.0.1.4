using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixMashter.Models.Entities;


namespace MixMashter.BLL.Interfaces
{
    public interface IArtistService
    {
        Task<Artist?> GetByIdAsync(int id);
        Task<IEnumerable<Artist>> GetAllAsync();
        Task<Artist?> CreateAsync(Artist artist);
        Task<bool> UpdateAsync(Artist artist);
        Task<bool> DeleteAsync(int id);
    }
}

