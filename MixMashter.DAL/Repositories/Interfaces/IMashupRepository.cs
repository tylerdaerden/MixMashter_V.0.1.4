using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MixMashter.Models.Entities;

namespace MixMashter.DAL.Repositories.Interfaces
{
    public interface IMashupRepository
    {
        Task<Mashup?> GetByIdAsync(int id);
        Task<IEnumerable<Mashup>> GetAllAsync();
        Task<Mashup?> AddAsync(Mashup mashup);
        Task<bool> UpdateAsync(Mashup mashup);
        Task<bool> DeleteAsync(int id);

        // Ajout en cours de développement , méthode pour récupérer un mashup avec ses chansons 
        // Optionnelle à ce stade du projet, donc potentiellement pas implémentée partout, mais bonne piste pour dev futur
        Task<Mashup?> GetByIdWithSongsAsync(int id);
    }
}
