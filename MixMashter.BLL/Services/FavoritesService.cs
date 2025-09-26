using MixMashter.BLL.Interfaces;
using MixMashter.DAL.Repositories.Interfaces;
using MixMashter.Models.Entities;

namespace MixMashter.BLL.Services
{
    public class FavoritesService : IFavoritesService
    {
        private readonly IFavoritesRepository _favoritesRepository;

        public FavoritesService(IFavoritesRepository favoritesRepository)
        {
            _favoritesRepository = favoritesRepository;
        }

        public async Task<Favorites?> GetByIdsAsync(int userId, int mashupId)
        {
            return await _favoritesRepository.GetByIdsAsync(userId, mashupId);
        }

        public async Task<IEnumerable<Favorites>> GetByUserIdAsync(int userId)
        {
            return await _favoritesRepository.GetByUserIdAsync(userId);
        }

        public async Task<Favorites?> AddAsync(int userId, int mashupId)
        {
            var favorite = new Favorites
            {
                UserId = userId,
                MashupId = mashupId,
                AddedDate = DateTime.UtcNow
            };
            return await _favoritesRepository.AddAsync(favorite);
        }

        public async Task<bool> RemoveAsync(int userId, int mashupId)
        {
            return await _favoritesRepository.DeleteAsync(userId, mashupId);
        }
    }
}
