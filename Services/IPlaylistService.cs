using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackService.Database.Models;
using TrackService.Database.Models.Dtos.Requests;
using TrackService.Database.Models.Dtos.Responses;

namespace TrackService.Services
{
    public interface IPlaylistService
    {
        Task<Playlist> AddPlaylistAsync(Playlist playlist);
        Task<PlaylistResponse> GetPlaylistByIdAsync(Guid id);
        Task<Playlist> GetRawById(Guid id);
        Task<Playlist> UpdatePlaylistAsync(Playlist playlist);
        Task<Playlist> DeletePlaylistByIdAsync(Guid id);
        Task<List<PlaylistResponse>> GetAllByUserIdAsync(Guid id);
    }
}
