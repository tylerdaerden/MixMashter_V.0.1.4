using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixMashter.Models.DTO
{
    // DTO pour création ici usage en data annotation pour forcer les champs obligatoires, aproche différente testé avant à Technofutur TIC en exercices
    public class SongCreateDto
    {
        [Required]
        public int ArtistId { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public int Length { get; set; }

        [Required, MaxLength(100)]
        public string Genre { get; set; }

        [Required]
        public int YearRelease { get; set; }

        [Required]
        public bool IsExplicit { get; set; }
    }

    //string.Empty par défaut, pour éviter de renvoyer du null côté API.  l'inverse du CREATE/Read où mon [required] gère la validation
    public class SongReadDto
    {
        public int SongId { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Length { get; set; }
        public string Genre { get; set; } = string.Empty;
        public int YearRelease { get; set; }
        public bool IsExplicit { get; set; }

        // Bonus chokotff: afficher le nom de l’artiste directement
        public string ArtistName { get; set; } = string.Empty;
    }

    public class SongUpdateDto
    {
        [Required]
        public int SongId { get; set; }

        [Required]
        public int ArtistId { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public int Length { get; set; }

        [Required, MaxLength(100)]
        public string Genre { get; set; }

        [Required]
        public int YearRelease { get; set; }

        [Required]
        public bool IsExplicit { get; set; }
    }

    public class SongPatchDto
    {
        public int? ArtistId { get; set; }
        public string? Title { get; set; }
        public int? Length { get; set; }
        public string? Genre { get; set; }
        public int? YearRelease { get; set; }
        public bool? IsExplicit { get; set; }
    }



}
