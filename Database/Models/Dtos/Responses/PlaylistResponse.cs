using System;
using System.Collections.Generic;

namespace TrackService.Database.Models.Dtos
{
    public class PlaylistResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Track> Tracks { get; set; }
        public Guid UserId { get; set; }
    }
}
