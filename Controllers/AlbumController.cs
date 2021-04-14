using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class AlbumController : Controller
    {
        private readonly TrackContext _context;
        private readonly IDtoConverter<Album, AlbumRequest, AlbumResponse> _converter;
        public AlbumController(TrackContext context, IDtoConverter<Album, AlbumRequest, AlbumResponse> converter)
        {
            _context = context;
            _converter = converter;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAlbum(AlbumRequest request)
        {
            _context.Albums.Add(_converter.DtoToModel(request));
            await _context.SaveChangesAsync();

            return Created("Created", request);
        }

        [HttpGet]
        public async Task<ActionResult<List<AlbumResponse>>> GetAllAlbums()
        {
            return Ok(_converter.ModelToDto(await _context.Albums.ToListAsync()));
        }
    }
}
