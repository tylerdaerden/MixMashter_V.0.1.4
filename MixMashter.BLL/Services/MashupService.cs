using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MixMashter.BLL.Interfaces;
using MixMashter.DAL.Repositories.Interfaces;
using MixMashter.Models.Entities;

namespace MixMashter.BLL.Services
{
    public class MashupService : IMashupService
    {
        private readonly IMashupRepository _mashupRepository;

        public MashupService(IMashupRepository mashupRepository)
        {
            _mashupRepository = mashupRepository;
        }

        public async Task<Mashup?> GetByIdAsync(int id)
        {
            return await _mashupRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Mashup>> GetAllAsync()
        {
            return await _mashupRepository.GetAllAsync();
        }

        public async Task<Mashup?> CreateAsync(Mashup mashup)
        {
            return await _mashupRepository.AddAsync(mashup);
        }

        public async Task<bool> UpdateAsync(Mashup mashup)
        {
            return await _mashupRepository.UpdateAsync(mashup);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _mashupRepository.DeleteAsync(id);
        }

        // Exemple de logique métier propre au Mashup (optionnel , piste de dév pour plus tard , je la garde pour plus tard , pê le TFE ? )
        public async Task<int> CalculateLengthAsync(int mashupId)
        {
            var mashup = await _mashupRepository.GetByIdWithSongsAsync(mashupId);
            if (mashup == null) return 0;

            // On additionne la durée de toutes les chansons du mashup
            return mashup.MashupSongs.Sum(ms => ms.Song.Length);
        }
    }
}