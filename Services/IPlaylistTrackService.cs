using System;
using System.Threading.Tasks;
using TrackService.Database.Models;
using TrackService.Database.Models.Dtos.Requests;
using TrackService.Database.Models.Dtos.Responses;

namespace TrackService.Services
{
    public interface IPlaylistTrackService
    {
        Task<PlaylistTrack> AddPlaylistTrackAsync(PlaylistTrackRequest request);
        Task<PlaylistTrack> GetByIdAsync(Guid id);
        Task<PlaylistTrack> DeletePlaylistTrackByIdAsync(Guid id);
    }
}
