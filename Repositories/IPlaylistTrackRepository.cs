using LocatieService.Repositories;
using System;
using System.Threading.Tasks;
using TrackService.Database.Models;

namespace TrackService.Repositories
{
    public interface IPlaylistTrackRepository : IRepository<PlaylistTrack>
    {
        Task<PlaylistTrack> GetByIdAsync(Guid id);
    }
}
