using System;
using System.ComponentModel.DataAnnotations;

namespace TrackService.Database.Models
{
    public class Track
    {
        [Key]
        public Guid Id { get; set; }
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
