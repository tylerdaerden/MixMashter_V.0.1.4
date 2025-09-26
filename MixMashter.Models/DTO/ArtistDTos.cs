using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixMashter.Models.DTOs
{
    public class ArtistReadDto
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; } = string.Empty;
        public string? Band { get; set; }
        public string? WebsiteLink { get; set; }
    }

    public class ArtistCreateDto
    {
        public string ArtistName { get; set; } = string.Empty;
        public string? Band { get; set; }
        public string? WebsiteLink { get; set; }
    }

    public class ArtistUpdateDto
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; } = string.Empty;
        public string? Band { get; set; }
        public string? WebsiteLink { get; set; }
    }

    public class ArtistPatchDto
    {
        public string? ArtistName { get; set; }
        public string? Band { get; set; }
        public string? WebsiteLink { get; set; }
    }
}

