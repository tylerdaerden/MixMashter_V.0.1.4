using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MixMashter.BLL.Interfaces;
using MixMashter.Models.DTOs;
using MixMashter.Models.Entities;

namespace MixMashter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;
        private readonly IUserService _userService;
        private readonly IMashupService _mashupService;
        private readonly IPlaylistMashupService _playlistMashupService;

        public PlaylistController(
            IPlaylistService playlistService,
            IUserService userService,
            IMashupService mashupService,
            IPlaylistMashupService playlistMashupService)
        {
            _playlistService = playlistService;
            _userService = userService;
            _mashupService = mashupService;
            _playlistMashupService = playlistMashupService;
        }

        // Exemple: GET /api/Playlist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaylistReadDto>>> GetAll()
        {
            var playlists = await _playlistService.GetAllAsync();

            var result = playlists.Select(p => new PlaylistReadDto
            {
                PlaylistId = p.PlaylistId,
                UserId = p.UserId,
                Title = p.Title,
                DateCreated = p.DateCreated,
                Username = p.User?.Username,
                Mashups = p.PlaylistMashups.Select(pm => new MashupReadDto
                {
                    MashupId = pm.Mashup.MashupId,
                    Title = pm.Mashup.Title,
                    Length = pm.Mashup.Length,
                    IsExplicit = pm.Mashup.IsExplicit,
                    Description = pm.Mashup.Description,
                    Format = pm.Mashup.Format,
                    UrlLink = pm.Mashup.UrlLink,
                    CoverImage = pm.Mashup.CoverImage,
                    UserId = pm.Mashup.UserId
                }).ToList()
            });

            return Ok(result);
        }


        // GET by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaylistReadDto>> GetById(int id)
        {
            var playlist = await _playlistService.GetByIdAsync(id);
            if (playlist == null) return NotFound();

            var result = new PlaylistReadDto
            {
                PlaylistId = playlist.PlaylistId,
                UserId = playlist.UserId,
                Title = playlist.Title,
                DateCreated = playlist.DateCreated,
                Username = playlist.User?.Username,
                Mashups = playlist.PlaylistMashups.Select(pm => new MashupReadDto
                {
                    MashupId = pm.Mashup.MashupId,
                    Title = pm.Mashup.Title,
                    Length = pm.Mashup.Length,
                    IsExplicit = pm.Mashup.IsExplicit,
                    Description = pm.Mashup.Description,
                    Format = pm.Mashup.Format,
                    UrlLink = pm.Mashup.UrlLink,
                    CoverImage = pm.Mashup.CoverImage,
                    UserId = pm.Mashup.UserId
                }).ToList()
            };

            return Ok(result);
        }

        // POST create
        [HttpPost]
        public async Task<ActionResult<PlaylistReadDto>> Create([FromBody] PlaylistCreateDto dto)
        {
            var user = await _userService.GetByIdAsync(dto.UserId);
            if (user == null) return BadRequest("User not found");

            var playlist = new Playlist
            {
                UserId = dto.UserId,
                Title = dto.Title,
                DateCreated = DateTime.UtcNow
            };

            var created = await _playlistService.CreateAsync(playlist);

            var result = new PlaylistReadDto
            {
                PlaylistId = created.PlaylistId,
                UserId = created.UserId,
                Title = created.Title,
                DateCreated = created.DateCreated,
                Username = user.Username,
                Mashups = new List<MashupReadDto>() //vide à création , logique
            };

            return CreatedAtAction(nameof(GetById), new { id = created.PlaylistId }, result);
        }

        // PATCH update partiel
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<PlaylistPatchDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var playlist = await _playlistService.GetByIdAsync(id);
            if (playlist == null) return NotFound();

            var dto = new PlaylistPatchDto { Title = playlist.Title };

            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!string.IsNullOrEmpty(dto.Title))
                playlist.Title = dto.Title;

            var ok = await _playlistService.UpdateAsync(playlist);
            if (!ok) return NotFound();

            var result = new PlaylistReadDto
            {
                PlaylistId = playlist.PlaylistId,
                UserId = playlist.UserId,
                Title = playlist.Title,
                DateCreated = playlist.DateCreated,
                Username = playlist.User?.Username,
                Mashups = playlist.PlaylistMashups.Select(pm => new MashupReadDto
                {
                    MashupId = pm.Mashup.MashupId,
                    Title = pm.Mashup.Title,
                    Length = pm.Mashup.Length,
                    IsExplicit = pm.Mashup.IsExplicit,
                    Description = pm.Mashup.Description,
                    Format = pm.Mashup.Format,
                    UrlLink = pm.Mashup.UrlLink,
                    CoverImage = pm.Mashup.CoverImage,
                    UserId = pm.Mashup.UserId
                }).ToList()
            };

            return Ok(result);
        }

        // DELETE playlist
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _playlistService.DeleteAsync(id);
            if (!ok) return NotFound();

            return NoContent();
        }

        // Ajouter un mashup à une playlist
        [HttpPost("{id}/mashups/{mashupId}")]
        public async Task<IActionResult> AddMashup(int id, int mashupId)
        {
            var playlist = await _playlistService.GetByIdAsync(id);
            if (playlist == null) return NotFound("Playlist not found");

            var mashup = await _mashupService.GetByIdAsync(mashupId);
            if (mashup == null) return NotFound("Mashup not found");

            var existing = await _playlistMashupService.GetByIdsAsync(id, mashupId);
            if (existing != null) return BadRequest("Mashup already in playlist");

            var added = await _playlistMashupService.AddAsync(id, mashupId);
            return added != null ? NoContent() : BadRequest("Failed to add mashup");
        }

        // Retirer un mashup d’une playlist
        [HttpDelete("{id}/mashups/{mashupId}")]
        public async Task<IActionResult> RemoveMashup(int id, int mashupId)
        {
            var ok = await _playlistMashupService.RemoveAsync(id, mashupId);
            return ok ? NoContent() : NotFound();
        }

        // Lister les mashups d’une playlist
        [HttpGet("{id}/mashups")]
        public async Task<ActionResult<IEnumerable<MashupReadDto>>> GetMashups(int id)
        {
            var playlist = await _playlistService.GetByIdAsync(id);
            if (playlist == null) return NotFound("Playlist not found");

            var mashups = await _playlistMashupService.GetByPlaylistIdAsync(id);
            var result = mashups.Select(pm => new MashupReadDto
            {
                MashupId = pm.Mashup.MashupId,
                Title = pm.Mashup.Title,
                Length = pm.Mashup.Length,
                IsExplicit = pm.Mashup.IsExplicit,
                Description = pm.Mashup.Description,
                Format = pm.Mashup.Format,
                UrlLink = pm.Mashup.UrlLink,
                CoverImage = pm.Mashup.CoverImage,
                UserId = pm.Mashup.UserId
            });

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PlaylistUpdateDto dto)
        {
            if (id != dto.PlaylistId) return BadRequest("ID mismatch");

            var user = await _userService.GetByIdAsync(dto.UserId);
            if (user == null) return BadRequest("User not found");

            var playlist = new Playlist
            {
                PlaylistId = dto.PlaylistId,
                UserId = dto.UserId,
                Title = dto.Title,
                DateCreated = DateTime.UtcNow
            };

            var ok = await _playlistService.UpdateAsync(playlist);
            if (!ok) return NotFound();
                        
            var result = new PlaylistReadDto
            {
                PlaylistId = playlist.PlaylistId,
                UserId = playlist.UserId,
                Title = playlist.Title,
                DateCreated = playlist.DateCreated,
                Username = user.Username,
                Mashups = playlist.PlaylistMashups.Select(pm => new MashupReadDto
                {
                    MashupId = pm.Mashup.MashupId,
                    Title = pm.Mashup.Title,
                    Length = pm.Mashup.Length,
                    IsExplicit = pm.Mashup.IsExplicit,
                    Description = pm.Mashup.Description,
                    Format = pm.Mashup.Format,
                    UrlLink = pm.Mashup.UrlLink,
                    CoverImage = pm.Mashup.CoverImage,
                    UserId = pm.Mashup.UserId
                }).ToList()
            };

            return Ok(result);
        }
    }
}
