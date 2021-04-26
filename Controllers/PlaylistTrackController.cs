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
using TrackService.Services;

namespace TrackService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistTrackController : Controller
    {
        private readonly IPlaylistTrackService _service;
        private readonly IDtoConverter<PlaylistTrack, PlaylistTrackRequest, PlaylistTrackResponse> _converter;

        public PlaylistTrackController(IPlaylistTrackService service,
            IDtoConverter<PlaylistTrack, PlaylistTrackRequest, PlaylistTrackResponse> converter)
        {
            _service = service;
            _converter = converter;
        }

        [HttpPost]
        public async Task<ActionResult<PlaylistTrackResponse>> AddTrackToPlaylist(PlaylistTrackRequest request)
        {
            return Created("Created", await _service.AddPlaylistTrackAsync(request));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PlaylistTrackResponse>> GetPlaylistTrackById(Guid id)
        {
            return Ok(_converter.ModelToDto(await _service.GetByIdAsync(id)));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteById(Guid id)
        {
            await _service.DeletePlaylistTrackByIdAsync(id);

            return Ok();
        }
    }
}
