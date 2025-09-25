using System;
using System.ComponentModel.DataAnnotations;
using MixMashter.Models.Enums;

namespace MixMashter.Models.DTO
{
    // DTO pour création d’un user (par ex. Admin qui ajoute un utilisateur,
    // mais la plupart des cas passent par Auth/Register)
    public class UserCreateDto
    {
        [Required, MaxLength(100)]
        public string Firstname { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Lastname { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6), MaxLength(256)]
        public string Password { get; set; } = string.Empty;

        // Par défaut User → évite de devoir le passer à chaque fois
        public Role Role { get; set; } = Role.User;
    }

    // DTO pour lecture → ce qu’on renvoie côté API
    public class UserReadDto
    {
        public int UserId { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    // DTO pour mise à jour (profil hors mot de passe/role)
    public class UserUpdateDto
    {
        [Required]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Firstname { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Lastname { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(256)]
        public string Email { get; set; } = string.Empty;
    }

    // DTO pour changement de mot de passe
    public class ChangePasswordDto
    {
        [Required, MinLength(6), MaxLength(256)]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required, MinLength(6), MaxLength(256)]
        public string NewPassword { get; set; } = string.Empty;
    }
}
