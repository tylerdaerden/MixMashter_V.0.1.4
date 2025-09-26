using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MixMashter.BLL.Interfaces;
using MixMashter.Models.DTOs;
using MixMashter.Models.Entities;

namespace MixMashter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MashupController : ControllerBase
    {
        private readonly IMashupService _mashupService;
        private readonly IUserService _userService;

        public MashupController(IMashupService mashupService, IUserService userService)
        {
            _mashupService = mashupService;
            _userService = userService;
        }

        // GET all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MashupReadDto>>> GetAll()
        {
            var mashups = await _mashupService.GetAllAsync();
            var result = mashups.Select(m => new MashupReadDto
            {
                MashupId = m.MashupId,
                UserId = m.UserId,
                Title = m.Title,
                Length = m.Length,
                IsExplicit = m.IsExplicit,
                Description = m.Description,
                Format = m.Format,
                UrlLink = m.UrlLink,
                CoverImage = m.CoverImage,
                Username = m.User?.Username
            });
            return Ok(result);
        }

        // GET by ID: api/Mashup/id
        [HttpGet("{id}")]
        public async Task<ActionResult<MashupReadDto>> GetById(int id)
        {
            var mashup = await _mashupService.GetByIdAsync(id);
            if (mashup == null) return NotFound();

            var result = new MashupReadDto
            {
                MashupId = mashup.MashupId,
                UserId = mashup.UserId,
                Title = mashup.Title,
                Length = mashup.Length,
                IsExplicit = mashup.IsExplicit,
                Description = mashup.Description,
                Format = mashup.Format,
                UrlLink = mashup.UrlLink,
                CoverImage = mashup.CoverImage,
                Username = mashup.User?.Username
            };
            return Ok(result);
        }

        // POST create: api/Mashup/id
        [HttpPost]
        public async Task<ActionResult<MashupReadDto>> Create([FromBody] MashupCreateDto dto)
        {
            var user = await _userService.GetByIdAsync(dto.UserId);
            if (user == null) return BadRequest("User not found");

            // les validations
            if (!_mashupService.IsValidFormat(dto.Format)) return BadRequest("Invalid format (allowed: mp3, wav, flac)");
            if (!_mashupService.IsValidUrlLink(dto.UrlLink)) return BadRequest("Invalid URL link");
            if (!_mashupService.IsValidCoverImage(dto.CoverImage)) return BadRequest("Invalid cover image URL (must be .jpg/.jpeg/.png)");

            var mashup = new Mashup
            {
                UserId = dto.UserId,
                Title = dto.Title,
                Length = dto.Length,
                IsExplicit = dto.IsExplicit,
                Description = dto.Description,
                Format = dto.Format,
                UrlLink = dto.UrlLink,
                CoverImage = dto.CoverImage
            };

            var created = await _mashupService.CreateAsync(mashup);

            var result = new MashupReadDto
            {
                MashupId = created.MashupId,
                UserId = created.UserId,
                Title = created.Title,
                Length = created.Length,
                IsExplicit = created.IsExplicit,
                Description = created.Description,
                Format = created.Format,
                UrlLink = created.UrlLink,
                CoverImage = created.CoverImage,
                Username = user.Username
            };

            return CreatedAtAction(nameof(GetById), new { id = created.MashupId }, result);
        }


        // PUT update total: api/Mashup/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MashupUpdateDto dto)
        {
            if (id != dto.MashupId) return BadRequest("ID mismatch");

            var user = await _userService.GetByIdAsync(dto.UserId);
            if (user == null) return BadRequest("User not found");

            // les validations
            if (!_mashupService.IsValidFormat(dto.Format)) return BadRequest("Invalid format (allowed: mp3, wav, flac)");
            if (!_mashupService.IsValidUrlLink(dto.UrlLink)) return BadRequest("Invalid URL link");
            if (!_mashupService.IsValidCoverImage(dto.CoverImage)) return BadRequest("Invalid cover image URL (must be .jpg/.jpeg/.png)");

            var mashup = new Mashup
            {
                MashupId = dto.MashupId,
                UserId = dto.UserId,
                Title = dto.Title,
                Length = dto.Length,
                IsExplicit = dto.IsExplicit,
                Description = dto.Description,
                Format = dto.Format,
                UrlLink = dto.UrlLink,
                CoverImage = dto.CoverImage
            };

            var ok = await _mashupService.UpdateAsync(mashup);
            if (!ok) return NotFound();

            return NoContent();
        }


        // PATCH: api/Mashup/id
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<MashupPatchDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var mashup = await _mashupService.GetByIdAsync(id);
            if (mashup == null) return NotFound();

            var patchDto = new MashupPatchDto
            {
                Title = mashup.Title,
                Length = mashup.Length,
                IsExplicit = mashup.IsExplicit,
                Description = mashup.Description,
                Format = mashup.Format,
                UrlLink = mashup.UrlLink,
                CoverImage = mashup.CoverImage
            };

            patchDoc.ApplyTo(patchDto, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // les validations uniquement si modifié
            if (!string.IsNullOrEmpty(patchDto.Format) && !_mashupService.IsValidFormat(patchDto.Format))
                return BadRequest("Invalid format");
            if (!string.IsNullOrEmpty(patchDto.UrlLink) && !_mashupService.IsValidUrlLink(patchDto.UrlLink))
                return BadRequest("Invalid URL link");
            if (!string.IsNullOrEmpty(patchDto.CoverImage) && !_mashupService.IsValidCoverImage(patchDto.CoverImage))
                return BadRequest("Invalid cover image");

            mashup.Title = patchDto.Title ?? mashup.Title;
            mashup.Length = patchDto.Length ?? mashup.Length;
            mashup.IsExplicit = patchDto.IsExplicit ?? mashup.IsExplicit;
            mashup.Description = patchDto.Description ?? mashup.Description;
            mashup.Format = patchDto.Format ?? mashup.Format;
            mashup.UrlLink = patchDto.UrlLink ?? mashup.UrlLink;
            mashup.CoverImage = patchDto.CoverImage ?? mashup.CoverImage;

            var ok = await _mashupService.UpdateAsync(mashup);
            if (!ok) return NotFound();

            // retour vers le Readto de mes gentils Dtos
            return Ok(new MashupReadDto
            {
                MashupId = mashup.MashupId,
                UserId = mashup.UserId,
                Title = mashup.Title,
                Length = mashup.Length,
                IsExplicit = mashup.IsExplicit,
                Description = mashup.Description,
                Format = mashup.Format,
                UrlLink = mashup.UrlLink,
                CoverImage = mashup.CoverImage,
                Username = mashup.User?.Username
            });
        }



        // DELETE: api/Mashup/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _mashupService.DeleteAsync(id);
            if (!ok) return NotFound();

            return NoContent();
        }
    }
}
