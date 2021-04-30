using System;
using System.Threading.Tasks;
using TrackService.Database.Models.Dtos.Requests;
using TrackService.Database.Models.Dtos.Responses;

namespace TrackService.Services
{
    public interface IAlbumService
    {
        Task<AlbumResponse> AddAsync(AlbumRequest request);
        Task<AlbumResponse> GetByIdAsync(Guid id);
        Task<AlbumResponse> UpdateAsync(Guid id, AlbumRequest request);
        Task DeleteByIdAsync(Guid id);
    }
}
