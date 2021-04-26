using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TrackService.Database.Converters;
using TrackService.Database.Models;
using TrackService.Database.Models.Dtos.Requests;
using TrackService.Database.Models.Dtos.Responses;
using TrackService.Services;

namespace TrackService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackController : Controller
    {
        private readonly ITrackService _service;
        private readonly IDtoConverter<Track, TrackRequest, TrackResponse> _converter;
        public TrackController(ITrackService service,
            IDtoConverter<Track, TrackRequest, TrackResponse> converter)
        {
            _service = service;
            _converter = converter;
        }

        [HttpPost]
        public async Task<ActionResult<TrackResponse>> AddTrack(TrackRequest request)
        {
            Track track = _converter.DtoToModel(request);

            return Created("Created", _converter.ModelToDto(await _service.AddTrackAsync(track)));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TrackResponse>> GetTrackById(Guid id)
        {
            return Ok(await _service.GetTrackByIdAsync(id));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<TrackResponse>> UpdateTrack(Guid id, TrackRequest request)
        {
            Track track = _converter.DtoToModel(request); // Create model from updated info.
            track.Id = id; // Add id so record is updated instead of inserted.

            return Ok(_converter.ModelToDto(await _service.UpdateTrackAsync(track)));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteTrackById(Guid id)
        {
            await _service.DeleteTrackByIdAsync(id); // Delete record.

            return Ok(); // Return nothing with 200 status code.
        }
    }
}
