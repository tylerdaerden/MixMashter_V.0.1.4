using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MixMashter.BLL.Interfaces;
using MixMashter.Models.DTO;
using MixMashter.Models.Enums;
using System.Security.Claims;

namespace MixMashter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // toutes les routes nécessitent un JWT
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User (Admin only)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAll()
        {
            var users = await _userService.GetAllAsync();

            var result = users.Select(u => new UserReadDto
            {
                UserId = u.UserId,
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                Username = u.Username,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            });

            return Ok(result);
        }

        // GET: api/User/me (profil courant)
        [HttpGet("me")]
        public async Task<ActionResult<UserReadDto>> Me()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var user = await _userService.GetByIdAsync(userId);
            if (user == null) return NotFound();

            var result = new UserReadDto
            {
                UserId = user.UserId,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };

            return Ok(result);
        }

        // PUT: api/User/update-profile
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserUpdateDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (dto.UserId != userId) return Forbid("Vous ne pouvez mettre à jour que votre propre profil");

            if (!_userService.IsValidName(dto.Firstname) || !_userService.IsValidName(dto.Lastname))
                return BadRequest("Nom ou prénom invalide"); // j'ai bien retenu la leçon de la validation de ne pas dire ce qui est précisement invalide pour des raisons de sécurité 🙂

            if (!_userService.IsValidEmail(dto.Email))
                return BadRequest("Email invalide");

            var user = await _userService.GetByIdAsync(userId);
            if (user == null) return NotFound();

            user.Firstname = dto.Firstname;
            user.Lastname = dto.Lastname;
            user.Username = dto.Username;
            user.Email = dto.Email;

            var success = await _userService.UpdateAsync(user);
            if (!success) return BadRequest("Mise à jour impossible");

            return NoContent();
        }


        // PUT: api/User/change-password
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            //  Validation du nouveau mot de passe
            if (!_userService.IsValidPassword(dto.NewPassword))
                return BadRequest("Le mot de passe doit contenir au moins 8 caractères, dont des majuscules, des minuscules, des chiffres et des caractères spéciaux.");

            var success = await _userService.ChangePasswordAsync(userId, dto.CurrentPassword, dto.NewPassword);
            if (!success) return BadRequest("Mot de passe actuel invalide");

            return NoContent();
        }


        // DELETE: api/User/delete-profile
        [HttpDelete("delete-profile")]
        public async Task<IActionResult> DeleteProfile()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var success = await _userService.DeleteProfileAsync(userId);

            return success ? NoContent() : NotFound();
        }

        // PUT: api/User/set-role/{id}
        [HttpPut("set-role/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetRole(int id, [FromBody] Role newRole)
        {
            var success = await _userService.SetRoleAsync(id, newRole);
            return success ? NoContent() : NotFound();
        }
    }
}
