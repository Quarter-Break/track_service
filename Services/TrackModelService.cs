using System;
using System.Threading.Tasks;
using TrackService.Database.Converters;
using TrackService.Database.Models;
using TrackService.Database.Models.Dtos.Requests;
using TrackService.Database.Models.Dtos.Responses;
using TrackService.Repositories;

namespace TrackService.Services
{
    public class TrackModelService : ITrackService
    {
        private readonly ITrackRepository _repository;
        private readonly IDtoConverter<Track, TrackRequest, TrackResponse> _converter;

        public TrackModelService(ITrackRepository repository,
           IDtoConverter<Track, TrackRequest, TrackResponse> converter)
        {
            _repository = repository;
            _converter = converter;
        }

        public async Task<Track> AddTrackAsync(Track track)
        {
            return await _repository.AddAsync(track);
        }

        public async Task<Track> DeleteTrackByIdAsync(Guid id)
        {
            return await _repository.DeleteAsync(await _repository.GetRawById(id));
        }

        public async Task<Track> GetRawById(Guid id)
        {
            return await _repository.GetRawById(id);
        }

        public async Task<TrackResponse> GetTrackByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Track> UpdateTrackAsync(Track track)
        {
            return await _repository.UpdateAsync(track);
        }
    }
}
