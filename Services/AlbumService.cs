using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackService.Database.Models;
using TrackService.Repositories;

namespace TrackService.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _repository;
        public AlbumService(IAlbumRepository repository)
        {
            _repository = repository;
        }
        public async Task<Album> AddAlbumAsync(Album album)
        {
            return await _repository.AddAsync(album);
        }

        public async Task<Album> DeleteAlbumByIdAsync(Guid id)
        {
            return await _repository.DeleteAsync(await _repository.GetByIdAsync(id));
        }

        public async Task<Album> GetAlbumByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Album> UpdateAlbumAsync(Album album)
        {
            return await _repository.UpdateAsync(album);
        }
    }
}
