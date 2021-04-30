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
    public class TrackController : Controller
    {
        private readonly ITrackService _service;
        public TrackController(ITrackService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<TrackResponse>> AddTrack(TrackRequest request)
        {
            return await _service.AddAsync(request);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TrackResponse>> GetTrackById(Guid id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<TrackResponse>> UpdateTrack(Guid id, TrackRequest request)
        {
            return await _service.UpdateAsync(id, request);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteTrackById(Guid id)
        {
            await _service.DeleteByIdAsync(id);

            return Ok();
        }
    }
}
