using Microsoft.AspNetCore.Mvc;
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

namespace TrackService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistController : Controller
    {
        private readonly TrackContext _context;
        private readonly IDtoConverter<Playlist, PlaylistRequest, PlaylistResponse> _converter;
        private readonly IDtoConverter<Track, TrackRequest, TrackResponse> _trackConverter;
        public PlaylistController(TrackContext context, IDtoConverter<Playlist, PlaylistRequest, PlaylistResponse> converter,
            IDtoConverter<Track, TrackRequest, TrackResponse> trackConverter)
        {
            _context = context;
            _converter = converter;
            _trackConverter = trackConverter;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePlaylist(PlaylistRequest request)
        {
            Playlist playlist = _converter.DtoToModel(request);
            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();

            return Created("Created", playlist);
        }

        [HttpGet]
        public async Task<ActionResult<List<PlaylistResponse>>> GetAllPlaylists() // THIS METHOD EXISTS ONLY FOR TESTING. SHOULD NOT BE IN PRODUCTION
        {
            List<Playlist> playlists = await _context.Playlists.ToListAsync();

            return Ok(playlists);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<PlaylistResponse>>> GetPlaylistById(Guid id)
        {
            Playlist playlist = await _context.Playlists.FirstOrDefaultAsync(e => e.Id == id);
            if (playlist == null)
            {
                return NotFound($"Playlist with id {id} does not exist.");
            }

            List<PlaylistTrack> playlistTracks = await _context.PlaylistTracks.Where(e => e.PlaylistId == id).ToListAsync();
            List<TrackResponse> tracks = new();
            foreach(PlaylistTrack playlistTrack in playlistTracks)
            {
                Track track = await _context.Tracks.FirstOrDefaultAsync(e => e.Id == playlistTrack.TrackId);
                if (track != null)
                {
                    TrackResponse trackResponse = _trackConverter.ModelToDto(track);
                    Album album = await _context.Albums.FirstOrDefaultAsync(e => e.Id == track.AlbumId); // Find album.
                    trackResponse.Album = new TrackAlbumResponse(album);
                    tracks.Add(trackResponse);
                }
            }

            PlaylistResponse playlistResponse = _converter.ModelToDto(playlist);
            playlistResponse.Tracks = tracks;

            return Ok(playlistResponse);
        }
    }
}
