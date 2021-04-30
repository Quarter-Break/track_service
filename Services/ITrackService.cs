using System;
using System.Threading.Tasks;
using TrackService.Database.Models.Dtos.Requests;
using TrackService.Database.Models.Dtos.Responses;

namespace TrackService.Services
{
    public interface ITrackService
    {
        Task<TrackResponse> AddAsync(TrackRequest request);
        Task<TrackResponse> GetByIdAsync(Guid id);
        Task<TrackResponse> UpdateAsync(Guid id, TrackRequest request);
        Task DeleteByIdAsync(Guid id);
    }
}
