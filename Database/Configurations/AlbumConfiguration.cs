using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TrackService.Database.Models;

namespace TrackService.Database.Configurations
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("album");
            builder.HasKey(x => x.Id);
            builder.Property<string>("Title").IsRequired()
                .HasMaxLength(255);
            builder.Property<Guid>("ArtistId").IsRequired();
            builder.Property<DateTime>("ReleaseDate").IsRequired();

            builder.HasMany(x => x.Tracks);
        }
    }
}
