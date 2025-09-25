using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixMashter.Models.Entities;


namespace MixMashter.DAL.Repositories.Interfaces
{
    public interface IFavoritesRepository
    {
        Task<Favorites?> GetByIdsAsync(int userId, int mashupId);
        Task<IEnumerable<Favorites>> GetByUserIdAsync(int userId);
        Task<Favorites?> AddAsync(Favorites favorite);
        Task<bool> DeleteAsync(int userId, int mashupId);
    }
}

