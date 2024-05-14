using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.BookDTOs;
using BookHouseAPI.Application.DTOs.GenreDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookHouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAllGenres()
        {
            var result = await _genreService.GetAllGenres();
            //return Ok(result);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("/api/Genre/{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetGenreById([FromRoute] int id)
        {
            var result = await _genreService.GenreGetByID(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBook([FromBody] GenreDTO genreAddDTO)
        {
            var result = await _genreService.GenreAdd(genreAddDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGenre([FromBody] GenreDTO genreUpdateDTO, int id)
        {
            var result = await _genreService.GenreUpdate(genreUpdateDTO, id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("/api/Genre/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGenre([FromQuery] int id)
        {
            var result = await _genreService.GenreDelete(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
