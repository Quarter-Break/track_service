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
    public class AlbumController : Controller
    {
        private readonly IAlbumService _service;
        public AlbumController(IAlbumService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<AlbumResponse>> AddAlbum(AlbumRequest request)
        {
            return await _service.AddAsync(request);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AlbumResponse>> GetAlbumById(Guid id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<AlbumResponse>> UpdateAlbum(Guid id, AlbumRequest request)
        {
            return await _service.UpdateAsync(id, request);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAlbumById(Guid id)
        {
            await _service.DeleteByIdAsync(id); // Delete record.

            return NoContent();
        }
    }
}
