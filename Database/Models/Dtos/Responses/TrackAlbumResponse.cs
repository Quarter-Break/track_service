using System;

namespace TrackService.Database.Models.Dtos.Responses
{
    public class TrackAlbumResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid ArtistId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string CoverPath { get; set; }

        public TrackAlbumResponse(Album album)
        {
            Id = album.Id;
            Title = album.Title;
            ArtistId = album.ArtistId;
            ReleaseDate = album.ReleaseDate;
            CoverPath = album.CoverPath;
        }
    }
}
