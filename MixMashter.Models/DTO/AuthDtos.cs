using MixMashter.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixMashter.Models.DTOs
{
    //ici mes Data Transfer Object pour l'authentification : login et inscription . Comme pour les Entites je force le string.Empty et met le User par défaut.
    public class RegisterDto
    {
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;        
        public Role Role { get; set; } = Role.User; // Le user pourra choisir uniquement User ou Masher (je bloque la création d'un admin via l'API, il ne pourra être crée que par ajout direct en DB)
    }

    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

