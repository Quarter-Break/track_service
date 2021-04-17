using System;
using System.Collections.Generic;

namespace TrackService.Database.Models.Dtos.Requests
{
    public class PlaylistRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public ICollection<PlaylistTrackRequest> PlaylistTracks { get; set; }
    }
}
