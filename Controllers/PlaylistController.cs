using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public PlaylistController(IPlaylistService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<PlaylistResponse>> AddPlaylist(PlaylistRequest request)
        {
            return await _service.AddAsync(request);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PlaylistResponse>> GetPlaylistById(Guid id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpGet]
        [Route("user/{id}")]
        public async Task<ActionResult<List<PlaylistResponse>>> GetPlaylistByUserId(Guid id)
        {
            return await _service.GetAllByUserIdAsync(id);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<PlaylistResponse>> UpdatePlaylist(Guid id, PlaylistRequest request)
        {
            return await _service.UpdateAsync(id, request);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<PlaylistResponse>> DeletePlaylistById(Guid id)
        {
            await _service.DeleteByIdAsync(id);

            return Ok();
        }
    }
}
