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
    public class PlaylistTrackController : Controller
    {
        private readonly TrackContext _context;
        private readonly IDtoConverter<PlaylistTrack, PlaylistTrackRequest, PlaylistTrackResponse> _converter;

        public PlaylistTrackController(TrackContext context, IDtoConverter<PlaylistTrack, PlaylistTrackRequest, PlaylistTrackResponse> converter)
        {
            _context = context;
            _converter = converter;
        }

        [HttpPost]
        public async Task<ActionResult> AddTrackToPlaylist(PlaylistTrackRequest request)
        {
            Playlist playlist = await _context.Playlists.FirstOrDefaultAsync(p => p.Id == request.PlaylistId);
            Track track = await _context.Tracks.FirstOrDefaultAsync(t => t.Id == request.TrackId);

            if (playlist == null) // Check if playlist exists.
            {
                return NotFound("Playlist does not exist.");
            }

            if (track == null) // Check if track exists.
            {
                return NotFound("Track does not exist.");
            }
            PlaylistTrack playlistTrack = _converter.DtoToModel(request);
            _context.PlaylistTracks.Add(playlistTrack); // Insert playlistTrack object in db.
            _context.SaveChanges();

            return Created("Created", playlistTrack);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteById(Guid id)
        {
            PlaylistTrack playlistTrack = await _context.PlaylistTracks.FirstOrDefaultAsync(e => e.Id == id);

            if (playlistTrack == null)
            {
                return NotFound($"PlaylistTrack with id {id} does not exist.");
            }

            _context.Remove(playlistTrack); // Remove track from playlist.
            _context.SaveChanges();

            return Ok();
        }
    }
}
