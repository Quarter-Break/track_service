using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrackService.Database.Models
{
    public class Playlist
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public Guid UserId { get; set; }

        // Lazy load tracks
        private ILazyLoader LazyLoader { get; set; }
        private ICollection<Track> _tracks;

        [Required]
        public virtual ICollection<Track> Tracks
        {
            get => LazyLoader.Load(this, ref _tracks);
            set => _tracks = value;
        }
    }
}
