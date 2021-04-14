using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackService.Database.Contexts;
using TrackService.Database.Converters;
using TrackService.Database.Models;
using TrackService.Database.Models.Dtos;

namespace TrackService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistController : Controller
    {
        private readonly TrackContext _context;
        private readonly IDtoConverter<Playlist, PlaylistRequest, PlaylistResponse> _converter;
        public PlaylistController(TrackContext context, IDtoConverter<Playlist, PlaylistRequest, PlaylistResponse> converter)
        {
            _context = context;
            _converter = converter;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePlaylist(PlaylistRequest request)
        {
            _context.Playlists.Add(_converter.DtoToModel(request));
            await _context.SaveChangesAsync();

            return Created("Created", request);
        }

        [HttpPut]
        [Route("track")]
        public async Task<ActionResult> AddTrackToPlaylist(Guid playlistId, Guid trackId)
        {
            Playlist playlist = await _context.Playlists.FirstOrDefaultAsync(p => p.Id == playlistId);
            Track track = await _context.Tracks.FirstOrDefaultAsync(t => t.Id == trackId);

            if (playlist.Equals(null)) // Check if playlist exists.
            {
                return NotFound("Playlist does not exist.");
            }

            if (track.Equals(null)) // Check if track exists.
            {
                return NotFound("Track does not exist.");
            }

            //
            // CHECK IF TRACK ALREADY EXISTS IN LIST
            //

            playlist.Tracks.Add(track); // Add track to playlist.
            _context.Playlists.Update(playlist); // Update playlist record in DbSet.
            _context.SaveChanges();

            return Accepted();
        }

        [HttpGet]
        public async Task<ActionResult<List<PlaylistResponse>>> GetAllPlaylists()
        {
            return Ok(_converter.ModelToDto(await _context.Playlists.ToListAsync()));
        }

        [HttpDelete]
        [Route("track")]
        public async Task<ActionResult> RemoveTrackFromPlaylist(Guid playlistId, Guid trackId)
        {
            Playlist playlist = await _context.Playlists.FirstOrDefaultAsync(p => p.Id == playlistId);
            Track track = await _context.Tracks.FirstOrDefaultAsync(t => t.Id == trackId);

            if (playlist == null) // Check if playlist exists.
            {
                return NotFound("Playlist does not exist.");
            }

            if (track == null) // Check if track exists.
            {
                return NotFound("Track does not exist.");
            }

            playlist.Tracks.Remove(track); // Remove track from playlist.
            _context.Playlists.Update(playlist); // Update playlist record in DbSet.
            _context.SaveChanges();

            return Accepted();
        }
    }
}
