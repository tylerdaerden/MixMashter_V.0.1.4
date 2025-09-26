using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MixMashter.Models.DTOs
{
    // DTO pour création
    public class PlaylistCreateDto
    {
        [Required]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;
    }

    // DTO pour lecture
    public class PlaylistReadDto
    {
        public int PlaylistId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }

        // Pour simplifier la vie côté front (et une vie facile est une belle vie)
        public string? Username { get; set; }

        // Liste des Mashups liés à la playlist
        public List<MashupReadDto> Mashups { get; set; } = new List<MashupReadDto>();
    }

    // DTO pour update complet
    public class PlaylistUpdateDto
    {
        [Required]
        public int PlaylistId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;
    }

    // DTO pour PATCH (partiel)
    public class PlaylistPatchDto
    {
        public string? Title { get; set; }
    }
}

