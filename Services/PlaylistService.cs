using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackService.Database.Models;
using TrackService.Database.Models.Dtos.Responses;
using TrackService.Repositories;

namespace TrackService.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _repository;

        public PlaylistService(IPlaylistRepository repository)
        {
            _repository = repository;
        }

        public async Task<Playlist> AddPlaylistAsync(Playlist playlist)
        {
            return await _repository.AddAsync(playlist);
        }

        public async Task<Playlist> DeletePlaylistByIdAsync(Guid id)
        {
            return await _repository.DeleteAsync(await _repository.GetRawById(id));
        }

        public async Task<List<PlaylistResponse>> GetAllByUserIdAsync(Guid id)
        {
            return await _repository.GetAllByUserIdAsync(id);
        }

        public async Task<PlaylistResponse> GetPlaylistByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Playlist> GetRawById(Guid id)
        {
            return await _repository.GetRawById(id);
        }

        public async Task<Playlist> UpdatePlaylistAsync(Playlist playlist)
        {
            return await _repository.UpdateAsync(playlist);
        }
    }
}
