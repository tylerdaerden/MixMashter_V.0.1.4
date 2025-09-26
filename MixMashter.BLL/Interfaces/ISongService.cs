using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixMashter.Models.Entities;

namespace MixMashter.BLL.Interfaces
{
    public interface ISongService
    {
        Task<Song?> GetByIdAsync(int id);
        Task<IEnumerable<Song>> GetAllAsync();
        Task<Song?> CreateAsync(Song song);
        Task<bool> UpdateAsync(Song song);
        Task<bool> DeleteAsync(int id);

        // méthodes optionnelle pas dans mon dossier , mais je la garde pour plus tard sur suggestion d'un collègue .
        Task<IEnumerable<Song>> GetExplicitSongsAsync();

        //méthodes de validation
        public bool IsValidTitle(string title);

        public bool IsValidGenre(string genre);
        
    }
}

