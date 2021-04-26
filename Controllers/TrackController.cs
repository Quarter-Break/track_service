using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IAlbumService _albumService;
        private readonly IDtoConverter<Track, TrackRequest, TrackResponse> _converter;
        public TrackController(ITrackService service,
            IAlbumService albumService,
            IDtoConverter<Track, TrackRequest, TrackResponse> converter)
        {
            _service = service;
            _albumService = albumService;
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
            Track track = await _service.GetTrackByIdAsync(id);

            Album album = await _albumService.GetAlbumByIdAsync(track.AlbumId); // Find album.

            if (album == null)
            {
                return NotFound($"Album with id {id} not found.");
            }

            TrackResponse trackResponse = _converter.ModelToDto(track); // Convert to response.
            trackResponse.Album = new TrackAlbumResponse(album); // Add album.

            return Ok(trackResponse);
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
