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
        Task<IEnumerable<Artist>> GetAllAsync();
        Task<Artist?> GetByIdAsync(int id);
        Task<Artist> AddAsync(Artist artist);
        Task<bool> UpdateAsync(Artist artist);
        Task<bool> DeleteAsync(int id);

        // Validations métier pour le service Artist et aussi gérer mes futures entrées USer dans le front
        bool IsValidArtistName(string name);
        bool IsValidBand(string? band);
        bool IsValidWebsiteLink(string? websiteLink);
    }
}


