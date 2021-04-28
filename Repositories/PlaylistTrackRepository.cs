using LocatieService.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackService.Database.Contexts;
using TrackService.Database.Models;

namespace TrackService.Repositories
{
    public class PlaylistTrackRepository : Repository<PlaylistTrack>, IPlaylistTrackRepository
    {
        public PlaylistTrackRepository(TrackContext context) : base(context) { }

        // Override add method:
        public new async Task<PlaylistTrack> AddAsync(PlaylistTrack playlistTrack)
        {
            Playlist playlist = await _context.Playlists.FirstOrDefaultAsync(p => p.Id == playlistTrack.PlaylistId);
            Track track = await _context.Tracks.FirstOrDefaultAsync(t => t.Id == playlistTrack.TrackId);

            if (playlist == null) // Check if playlist exists.
            {
                throw new Exception($"Playlist with id {playlistTrack.PlaylistId} does not exist.");
            }

            if (track == null) // Check if track exists.
            {
                throw new Exception($"Track with id {playlistTrack.TrackId} does not exist.");
            }

            try
            {
                await _context.AddAsync(playlistTrack);
                await _context.SaveChangesAsync();

                return playlistTrack;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't create entity: {ex.Message}");
            }
        }

        public async Task<PlaylistTrack> GetByIdAsync(Guid id)
        {
            return await GetAll().FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
