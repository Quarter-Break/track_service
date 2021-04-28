using LocatieService.Repositories;
using System;
using System.Threading.Tasks;
using TrackService.Database.Models;

namespace TrackService.Repositories
{
    public interface IAlbumRepository : IRepository<Album>
    {
        Task<Album> GetByIdAsync(Guid id);
    }
}
