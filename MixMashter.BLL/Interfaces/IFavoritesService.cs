using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixMashter.Models.Entities;


namespace MixMashter.BLL.Interfaces
{
    public interface IFavoritesService
    {
        Task<Favorites?> GetByIdsAsync(int userId, int mashupId);
        Task<IEnumerable<Favorites>> GetByUserIdAsync(int userId);
        Task<Favorites?> AddAsync(int userId, int mashupId);
        Task<bool> RemoveAsync(int userId, int mashupId);
    }
}

