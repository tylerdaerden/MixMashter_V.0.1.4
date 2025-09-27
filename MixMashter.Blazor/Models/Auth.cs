using System.ComponentModel.DataAnnotations;

namespace MixMashter.Blazor.Models.Auth
{
    //mes DTOs côté Blazor pour l'authentification , copie de ceux utilisé dans le projet API
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterDto
    {
        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Le prénom doit contenir entre 2 et 100 caractères.")]
        public string Firstname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Le nom doit contenir entre 2 et 100 caractères.")]
        public string Lastname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nom d’utilisateur est obligatoire.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Le nom d’utilisateur doit contenir entre 3 et 100 caractères.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "L’email est obligatoire.")]
        [EmailAddress(ErrorMessage = "Format d’email invalide.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [MinLength(8, ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Le mot de passe doit contenir une majuscule, une minuscule, un chiffre et un caractère spécial.")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [RegularExpression("User|Masher", ErrorMessage = "Le rôle doit être User ou Masher.")]
        public string Role { get; set; } = "User"; // par défaut
    }

    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
