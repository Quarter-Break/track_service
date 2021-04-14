using System;
using System.ComponentModel.DataAnnotations;

namespace TrackService.Database.Models.Dtos
{
    public class TrackRequest
    {
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
        [Required]
        public string TrackId { get; set; }
        [Required]
        public Guid ArtistId { get; set; }
        [Required]
        public Guid AlbumId { get; set; }
    }
}
