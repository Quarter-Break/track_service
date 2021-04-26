﻿using Microsoft.AspNetCore.Mvc;
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
    public class AlbumController : Controller
    {
        private readonly IAlbumService _service;
        private readonly IDtoConverter<Album, AlbumRequest, AlbumResponse> _converter;
        private readonly IDtoConverter<Track, TrackRequest, TrackResponse> _trackConverter;
        public AlbumController(IAlbumService service,
            IDtoConverter<Album, AlbumRequest, AlbumResponse> converter,
            IDtoConverter<Track, TrackRequest, TrackResponse> trackConverter)
        {
            _service = service;
            _converter = converter;
            _trackConverter = trackConverter;
        }

        [HttpPost]
        public async Task<ActionResult<AlbumResponse>> AddAlbum(AlbumRequest request)
        {
            Album album = _converter.DtoToModel(request); // Create album from request.
            await _service.AddAlbumAsync(album); // Add to db using service.

            return Created("Created", _converter.ModelToDto(album)); // Return response.
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AlbumResponse>> GetAlbumById(Guid id)
        {
            return Ok(_converter.ModelToDto(await _service.GetAlbumByIdAsync(id)));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<AlbumResponse>> UpdateAlbum(Guid id, AlbumRequest request)
        {
            Album album = _converter.DtoToModel(request);
            album.Id = id; // Add id so record is updated instead of inserted.

            return Ok(_converter.ModelToDto(await _service.UpdateAlbumAsync(album)));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<AlbumResponse>> DeleteAlbumById(Guid id)
        {
            return Ok(_converter.ModelToDto(await _service.DeleteAlbumByIdAsync(id)));
        }
    }
}
