using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MixMashter.Models.Entities;

namespace MixMashter.BLL.Interfaces
{
    public interface IMashupService
    {
        // CRUD de base
        Task<Mashup?> GetByIdAsync(int id);
        Task<IEnumerable<Mashup>> GetAllAsync();
        Task<Mashup?> CreateAsync(Mashup mashup);
        Task<bool> UpdateAsync(Mashup mashup);
        Task<bool> DeleteAsync(int id);

        // Méthodes métier spécifiques
        Task<int> CalculateLengthAsync(int mashupId);

        //validations
        public bool IsValidFormat(string? format);
        public bool IsValidUrlLink(string? url);
        public bool IsValidCoverImage(string? url);

    }
}