using Microsoft.EntityFrameworkCore;
using TrackService.Database.Configurations;
using TrackService.Database.Models;

namespace TrackService.Database.Contexts
{
    public class TrackContext : DbContext
    {
        public TrackContext(DbContextOptions<TrackContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new PlaylistConfiguration());
            modelBuilder.ApplyConfiguration(new PlaylistTrackConfiguration());
            modelBuilder.ApplyConfiguration(new TrackConfiguration());
        }

        public DbSet<Track> Tracks { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistTrack> PlaylistTracks { get; set; }

    }
}
