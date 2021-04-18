using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    public class TrackController : Controller
    {
        private readonly TrackContext _context;
        private readonly IDtoConverter<Track, TrackRequest, TrackResponse> _converter;
        public TrackController(TrackContext context, IDtoConverter<Track, TrackRequest, TrackResponse> converter)
        {
            _context = context;
            _converter = converter;
        }

        [HttpPost]
        public async Task<ActionResult> CreateTrack(TrackRequest request)
        {
            Album album = await _context.Albums.FirstOrDefaultAsync(e => e.Id == request.AlbumId);

            if (album == null)
            {
                return NotFound($"Album with id {request.AlbumId} does not exist.");
            }

            Track track = _converter.DtoToModel(request);
            _context.Tracks.Add(track);
            await _context.SaveChangesAsync();

            return Created("Created", track);
        }

        [HttpGet]
        public async Task<ActionResult<List<TrackResponse>>> GetAllTracks()
        {
            List<Track> tracks = await _context.Tracks.ToListAsync();
            List<TrackResponse> trackResponses = new();

            foreach (Track track in tracks)
            {
                Album album = await _context.Albums.FirstOrDefaultAsync(e => e.Id == track.AlbumId); // Find album.
                TrackResponse trackResponse = _converter.ModelToDto(track);
                trackResponse.Album = new TrackAlbumResponse(album);
                trackResponses.Add(trackResponse);
            }

            return Ok(trackResponses);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TrackResponse>> GetTrackById(Guid id)
        {
            Track track = await _context.Tracks.FirstOrDefaultAsync(e => e.Id == id);

            if (track == null)
            {
                return NotFound("Object not found.");
            }

            Album album = await _context.Albums.FirstOrDefaultAsync(e => e.Id == track.AlbumId); // Find album.
            TrackResponse trackResponse = _converter.ModelToDto(track);
            trackResponse.Album = new TrackAlbumResponse(album);

            return Ok(trackResponse);
        }
    }
}
