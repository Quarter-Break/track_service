using System.Collections.Generic;
using TrackService.Database.Models;
using TrackService.Database.Models.Dtos.Requests;
using TrackService.Database.Models.Dtos.Responses;

namespace TrackService.Database.Converters
{
    public class AlbumDtoConverter : IDtoConverter<Album, AlbumRequest, AlbumResponse>
    {
        public Album DtoToModel(AlbumRequest request)
        {
            return new Album
            {
                Title = request.Title,
                ArtistId = request.ArtistId,
                ReleaseDate = request.ReleaseDate,
                CoverPath = request.CoverPath
            };
        }

        public AlbumResponse ModelToDto(Album model)
        {
            List<AlbumTrackResponse> tracks = new();

            // Convert tracklist to response list.
            if (model.Tracks != null)
            {
                foreach (Track track in model.Tracks)
                {
                    tracks.Add(new AlbumTrackResponse(track));
                }

            }

            return new AlbumResponse
            {
                Id = model.Id,
                Title = model.Title,
                ArtistId = model.ArtistId,
                ReleaseDate = model.ReleaseDate,
                CoverPath = model.CoverPath,
                Tracks = tracks
            };
        }

        public List<AlbumResponse> ModelToDto(List<Album> models)
        {
            List<AlbumResponse> responses = new();

            foreach (Album album in models)
            {
                responses.Add(ModelToDto(album));
            }

            return responses;
        }
    }
}
