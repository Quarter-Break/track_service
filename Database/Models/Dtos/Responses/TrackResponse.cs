using System;

namespace TrackService.Database.Models.Dtos.Responses
{
    public class TrackResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string TrackId { get; set; }
        public Guid ArtistId { get; set; }
        public TrackAlbumResponse Album { get; set; }
    }
}
