using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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

        public PlaylistTrackController(IPlaylistTrackService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<PlaylistTrackResponse>> AddTrackToPlaylist(PlaylistTrackRequest request)
        {
            return await _service.AddAsync(request);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PlaylistTrackResponse>> GetPlaylistTrackById(Guid id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteById(Guid id)
        {
            await _service.DeleteByIdAsync(id);

            return Ok();
        }
    }
}
