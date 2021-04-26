using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
    public class PlaylistController : Controller
    {
        private readonly IPlaylistService _service;
        private readonly IDtoConverter<Playlist, PlaylistRequest, PlaylistResponse> _converter;
        public PlaylistController(IPlaylistService service,
            IDtoConverter<Playlist, PlaylistRequest, PlaylistResponse> converter)
        {
            _service = service;
            _converter = converter;
        }

        [HttpPost]
        public async Task<ActionResult<PlaylistResponse>> AddPlaylist(PlaylistRequest request)
        {
            Playlist playlist = _converter.DtoToModel(request);

            return Created("Created", _converter.ModelToDto(await _service.AddPlaylistAsync(playlist)));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PlaylistResponse>> GetPlaylistById(Guid id)
        {
            return Ok(await _service.GetPlaylistByIdAsync(id));
        }

        [HttpGet]
        [Route("user/{id}")]
        public async Task<ActionResult<List<PlaylistResponse>>> GetPlaylistByUserId(Guid id)
        {
            return Ok(await _service.GetAllByUserIdAsync(id));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<PlaylistResponse>> UpdatePlaylist(Guid id, PlaylistRequest request)
        {
            Playlist playlist = _converter.DtoToModel(request);
            playlist.Id = id;

            return Ok(_converter.ModelToDto(await _service.UpdatePlaylistAsync(playlist)));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<PlaylistResponse>> DeletePlaylistById(Guid id)
        {
            await _service.DeletePlaylistByIdAsync(id);

            return Ok();
        }
    }
}
