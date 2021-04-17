﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrackService.Database.Contexts;

namespace TrackService.Migrations
{
    [DbContext(typeof(TrackContext))]
    [Migration("20210417150913_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TrackService.Database.Models.Album", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ArtistId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CoverPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("album");
                });

            modelBuilder.Entity("TrackService.Database.Models.Playlist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("playlist");
                });

            modelBuilder.Entity("TrackService.Database.Models.PlaylistTrack", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlaylistId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TrackId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TrackId");

                    b.ToTable("playlist_track");
                });

            modelBuilder.Entity("TrackService.Database.Models.Track", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AlbumId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ArtistId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PlaylistId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TrackId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("PlaylistId");

                    b.ToTable("track");
                });

            modelBuilder.Entity("TrackService.Database.Models.PlaylistTrack", b =>
                {
                    b.HasOne("TrackService.Database.Models.Track", null)
                        .WithMany("PlaylistTracks")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrackService.Database.Models.Track", b =>
                {
                    b.HasOne("TrackService.Database.Models.Album", null)
                        .WithMany("Tracks")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrackService.Database.Models.Playlist", null)
                        .WithMany("Tracks")
                        .HasForeignKey("PlaylistId");
                });

            modelBuilder.Entity("TrackService.Database.Models.Album", b =>
                {
                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("TrackService.Database.Models.Playlist", b =>
                {
                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("TrackService.Database.Models.Track", b =>
                {
                    b.Navigation("PlaylistTracks");
                });
#pragma warning restore 612, 618
        }
    }
}