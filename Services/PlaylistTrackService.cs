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
    public class PlaylistTrackService : IPlaylistTrackService
    {
        private readonly TrackContext _context;
        private readonly IDtoConverter<PlaylistTrack, PlaylistTrackRequest, PlaylistTrackResponse> _converter;

        public PlaylistTrackService(TrackContext context,
            IDtoConverter<PlaylistTrack, PlaylistTrackRequest, PlaylistTrackResponse> converter)
        {
            _context = context;
            _converter = converter;
        }

        public async Task<PlaylistTrackResponse> AddAsync(PlaylistTrackRequest request)
        {
            PlaylistTrack playlistTrack = _converter.DtoToModel(request);
            await _context.AddAsync(playlistTrack);
            await _context.SaveChangesAsync();

            return _converter.ModelToDto(playlistTrack);
        }


        public async Task<PlaylistTrackResponse> GetByIdAsync(Guid id)
        {
            PlaylistTrack playlistTrack = await GetRawByIdAsync(id);

            return _converter.ModelToDto(playlistTrack);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            PlaylistTrack playlistTrack = await GetRawByIdAsync(id);

            _context.Remove(playlistTrack);
            await _context.SaveChangesAsync();
        }

        private async Task<PlaylistTrack> GetRawByIdAsync(Guid id)
        {
            PlaylistTrack playlistTrack = await _context.PlaylistTracks.FirstOrDefaultAsync(e => e.Id == id);

            if (playlistTrack == null)
            {
                throw new Exception($"PlaylistTrack with id {id} not found.");
            }

            return playlistTrack;
        }
    }
}
