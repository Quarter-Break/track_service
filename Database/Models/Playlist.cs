using System;
using System.Collections.Generic;

namespace TrackService.Database.Models
{
    public class Playlist
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public virtual List<Track> Tracks { get; set; }
    }
}
