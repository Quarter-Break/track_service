using System;
using System.Threading.Tasks;
using TrackService.Database.Models.Dtos.Requests;
using TrackService.Database.Models.Dtos.Responses;

namespace TrackService.Services
{
    public interface IPlaylistTrackService
    {
        Task<PlaylistTrackResponse> AddAsync(PlaylistTrackRequest request);
        Task<PlaylistTrackResponse> GetByIdAsync(Guid id);
        Task DeleteByIdAsync(Guid id);
    }
}
