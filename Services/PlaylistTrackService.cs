using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackService.Database.Converters;
using TrackService.Database.Models;
using TrackService.Database.Models.Dtos.Requests;
using TrackService.Database.Models.Dtos.Responses;
using TrackService.Repositories;

namespace TrackService.Services
{
    public class PlaylistTrackService : IPlaylistTrackService
    {
        private readonly IPlaylistTrackRepository _repository;
        private readonly IDtoConverter<PlaylistTrack, PlaylistTrackRequest, PlaylistTrackResponse> _converter;


        public PlaylistTrackService(IPlaylistTrackRepository repository,
            IDtoConverter<PlaylistTrack, PlaylistTrackRequest, PlaylistTrackResponse> converter)
        {
            _repository = repository;
            _converter = converter;
        }

        public async Task<PlaylistTrack> AddPlaylistTrackAsync(PlaylistTrackRequest request)
        {
            PlaylistTrack playlistTrack = _converter.DtoToModel(request);

            return await _repository.AddAsync(playlistTrack);
        }

        public async Task<PlaylistTrack> DeletePlaylistTrackByIdAsync(Guid id)
        {
            PlaylistTrack playlistTrack = await _repository.GetByIdAsync(id);

            return await _repository.DeleteAsync(playlistTrack);
        }

        public async Task<PlaylistTrack> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
