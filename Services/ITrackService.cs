using System;
using System.Threading.Tasks;
using TrackService.Database.Models;
using TrackService.Database.Models.Dtos.Responses;

namespace TrackService.Services
{
    public interface ITrackService
    {
        Task<Track> AddTrackAsync(Track track);
        Task<TrackResponse> GetTrackByIdAsync(Guid id);
        Task<Track> GetRawById(Guid id);
        Task<Track> UpdateTrackAsync(Track track);
        Task<Track> DeleteTrackByIdAsync(Guid id);
    }
}
