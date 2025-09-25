//using Microsoft.AspNetCore.JsonPatch;
//using Microsoft.AspNetCore.Mvc;
//using MixMashter.BLL.Interfaces;
//using MixMashter.Models.Entities;

////Classe temporaire , laissée pour pédagogie ,comme pour mon DB Test Controller , de test de l'ArtistService 
////pour vérifier que tout est ok avant d'attaquer le front et l'API plus en profondeur

//namespace MixMashter.API.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class ArtistTestController : ControllerBase
//    {
//        private readonly IArtistService _artistService;

//        public ArtistTestController(IArtistService artistService)
//        {
//            _artistService = artistService;
//        }

//        // GET: api/Artist
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Artist>>> GetAll()
//        {
//            var artists = await _artistService.GetAllAsync();
//            return Ok(artists);
//        }

//        // GET: api/Artist/id
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Artist>> GetById(int id)
//        {
//            var artist = await _artistService.GetByIdAsync(id);
//            if (artist == null)
//                return NotFound();

//            return Ok(artist);
//        }

//        // POST: api/Artist
//        [HttpPost]
//        public async Task<ActionResult<Artist>> Create([FromBody] Artist artist)
//        {
//            var created = await _artistService.CreateAsync(artist);
//            return CreatedAtAction(nameof(GetById), new { id = created.ArtistId }, created);
//        }

//        // PUT: api/Artist/id
//        [HttpPut("{id}")]
//        public async Task<IActionResult> Update(int id, [FromBody] Artist artist)
//        {
//            if (id != artist.ArtistId)
//                return BadRequest("ID mismatch");

//            var success = await _artistService.UpdateAsync(artist);
//            if (!success)
//                return NotFound();

//            return NoContent();
//        }

//        // DELETE: api/Artist/id
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var success = await _artistService.DeleteAsync(id);
//            if (!success)
//                return NotFound();

//            return NoContent();
//        }

//        // PATCH: api/Artist/id
//        [HttpPatch("{id}")]
//        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Artist> patchDoc)
//        {
//            if (patchDoc == null)
//                return BadRequest();

//            var artist = await _artistService.GetByIdAsync(id);
//            if (artist == null)
//                return NotFound();

//            patchDoc.ApplyTo(artist, ModelState);

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var updated = await _artistService.UpdateAsync(artist);
//            if (!updated)
//                return NotFound();

//            return Ok(artist);
//        }
//    }
//}
