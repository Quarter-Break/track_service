using System;

namespace TrackService.Database.Models
{
    public class PlaylistTrack
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PlaylistId { get; set; }
        public Guid TrackId { get; set; }
    }
}
