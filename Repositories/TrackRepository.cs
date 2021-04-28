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
    public class TrackRepository : Repository<Track>, ITrackRepository
    {
        private readonly IAlbumService _albumService;
        private readonly IDtoConverter<Track, TrackRequest, TrackResponse> _converter;

        public TrackRepository(TrackContext context,
            IAlbumService albumService,
            IDtoConverter<Track, TrackRequest, TrackResponse> converter)
            : base(context)
        {
            _albumService = albumService;
            _converter = converter;
        }

        public async Task<TrackResponse> GetByIdAsync(Guid id)
        {
            Track track = await GetAll().FirstOrDefaultAsync(e => e.Id == id);

            Album album = await _albumService.GetAlbumByIdAsync(track.AlbumId); // Find album.

            if (album == null)
            {
                throw new Exception($"Album with id {track.AlbumId} not found.");
            }

            TrackResponse trackResponse = _converter.ModelToDto(track); // Convert to response.
            trackResponse.Album = new TrackAlbumResponse(album); // Add album.

            return trackResponse;
        }

        public async Task<Track> GetRawById(Guid id)
        {
            return await GetAll().FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
