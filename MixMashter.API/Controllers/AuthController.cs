using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MixMashter.BLL.Interfaces;
using MixMashter.Models.DTOs;
using MixMashter.Models.Entities;
using MixMashter.Models.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MixMashter.API.Controllers
{
    //mon controller pour l'authentification et l'autorisation (inscription et connexion) 


    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!_userService.IsValidName(dto.Firstname) || !_userService.IsValidName(dto.Lastname))
                return BadRequest("Nom ou prénom invalide");

            if (!_userService.IsValidEmail(dto.Email))
                return BadRequest("Email invalide");

            if (!_userService.IsValidPassword(dto.Password))
                return BadRequest("Mot de passe invalide");

            if (dto.Role != Role.User && dto.Role != Role.Masher)
            {
                return BadRequest("Rôle invalide. Choisissez User ou Masher.");
            }

            var user = await _userService.RegisterAsync(
                dto.Firstname,
                dto.Lastname,
                dto.Username,
                dto.Email,
                dto.Password,
                dto.Role
            );

            if (user == null) return BadRequest("Email déjà utilisé.");

            return Ok(new { user.UserId, user.Email, user.Username, user.Role });
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userService.LoginAsync(dto.Email, dto.Password);
            if (user == null) return Unauthorized("Identifiants invalides.");

            var token = GenerateJwtToken(user);

            return Ok(new { token, role = user.Role.ToString() });
        }

        /// <summary>
        /// Retourne les infos de l’utilisateur connecté (basées sur le JWT).
        /// https://learn.microsoft.com/en-us/aspnet/core/security/authentication/configure-jwt-bearer-authentication?view=aspnetcore-9.0
        /// commeça simple refresh et renvoi pour voir qui est connecté
        /// </summary>
        [HttpGet("me")]
        [Authorize] // nécéssite un JWT valide
        public async Task<ActionResult<User>> Me()
        {
            // Récupère l'ID de l'utilisateur depuis le JWT
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("token invalide: pas de user id");

            if (!int.TryParse(userIdClaim, out var userId))
                return Unauthorized("token invalide: user id au mauvais format format");

            // Charge l'utilisateur depuis la DB
            var user = await _userService.GetByIdAsync(userId);
            if (user == null)
                return NotFound("Pas d'User trouvé");

            return Ok(new
            {
                user.UserId,
                user.Firstname,
                user.Lastname,
                user.Email,
                user.Role,
                user.CreatedAt
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnly()
        {
            return Ok("Bienvenue ô Admin tout puissant !");
        }

        private string GenerateJwtToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtKey));
            var creds = new Microsoft.IdentityModel.Tokens.SigningCredentials(key, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
