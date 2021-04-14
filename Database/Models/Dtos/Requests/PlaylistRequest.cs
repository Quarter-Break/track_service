using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrackService.Database.Models.Dtos
{
    public class PlaylistRequest
    {
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public ICollection<Track> Tracks { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
