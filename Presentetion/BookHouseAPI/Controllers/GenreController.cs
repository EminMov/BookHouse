using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.BookDTOs;
using BookHouseAPI.Application.DTOs.GenreDTOs;
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

        [HttpGet("get")]
        public async Task<IActionResult> GetAllGenres()
        {
            var result = await _genreService.GetAllGenres();
            //return Ok(result);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetGenreById([FromQuery] int id)
        {
            var result = await _genreService.GenreGetByID(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBook([FromBody] GenreDTO genreAddDTO)
        {
            var result = await _genreService.GenreAdd(genreAddDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateGenre([FromBody] GenreDTO genreUpdateDTO, int id)
        {
            var result = await _genreService.GenreUpdate(genreUpdateDTO, id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteGenre([FromQuery] int id)
        {
            var result = await _genreService.GenreDelete(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
