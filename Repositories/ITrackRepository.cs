using LocatieService.Repositories;
using System;
using System.Threading.Tasks;
using TrackService.Database.Models;
using TrackService.Database.Models.Dtos.Responses;

namespace TrackService.Repositories
{
    public interface ITrackRepository : IRepository<Track>
    {
        Task<TrackResponse> GetByIdAsync(Guid id);
        Task<Track> GetRawById(Guid id);
    }
}
