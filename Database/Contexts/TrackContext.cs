using Microsoft.EntityFrameworkCore;
using TrackService.Database.Models;

namespace TrackService.Database.Contexts
{
    public class TrackContext : DbContext
    {
        public TrackContext(DbContextOptions<TrackContext> options)
            : base(options) { }

        public DbSet<Track> Tracks { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
    }
}
