using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TrackService.Database.Models;

namespace TrackService.Database.Configurations
{
    public class TrackConfiguration : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {
            builder.ToTable("track");
            builder.HasKey(x => x.Id);
            builder.Property<string>("Title").IsRequired().HasMaxLength(255);
            builder.Property<string>("TrackId").IsRequired().HasMaxLength(255);
            builder.Property<Guid>("ArtistId").IsRequired();
            builder.Property<Guid>("AlbumId").IsRequired();
        }
    }
}
