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
        Task<PlaylistResponse> AddAsync(PlaylistRequest request);
        Task<PlaylistResponse> GetByIdAsync(Guid id);
        Task<List<PlaylistResponse>> GetAllByUserIdAsync(Guid id);
        Task<PlaylistResponse> UpdateAsync(Guid id, PlaylistRequest request);
        Task DeleteByIdAsync(Guid id);
    }
}
