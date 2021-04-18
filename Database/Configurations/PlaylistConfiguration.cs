using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TrackService.Database.Models;

namespace TrackService.Database.Configurations
{
    public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
    {
        public void Configure(EntityTypeBuilder<Playlist> builder)
        {
            builder.ToTable("playlist");
            builder.HasKey(x => x.Id);
            builder.Property<string>("Title").IsRequired().HasMaxLength(255);
            builder.Property<Guid>("UserId").IsRequired();
        }
    }
}
