using System;

namespace TrackService.Database.Models.Dtos.Requests
{
    public class PlaylistTrackRequest
    {
        public Guid PlaylistId { get; set; }
        public Guid TrackId { get; set; }
    }
}
