using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TrackService.Database.Contexts;
using TrackService.Database.Converters;
using TrackService.Database.Models;
using TrackService.Database.Models.Dtos.Requests;
using TrackService.Database.Models.Dtos.Responses;

namespace TrackService.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly TrackContext _context;
        private readonly IDtoConverter<Album, AlbumRequest, AlbumResponse> _converter;
        public AlbumService(TrackContext context,
             IDtoConverter<Album, AlbumRequest, AlbumResponse> converter)
        {
            _context = context;
            _converter = converter;
        }

        public async Task<AlbumResponse> AddAsync(AlbumRequest request)
        {
            Album album = _converter.DtoToModel(request);
            await _context.AddAsync(album);
            await _context.SaveChangesAsync();

            return await CreateResponseAsync(album);
        }

        public async Task<AlbumResponse> GetByIdAsync(Guid id)
        {
            Album album = await GetRawByIdAsync(id);

            return await CreateResponseAsync(album);
        }

        public async Task<AlbumResponse> UpdateAsync(Guid id, AlbumRequest request)
        {
            Album album = _converter.DtoToModel(request);
            album.Id = id;

            _context.Update(album);
            await _context.SaveChangesAsync();

            return await CreateResponseAsync(album);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            Album album = await GetRawByIdAsync(id);

            _context.Remove(album);
            await _context.SaveChangesAsync();
        }

        private async Task<Album> GetRawByIdAsync(Guid id)
        {
            Album album = await _context.Albums.FirstOrDefaultAsync(e => e.Id == id);

            if (album == null)
            {
                throw new Exception($"Album with id {id} not found.");
            }

            return album;
        }

        private async Task<AlbumResponse> CreateResponseAsync(Album album)
        {
            album.Tracks = await _context.Tracks.Where(e => e.AlbumId == album.Id).ToListAsync();

            return _converter.ModelToDto(album);
        }
    }
}
