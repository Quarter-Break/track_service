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

namespace TrackService.Repositories
{
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        private readonly IDtoConverter<Track, TrackRequest, TrackResponse> _converter;
        public AlbumRepository(TrackContext context,
            IDtoConverter<Track, TrackRequest, TrackResponse> converter)
            : base(context)
        {
            _converter = converter;
        }
        public async Task<Album> GetByIdAsync(Guid id)
        {
            Album album = await GetAll().FirstOrDefaultAsync(e => e.Id == id);

            if(album == null)
            {
                throw new Exception($"Album with id {id} not found.");
            }

            album.Tracks = await _context.Tracks.Where(e => e.AlbumId == id).ToListAsync();

            return album;
        }
    }
}
