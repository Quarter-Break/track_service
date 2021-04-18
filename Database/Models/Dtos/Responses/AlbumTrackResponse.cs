using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackService.Database.Models.Dtos.Responses
{
    public class AlbumTrackResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string TrackId { get; set; }
        public Guid ArtistId { get; set; }

        public AlbumTrackResponse(Track track)
        {
            Id = track.Id;
            Title = track.Title;
            TrackId = track.TrackId;
            ArtistId = track.ArtistId;
        }
    }
}
