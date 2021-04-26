using LocatieService.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackService.Database.Models;
using TrackService.Database.Models.Dtos.Responses;

namespace TrackService.Repositories
{
    public interface IPlaylistRepository : IRepository<Playlist>
    {
        Task<PlaylistResponse> GetByIdAsync(Guid id);
        Task<List<PlaylistResponse>> GetAllByUserIdAsync(Guid id);
        Task<Playlist> GetRawById(Guid id);
    }
}
