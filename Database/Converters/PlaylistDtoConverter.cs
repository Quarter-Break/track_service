using System;
using System.Collections.Generic;
using TrackService.Database.Models;
using TrackService.Database.Models.Dtos;

namespace TrackService.Database.Converters
{
    public class PlaylistDtoConverter : IDtoConverter<Playlist, PlaylistRequest, PlaylistResponse>
    {
        public Playlist DtoToModel(PlaylistRequest request)
        {
            return new Playlist
            {
                Title = request.Title,
                Description = request.Description,
                Tracks = request.Tracks,
                UserId = request.UserId
            };
        }

        public PlaylistResponse ModelToDto(Playlist model)
        {
            return new PlaylistResponse
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Tracks = model.Tracks,
                UserId = model.UserId
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
