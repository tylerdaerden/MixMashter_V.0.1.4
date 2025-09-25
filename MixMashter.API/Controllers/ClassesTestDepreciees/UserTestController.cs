//using Microsoft.AspNetCore.JsonPatch;
//using Microsoft.AspNetCore.Mvc;
//using MixMashter.BLL.Interfaces;
//using MixMashter.Models.Entities;

//namespace MixMashter.API.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class UserTestController : ControllerBase
//    {
//        private readonly IUserService _userService;

//        public UserTestController(IUserService userService)
//        {
//            _userService = userService;
//        }

//        // GET: api/UserTest
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<User>>> GetAll()
//        {
//            var users = await _userService.GetAllAsync();
//            return Ok(users);
//        }

//        // GET: api/UserTest/id
//        [HttpGet("{id}")]
//        public async Task<ActionResult<User>> GetById(int id)
//        {
//            var user = await _userService.GetByIdAsync(id);
//            if (user == null)
//                return NotFound();

//            return Ok(user);
//        }

//        // POST: api/UserTest
//        [HttpPost]
//        public async Task<ActionResult<User>> Create([FromBody] User user)
//        {
//            var created = await _userService.RegisterAsync(
//                user.Firstname,
//                user.Lastname,
//                user.Username,
//                user.Email,
//                user.PasswordHash,
//                user.Role
//            );

//            if (created == null)
//                return BadRequest("User already exists.");

//            return CreatedAtAction(nameof(GetById), new { id = created.UserId }, created);
//        }

//        // PUT: api/UserTest/id
//        [HttpPut("{id}")]
//        public async Task<IActionResult> Update(int id, [FromBody] User user)
//        {
//            if (id != user.UserId)
//                return BadRequest("ID mismatch");

//            var success = await _userService.UpdateAsync(user);
//            if (!success)
//                return NotFound();

//            return NoContent();
//        }

//        // DELETE: api/UserTest/id
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var success = await _userService.DeleteAsync(id);
//            if (!success)
//                return NotFound();

//            return NoContent();
//        }

//        // PATCH: api/UserTest/id
//        [HttpPatch("{id}")]
//        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<User> patchDoc)
//        {
//            if (patchDoc == null)
//                return BadRequest();

//            var user = await _userService.GetByIdAsync(id);
//            if (user == null)
//                return NotFound();

//            patchDoc.ApplyTo(user, ModelState);
//            TryValidateModel(user);

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var success = await _userService.UpdateAsync(user);
//            if (!success)
//                return NotFound();

//            return Ok(user);
//        }
//    }
//}
