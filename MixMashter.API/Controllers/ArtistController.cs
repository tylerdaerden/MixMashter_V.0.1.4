using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MixMashter.BLL.Interfaces;
using MixMashter.Models.DTOs;
using MixMashter.Models.Entities;

namespace MixMashter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        // GET all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistReadDto>>> GetAll()
        {
            var artists = await _artistService.GetAllAsync();
            return Ok(artists.Select(a => new ArtistReadDto
            {
                ArtistId = a.ArtistId,
                ArtistName = a.ArtistName,
                Band = a.Band,
                WebsiteLink = a.WebsiteLink
            }));
        }

        // GET by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistReadDto>> GetById(int id)
        {
            var artist = await _artistService.GetByIdAsync(id);
            if (artist == null) return NotFound();

            return Ok(new ArtistReadDto
            {
                ArtistId = artist.ArtistId,
                ArtistName = artist.ArtistName,
                Band = artist.Band,
                WebsiteLink = artist.WebsiteLink
            });
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<ArtistReadDto>> Create([FromBody] ArtistCreateDto dto)
        {
            var artist = new Artist
            {
                ArtistName = dto.ArtistName,
                Band = dto.Band,
                WebsiteLink = dto.WebsiteLink
            };

            var created = await _artistService.AddAsync(artist);

            var result = new ArtistReadDto
            {
                ArtistId = created.ArtistId,
                ArtistName = created.ArtistName,
                Band = created.Band,
                WebsiteLink = created.WebsiteLink
            };

            return CreatedAtAction(nameof(GetById), new { id = created.ArtistId }, result);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ArtistUpdateDto dto)
        {
            if (id != dto.ArtistId) return BadRequest("ID mismatch");

            var artist = new Artist
            {
                ArtistId = dto.ArtistId,
                ArtistName = dto.ArtistName,
                Band = dto.Band,
                WebsiteLink = dto.WebsiteLink
            };

            var success = await _artistService.UpdateAsync(artist);
            if (!success) return BadRequest("Validation failed or artist not found");

            return NoContent();
        }

        // PATCH
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<ArtistPatchDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var artist = await _artistService.GetByIdAsync(id);
            if (artist == null) return NotFound();

            var dto = new ArtistPatchDto
            {
                ArtistName = artist.ArtistName,
                Band = artist.Band,
                WebsiteLink = artist.WebsiteLink
            };

            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            artist.ArtistName = dto.ArtistName ?? artist.ArtistName;
            artist.Band = dto.Band ?? artist.Band;
            artist.WebsiteLink = dto.WebsiteLink ?? artist.WebsiteLink;

            var success = await _artistService.UpdateAsync(artist);
            if (!success) return BadRequest("Validation failed");

            return Ok(new ArtistReadDto
            {
                ArtistId = artist.ArtistId,
                ArtistName = artist.ArtistName,
                Band = artist.Band,
                WebsiteLink = artist.WebsiteLink
            });
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _artistService.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
