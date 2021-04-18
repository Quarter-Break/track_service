using System.Collections.Generic;
using TrackService.Database.Models;
using TrackService.Database.Models.Dtos.Requests;
using TrackService.Database.Models.Dtos.Responses;

namespace TrackService.Database.Converters
{
    public class PlaylistTrackDtoConverter : IDtoConverter<PlaylistTrack, PlaylistTrackRequest, PlaylistTrackResponse>
    {
        public PlaylistTrack DtoToModel(PlaylistTrackRequest request)
        {
            return new PlaylistTrack
            {
                PlaylistId = request.PlaylistId,
                TrackId = request.TrackId
            };
        }

        public PlaylistTrackResponse ModelToDto(PlaylistTrack model)
        {
            return new PlaylistTrackResponse
            {
                Id = model.Id,
                PlaylistId = model.PlaylistId,
                TrackId = model.TrackId
            };
        }

        public List<PlaylistTrackResponse> ModelToDto(List<PlaylistTrack> models)
        {
            throw new System.NotImplementedException();
        }
    }
}
