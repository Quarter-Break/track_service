using System;

namespace TrackService.Database.Models.Dtos
{
    public class AlbumResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid ArtistId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string CoverPath { get; set; }
    }
}
