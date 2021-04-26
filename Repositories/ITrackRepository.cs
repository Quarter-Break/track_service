using LocatieService.Repositories;
using System;
using System.Threading.Tasks;
using TrackService.Database.Models;

namespace TrackService.Repositories
{
    public interface ITrackRepository : IRepository<Track>
    {
        Task<Track> GetByIdAsync(Guid id);
    }
}
