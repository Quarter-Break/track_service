using LocatieService.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackService.Database.Contexts;
using TrackService.Database.Converters;
using TrackService.Database.Models;
using TrackService.Database.Models.Dtos.Requests;
using TrackService.Database.Models.Dtos.Responses;
using TrackService.Services;

namespace TrackService.Repositories
{
    public class PlaylistRepository : Repository<Playlist>, IPlaylistRepository
    {
        private readonly IDtoConverter<Playlist, PlaylistRequest, PlaylistResponse> _converter;
        private readonly IDtoConverter<Track, TrackRequest, TrackResponse> _trackConverter;

        public PlaylistRepository(TrackContext context,
            IDtoConverter<Playlist, PlaylistRequest, PlaylistResponse> converter,
            IDtoConverter<Track, TrackRequest, TrackResponse> trackConverter)
            : base(context)
        {
            _converter = converter;
            _trackConverter = trackConverter;
        }
        public async Task<List<PlaylistResponse>> GetAllByUserIdAsync(Guid id)
        {
            return _converter.ModelToDto(await GetAll().Where(e => e.UserId == id).ToListAsync());
        }

        public async Task<PlaylistResponse> GetByIdAsync(Guid id)
        {
            Playlist playlist = await GetAll().FirstOrDefaultAsync(e => e.Id == id);
            PlaylistResponse playlistResponse = _converter.ModelToDto(playlist);

            if (playlist == null)
            {
                throw new Exception($"Playlist with id {id} not found.");
            }

            List<PlaylistTrack> playlistTracks = await _context.PlaylistTracks.Where(e => e.PlaylistId == id).ToListAsync();
            List<TrackResponse> tracks = new();
            foreach (PlaylistTrack playlistTrack in playlistTracks)
            {
                Track track = await _context.Tracks.FirstOrDefaultAsync(e => e.Id == playlistTrack.TrackId);

                if (track == null)
                {
                    throw new Exception($"Track with id {playlistTrack.TrackId} not found.");
                }

                TrackResponse trackResponse = _trackConverter.ModelToDto(track);
                Album album = await _context.Albums.FirstOrDefaultAsync(e => e.Id == track.AlbumId); // Find album.
                trackResponse.Album = new TrackAlbumResponse(album);
                tracks.Add(trackResponse);
            }

            playlistResponse.Tracks = tracks;

            return playlistResponse;
        }

        public async Task<Playlist> GetRawById(Guid id)
        {
            return await GetAll().FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
