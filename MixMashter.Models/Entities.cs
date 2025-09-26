using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MixMashter.Models.Enums;

namespace MixMashter.Models.Entities


//ici je crée mes entities , et par facilité je vais mettre un string empty pour les string nullable comme ça je "force" , comme ça pas de NULL envoyé en DB
//comme j'ai voulu tester EF que j'avais vu à Technofutur je me suis appuyé sur des exos faits il y a quelques années.
//je doc au possible avec commentaires pour que ce soit plus clair


{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Firstname { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Lastname { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required, MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(256)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public Role Role { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation dans les mashups , je prends une Icollection pour pouvoir avoir plusieurs mashups dans une simple List
        public ICollection<Mashup> Mashups { get; set; } = new List<Mashup>();
        public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
        public ICollection<Favorites> Favorites { get; set; } = new List<Favorites>();
    }

    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }

        [Required, MaxLength(100)]
        public string ArtistName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Band { get; set; }

        [MaxLength(256)]
        public string? WebsiteLink { get; set; }

        // Navigation dans les songs , même tintouin qu'au dessus
        public ICollection<Song> Songs { get; set; } = new List<Song>();
    }

    public class Song
    {
        [Key]
        public int SongId { get; set; }

        [ForeignKey(nameof(Artist))]
        public int ArtistId { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public int Length { get; set; }

        [Required, MaxLength(100)]
        public string Genre { get; set; } = string.Empty;

        public int YearRelease { get; set; }

        public bool IsExplicit { get; set; } // Je passe Explicit en IsExplicit pour éviter conflit avec mot réservé en C#

        // Navigation
        public Artist Artist { get; set; } = null!;
        public ICollection<Mashup_Song> MashupSongs { get; set; } = new List<Mashup_Song>();
    }

    public class Mashup
    {
        [Key]
        public int MashupId { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public int Length { get; set; }

        public bool IsExplicit { get; set; } //Pareil ,je passe Explicit en IsExplicit pour éviter conflit avec mot réservé en C#

        [MaxLength(256)]
        public string? Description { get; set; }

        [MaxLength(100)]
        public string? Format { get; set; }

        [MaxLength(256)]
        public string? UrlLink { get; set; }

        [MaxLength(256)]
        public string? CoverImage { get; set; }

        // Navigation dans les favoris des users + liaison avec les songs et users
        public User User { get; set; } = null!;
        public ICollection<Favorites> Favorites { get; set; } = new List<Favorites>();
        public ICollection<Mashup_Song> MashupSongs { get; set; } = new List<Mashup_Song>();
        public ICollection<Playlist_Mashup> PlaylistMashups { get; set; } = new List<Playlist_Mashup>();

    }

    public class Playlist
    {
        [Key]
        public int PlaylistId { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; }

        // liaison avec les users et les mashups
        public User User { get; set; } = null!;
        public ICollection<Playlist_Mashup> PlaylistMashups { get; set; } = new List<Playlist_Mashup>();
    }

    //ici comme j'ai des attributs en plus dans mes tables de jonction je crée des classes pour elles aussi

    public class Favorites
    {
        public int UserId { get; set; }
        public int MashupId { get; set; }

        public DateTime AddedDate { get; set; }

        // Navigation dans les users et les mashups
        public User User { get; set; } = null!;
        public Mashup Mashup { get; set; } = null!;
    }

    public class Mashup_Song
    {
        public int MashupId { get; set; }
        public int SongId { get; set; }

        // Navigation comme avant dans les autres tables de jonction
        public Mashup Mashup { get; set; } = null!;
        public Song Song { get; set; } = null!;
    }

    public class Playlist_Mashup
    {
        public int PlaylistId { get; set; }
        public int MashupId { get; set; }
        public DateTime AddedDate { get; set; }

        // Navigation dans les playlists et les mashups
        public Playlist Playlist { get; set; } = null!;
        public Mashup Mashup { get; set; } = null!;
    }
}
