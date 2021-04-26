using System;
using System.Threading.Tasks;
using TrackService.Database.Models;
using TrackService.Repositories;

namespace TrackService.Services
{
    public class TrackModelService : ITrackService
    {
        private readonly ITrackRepository _repository;

        public TrackModelService(ITrackRepository repository)
        {
            _repository = repository;
        }

        public async Task<Track> AddTrackAsync(Track track)
        {
            return await _repository.AddAsync(track);
        }

        public async Task<Track> DeleteTrackByIdAsync(Guid id)
        {
            return await _repository.DeleteAsync(await _repository.GetByIdAsync(id));
        }

        public async Task<Track> GetTrackByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Track> UpdateTrackAsync(Track track)
        {
            return await _repository.UpdateAsync(track);
        }
    }
}
