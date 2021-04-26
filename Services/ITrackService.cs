using System;
using System.Threading.Tasks;
using TrackService.Database.Models;

namespace TrackService.Services
{
    public interface ITrackService
    {
        Task<Track> AddTrackAsync(Track track);
        Task<Track> GetTrackByIdAsync(Guid id);
        Task<Track> UpdateTrackAsync(Track track);
        Task<Track> DeleteTrackByIdAsync(Guid id);
    }
}
