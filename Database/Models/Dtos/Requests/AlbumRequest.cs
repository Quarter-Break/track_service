using System;
using System.Collections.Generic;

namespace TrackService.Database.Models.Dtos.Requests
{
    public class AlbumRequest
    {
        public string Title { get; set; }
        public Guid ArtistId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string CoverPath { get; set; }
        public ICollection<TrackRequest> Tracks { get; set; }
    }
}
