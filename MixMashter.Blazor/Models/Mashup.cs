namespace MixMashter.Blazor.Models.Mashup
{
    public class MashupCreateDto
    {
        public int UserId { get; set; }   // récupéré depuis le JWT
        public string Title { get; set; } = string.Empty;
        public int Length { get; set; }   // durée en secondes (par défaut 0 si inconnu)
        public bool IsExplicit { get; set; }   // false par défaut
        public string Description { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
        public string UrlLink { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
    }

    public class MashupReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
        public string UrlLink { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty; 
        public DateTime DateCreated { get; set; }
    }
}
