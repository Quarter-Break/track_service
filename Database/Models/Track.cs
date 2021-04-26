using System;
using System.Collections.Generic;

namespace TrackService.Database.Models
{
    public class Track
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string TrackId { get; set; }
        public Guid ArtistId { get; set; }
        public Guid AlbumId { get; set; }
    }
}
