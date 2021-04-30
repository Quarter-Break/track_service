using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TrackService.Database.Contexts;
using TrackService.Database.Converters;
using TrackService.Database.Models;
using TrackService.Database.Models.Dtos.Requests;
using TrackService.Database.Models.Dtos.Responses;

namespace TrackService.Services
{
    public class TrackModelService : ITrackService
    {
        private readonly TrackContext _context;
        private readonly IDtoConverter<Track, TrackRequest, TrackResponse> _converter;

        public TrackModelService(TrackContext context,
             IDtoConverter<Track, TrackRequest, TrackResponse> converter)
        {
            _context = context;
            _converter = converter;
        }

        public async Task<TrackResponse> AddAsync(TrackRequest request)
        {
            Track track = _converter.DtoToModel(request);
            await _context.AddAsync(track);
            await _context.SaveChangesAsync();

            return await CreateResponseAsync(track);
        }


        public async Task<TrackResponse> GetByIdAsync(Guid id)
        {
            Track track = await GetRawByIdAsync(id);

            return await CreateResponseAsync(track);
        }

        public async Task<TrackResponse> UpdateAsync(Guid id, TrackRequest request)
        {
            Track track = _converter.DtoToModel(request);
            track.Id = id;

            _context.Update(track);
            await _context.SaveChangesAsync();

            return await CreateResponseAsync(track);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            Track track = await GetRawByIdAsync(id);

            _context.Remove(track);
            await _context.SaveChangesAsync();
        }

        private async Task<Track> GetRawByIdAsync(Guid id)
        {
            Track track = await _context.Tracks.FirstOrDefaultAsync(e => e.Id == id);

            if (track == null)
            {
                throw new Exception($"Track with id {id} not found.");
            }

            return track;
        }

        private async Task<TrackResponse> CreateResponseAsync(Track track)
        {
            Album album = await _context.Albums.FirstOrDefaultAsync(e => e.Id == track.AlbumId); // Find album.

            if (album == null)
            {
                throw new Exception($"Album with id {track.AlbumId} not found.");
            }

            TrackResponse trackResponse = _converter.ModelToDto(track); // Convert to response.
            trackResponse.Album = new TrackAlbumResponse(album); // Add album.

            return trackResponse;
        }
    }
}
