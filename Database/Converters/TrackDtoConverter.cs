using System.Collections.Generic;
using TrackService.Database.Models;
using TrackService.Database.Models.Dtos.Requests;
using TrackService.Database.Models.Dtos.Responses;

namespace TrackService.Database.Converters
{
    public class TrackDtoConverter : IDtoConverter<Track, TrackRequest, TrackResponse>
    {
        public Track DtoToModel(TrackRequest request)
        {
            return new Track
            {
                Title = request.Title,
                TrackId = request.TrackId,
                ArtistId = request.ArtistId,
                AlbumId = request.AlbumId
            };
        }

        public TrackResponse ModelToDto(Track model)
        {
            return new TrackResponse
            {
                Id = model.Id,
                Title = model.Title,
                TrackId = model.TrackId,
                ArtistId = model.ArtistId,
                // Add album in controller.
            };
        }

        public List<TrackResponse> ModelToDto(List<Track> models)
        {
            List<TrackResponse> responses = new();

            foreach (Track track in models)
            {
                responses.Add(ModelToDto(track));
            }

            return responses;
        }
    }
}
