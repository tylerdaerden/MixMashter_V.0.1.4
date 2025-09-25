using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MixMashter.BLL.Interfaces;
using MixMashter.Models.DTO;
using MixMashter.Models.Entities;


namespace MixMashter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;
        private readonly IArtistService _artistService;

        public SongController(ISongService songService, IArtistService artistService)
        {
            _songService = songService;
            _artistService = artistService;
        }

        // GET: api/Song
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongReadDto>>> GetAll()
        {
            var songs = await _songService.GetAllAsync();

            var result = songs.Select(s => new SongReadDto
            {
                SongId = s.SongId,
                ArtistId = s.ArtistId,
                Title = s.Title,
                Length = s.Length,
                Genre = s.Genre,
                YearRelease = s.YearRelease,
                IsExplicit = s.IsExplicit,
                ArtistName = s.Artist?.ArtistName ?? string.Empty
            });

            return Ok(result);
        }

        // GET: api/Song/id
        [HttpGet("{id}")]
        public async Task<ActionResult<SongReadDto>> GetById(int id)
        {
            var song = await _songService.GetByIdAsync(id);
            if (song == null) return NotFound();

            var result = new SongReadDto
            {
                SongId = song.SongId,
                ArtistId = song.ArtistId,
                Title = song.Title,
                Length = song.Length,
                Genre = song.Genre,
                YearRelease = song.YearRelease,
                IsExplicit = song.IsExplicit,
                ArtistName = song.Artist?.ArtistName ?? string.Empty
            };

            return Ok(result);
        }

        // POST: api/Song
        [HttpPost]
        public async Task<ActionResult<SongReadDto>> Create(SongCreateDto dto)
        {
            // Vérifie si l’artiste existe avant de créer la chanson
            var artist = await _artistService.GetByIdAsync(dto.ArtistId);
            if (artist == null) return BadRequest("Artiste non trouvé");

            var song = new Song
            {
                ArtistId = dto.ArtistId,
                Title = dto.Title,
                Length = dto.Length,
                Genre = dto.Genre,
                YearRelease = dto.YearRelease,
                IsExplicit = dto.IsExplicit
            };

            var created = await _songService.CreateAsync(song);

            var result = new SongReadDto
            {
                SongId = created.SongId,
                ArtistId = created.ArtistId,
                Title = created.Title,
                Length = created.Length,
                Genre = created.Genre,
                YearRelease = created.YearRelease,
                IsExplicit = created.IsExplicit,
                ArtistName = artist.ArtistName
            };

            return CreatedAtAction(nameof(GetById), new { id = result.SongId }, result);
        }

        // PUT: api/Song/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SongUpdateDto dto)
        {
            if (id != dto.SongId) return BadRequest("ID Incorrect");

            var existing = await _songService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.ArtistId = dto.ArtistId;
            existing.Title = dto.Title;
            existing.Length = dto.Length;
            existing.Genre = dto.Genre;
            existing.YearRelease = dto.YearRelease;
            existing.IsExplicit = dto.IsExplicit;

            var success = await _songService.UpdateAsync(existing);
            if (!success) return NotFound();

            return NoContent();
        }

        // DELETE: api/Song/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _songService.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }

        //Comme pour toutes les autres controllers , je laisse cette méthode PATCH pour une meilleure expérience Utilisateur plus tard (qu'une modification partielle puissa voir lieu)
        // PATCH: api/Song/id
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SongPatchDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var song = await _songService.GetByIdAsync(id);
            if (song == null) return NotFound();

            // On projette l'entité Song vers un DTO partiel
            var dto = new SongPatchDto
            {
                ArtistId = song.ArtistId,
                Title = song.Title,
                Length = song.Length,
                Genre = song.Genre,
                YearRelease = song.YearRelease,
                IsExplicit = song.IsExplicit
            };

            // Application du patch sur le DTO
            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Validation supplémentaire : si l’ArtistId change → vérifier que l’artiste existe
            if (dto.ArtistId.HasValue && dto.ArtistId.Value != song.ArtistId)
            {
                var artist = await _artistService.GetByIdAsync(dto.ArtistId.Value);
                if (artist == null) return BadRequest("Artiste non trouvé");
            }

            // Projection inverse : mise à jour de l’entité depuis le DTO
            if (dto.ArtistId.HasValue) song.ArtistId = dto.ArtistId.Value;
            if (!string.IsNullOrEmpty(dto.Title)) song.Title = dto.Title;
            if (dto.Length.HasValue) song.Length = dto.Length.Value;
            if (!string.IsNullOrEmpty(dto.Genre)) song.Genre = dto.Genre;
            if (dto.YearRelease.HasValue) song.YearRelease = dto.YearRelease.Value;
            if (dto.IsExplicit.HasValue) song.IsExplicit = dto.IsExplicit.Value;

            var ok = await _songService.UpdateAsync(song);
            if (!ok) return NotFound();

            // Retourner un ReadDto
            var artistName = (await _artistService.GetByIdAsync(song.ArtistId))?.ArtistName ?? string.Empty;
            var result = new SongReadDto
            {
                SongId = song.SongId,
                ArtistId = song.ArtistId,
                Title = song.Title,
                Length = song.Length,
                Genre = song.Genre,
                YearRelease = song.YearRelease,
                IsExplicit = song.IsExplicit,
                ArtistName = artistName
            };

            return Ok(result);
        }



    }
}


