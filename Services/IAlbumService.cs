using System;
using System.Threading.Tasks;
using TrackService.Database.Models;

namespace TrackService.Services
{
    public interface IAlbumService
    {
        Task<Album> AddAlbumAsync(Album album);
        Task<Album> GetAlbumByIdAsync(Guid id);
        Task<Album> UpdateAlbumAsync(Album album);
        Task<Album> DeleteAlbumByIdAsync(Guid id);
    }
}
