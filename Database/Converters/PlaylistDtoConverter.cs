using System;
using System.Collections.Generic;
using TrackService.Database.Models;
using TrackService.Database.Models.Dtos.Requests;
using TrackService.Database.Models.Dtos.Responses;

namespace TrackService.Database.Converters
{
    public class PlaylistDtoConverter : IDtoConverter<Playlist, PlaylistRequest, PlaylistResponse>
    {
        private readonly IDtoConverter<Track, TrackRequest, TrackResponse> _trackConverter;

        public PlaylistDtoConverter(IDtoConverter<Track, TrackRequest, TrackResponse> trackConverter)
        {
            _trackConverter = trackConverter;
        }

        public Playlist DtoToModel(PlaylistRequest request)
        {
            return new Playlist
            {
                Title = request.Title,
                Description = request.Description,
                UserId = request.UserId,
            };
        }

        public PlaylistResponse ModelToDto(Playlist model)
        {
            return new PlaylistResponse
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                UserId = model.UserId,
                Tracks = _trackConverter.ModelToDto(model.Tracks)
            };
        }

        public List<PlaylistResponse> ModelToDto(List<Playlist> models)
        {
            List<PlaylistResponse> responses = new();

            foreach (Playlist playlist in models)
            {
                responses.Add(ModelToDto(playlist));
            }

            return responses;
        }
    }
}
