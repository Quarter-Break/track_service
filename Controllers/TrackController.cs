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
            _context.Tracks.Add(_converter.DtoToModel(request));
            await _context.SaveChangesAsync();

            return Created("Created", request);
        }

        [HttpGet]
        public async Task<ActionResult<List<TrackResponse>>> GetAllTracks()
        {
            return Ok(_converter.ModelToDto(await _context.Tracks.ToListAsync()));
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

            return Ok(_converter.ModelToDto(track));
        }
    }
}
