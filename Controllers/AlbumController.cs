using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackService.Database.Contexts;
using TrackService.Database.Converters;
using TrackService.Database.Models;
using TrackService.Database.Models.Dtos.Requests;
using TrackService.Database.Models.Dtos.Responses;

namespace TrackService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : Controller
    {
        private readonly TrackContext _context;
        private readonly IDtoConverter<Album, AlbumRequest, AlbumResponse> _converter;
        private readonly IDtoConverter<Track, TrackRequest, TrackResponse> _trackConverter;
        public AlbumController(TrackContext context, IDtoConverter<Album, AlbumRequest, AlbumResponse> converter,
            IDtoConverter<Track, TrackRequest, TrackResponse> trackConverter)
        {
            _context = context;
            _converter = converter;
            _trackConverter = trackConverter;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAlbum(AlbumRequest request)
        {
            Album album = _converter.DtoToModel(request);
            _context.Albums.Add(album);
            await _context.SaveChangesAsync();

            return Created("Created", album);
        }

        [HttpGet]
        public async Task<ActionResult<List<AlbumResponse>>> GetAllAlbums()
        {
            List<AlbumResponse> albums = _converter.ModelToDto(await _context.Albums.ToListAsync());

            foreach (AlbumResponse album in albums)
            {
                album.Tracks = new();
                List<Track> tracks = await _context.Tracks.Where(e => e.AlbumId == album.Id).ToListAsync(); // Find all tracks with albumId.
                List<AlbumTrackResponse> albumTrackResponses = new();

                foreach(Track track in tracks)
                {
                    albumTrackResponses.Add(new AlbumTrackResponse(track));
                }

                album.Tracks = albumTrackResponses;
            }

            return Ok(albums);
        }
    }
}
