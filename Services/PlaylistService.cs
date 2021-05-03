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

namespace TrackService.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly TrackContext _context;
        private readonly IDtoConverter<Playlist, PlaylistRequest, PlaylistResponse> _converter;
        private readonly IDtoConverter<Track, TrackRequest, TrackResponse> _trackConverter;

        public PlaylistService(TrackContext context,
            IDtoConverter<Playlist, PlaylistRequest, PlaylistResponse> converter,
            IDtoConverter<Track, TrackRequest, TrackResponse> trackConverter)
        {
            _context = context;
            _converter = converter;
            _trackConverter = trackConverter;
        }

        public async Task<PlaylistResponse> AddAsync(PlaylistRequest request)
        {
            Playlist playlist = _converter.DtoToModel(request);
            await _context.AddAsync(playlist);
            await _context.SaveChangesAsync();

            return await CreateResponseAsync(playlist);
        }

        public async Task<PlaylistResponse> GetByIdAsync(Guid id)
        {
            Playlist playlist = await GetRawByIdAsync(id);

            return await CreateResponseAsync(playlist);
        }


        public async Task<List<PlaylistResponse>> GetAllByUserIdAsync(Guid id)
        {
            List<Playlist> playlists = await _context.Playlists.Where(e => e.UserId == id).ToListAsync();
            List<PlaylistResponse> responses = new();

            foreach (Playlist playlist in playlists)
            {
                responses.Add(await CreateResponseAsync(playlist));
            }

            return responses;
        }


        public async Task<PlaylistResponse> UpdateAsync(Guid id, PlaylistRequest request)
        {
            Playlist playlist = _converter.DtoToModel(request);
            playlist.Id = id;

            _context.Update(playlist);
            await _context.SaveChangesAsync();

            return await CreateResponseAsync(playlist);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            Playlist playlist = await GetRawByIdAsync(id);

            _context.Remove(playlist);
            await _context.SaveChangesAsync();
        }

        private async Task<Playlist> GetRawByIdAsync(Guid id)
        {
            Playlist playlist = await _context.Playlists.FirstOrDefaultAsync(e => e.Id == id);

            if (playlist == null)
            {
                throw new Exception($"Playlist with id {id} not found.");
            }

            return playlist;
        }

        private async Task<PlaylistResponse> CreateResponseAsync(Playlist playlist)
        {
            PlaylistResponse playlistResponse = _converter.ModelToDto(playlist);

            List<PlaylistTrack> playlistTracks = await _context.PlaylistTracks.Where(e => e.PlaylistId == playlist.Id).ToListAsync();
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
    }
}
