using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MixMashter.Models.DTOs
{
    // DTO pour créer un Mashup
    public class MashupCreateDto
    {
        [Required]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public int Length { get; set; }

        public bool IsExplicit { get; set; }

        [MaxLength(256)]
        public string? Description { get; set; }

        [MaxLength(100)]
        public string? Format { get; set; }

        [MaxLength(256)]
        public string? UrlLink { get; set; }

        [MaxLength(256)]
        public string? CoverImage { get; set; }
    }

    // DTO pour mise à jour complète
    public class MashupUpdateDto
    {
        [Required]
        public int MashupId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public int Length { get; set; }

        public bool IsExplicit { get; set; }

        [MaxLength(256)]
        public string? Description { get; set; }

        [MaxLength(100)]
        public string? Format { get; set; }

        [MaxLength(256)]
        public string? UrlLink { get; set; }

        [MaxLength(256)]
        public string? CoverImage { get; set; }
    }

    // DTO pour lecture (réponses API)
    public class MashupReadDto
    {
        public int MashupId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Length { get; set; }
        public bool IsExplicit { get; set; }
        public string? Description { get; set; }
        public string? Format { get; set; }
        public string? UrlLink { get; set; }
        public string? CoverImage { get; set; }

        // Pour + de lisibilité côté front
        public string? Username { get; set; }
    }

    /// <summary>
    /// DTO pour appliquer un patch sur un Mashup sans inclure les navigations EF (User, Favorites, etc.)
    /// pour réparer une erreur constatée pendant piti test sur swagger lors du patch d'un mashup , il demandait un userId alors que je ne veux pas le modifier
    /// </summary>
    public class MashupPatchDto
    {
        public string? Title { get; set; }
        public int? Length { get; set; }
        public bool? IsExplicit { get; set; }
        public string? Description { get; set; }
        public string? Format { get; set; }
        public string? UrlLink { get; set; }
        public string? CoverImage { get; set; }
    }


}
