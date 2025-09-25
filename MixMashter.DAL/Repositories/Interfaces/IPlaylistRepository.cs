using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixMashter.Models.Entities;

namespace MixMashter.DAL.Repositories.Interfaces
{
    public interface IPlaylistRepository
    {
        Task<Playlist?> GetByIdAsync(int id);
        Task<IEnumerable<Playlist>> GetAllAsync();
        Task<Playlist?> AddAsync(Playlist playlist);
        Task<bool> UpdateAsync(Playlist playlist);
        Task<bool> DeleteAsync(int id);

        // même logique de test futur que mes autres repository , test futur ou bonus chokotoff
        Task<Playlist?> GetByIdWithMashupsAsync(int id);
    }
}
