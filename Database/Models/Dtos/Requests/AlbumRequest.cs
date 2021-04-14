using System;
using System.ComponentModel.DataAnnotations;

namespace TrackService.Database.Models.Dtos
{
    public class AlbumRequest
    {
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
        [Required]
        public Guid ArtistId { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string CoverPath { get; set; }
    }
}
