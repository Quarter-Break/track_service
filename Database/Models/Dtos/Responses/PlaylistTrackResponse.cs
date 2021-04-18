using System;

namespace TrackService.Database.Models.Dtos.Responses
{
    public class PlaylistTrackResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PlaylistId { get; set; }
        public Guid TrackId { get; set; }
    }
}
