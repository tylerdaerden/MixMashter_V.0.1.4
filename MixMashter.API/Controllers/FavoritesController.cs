using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MixMashter.BLL.Interfaces;
using MixMashter.Models.DTO;
using MixMashter.Models.DTOs;


namespace MixMashter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoritesService _favoritesService;
        private readonly IUserService _userService;
        private readonly IMashupService _mashupService;

        public FavoritesController(
            IFavoritesService favoritesService,
            IUserService userService,
            IMashupService mashupService)
        {
            _favoritesService = favoritesService;
            _userService = userService;
            _mashupService = mashupService;
        }

        // GET: api/Favorites/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<FavoriteReadDto>>> GetByUser(int userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            if (user == null) return NotFound("User not found");

            var favorites = await _favoritesService.GetByUserIdAsync(userId);

            var result = favorites.Select(f => new FavoriteReadDto
            {
                UserId = f.UserId,
                MashupId = f.MashupId,
                AddedDate = f.AddedDate,
                Mashup = new MashupReadDto
                {
                    MashupId = f.Mashup.MashupId,
                    UserId = f.Mashup.UserId,
                    Title = f.Mashup.Title,
                    Length = f.Mashup.Length,
                    IsExplicit = f.Mashup.IsExplicit,
                    Description = f.Mashup.Description,
                    Format = f.Mashup.Format,
                    UrlLink = f.Mashup.UrlLink,
                    CoverImage = f.Mashup.CoverImage,
                    Username = f.Mashup.User?.Username
                }
            });

            return Ok(result);
        }


        // POST: api/Favorites/{userId}/{mashupId}
        [HttpPost("{userId}/{mashupId}")]
        public async Task<IActionResult> Add(int userId, int mashupId)
        {
            var user = await _userService.GetByIdAsync(userId);
            if (user == null) return NotFound("Utilisateur introuvable");

            var mashup = await _mashupService.GetByIdAsync(mashupId);
            if (mashup == null) return NotFound("Mashup introuvable");

            var existing = await _favoritesService.GetByIdsAsync(userId, mashupId);
            if (existing != null) return BadRequest("Mashup déjà en favoris");

            var added = await _favoritesService.AddAsync(userId, mashupId);
            return added != null ? NoContent() : BadRequest("Erreur lors de l'ajout aux favoris");
        }

        // DELETE: api/Favorites/{userId}/{mashupId}
        [HttpDelete("{userId}/{mashupId}")]
        public async Task<IActionResult> Remove(int userId, int mashupId)
        {
            var ok = await _favoritesService.RemoveAsync(userId, mashupId);
            return ok ? NoContent() : NotFound();
        }


    }
}
