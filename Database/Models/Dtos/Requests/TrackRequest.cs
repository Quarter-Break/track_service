using System;
using System.Collections.Generic;

namespace TrackService.Database.Models.Dtos.Requests
{
    public class TrackRequest
    {
        public string Title { get; set; }
        public string TrackId { get; set; }
        public Guid ArtistId { get; set; }
        public Guid AlbumId { get; set; }
        public ICollection<PlaylistTrackRequest> PlaylistTracks { get; set; }
    }
}
