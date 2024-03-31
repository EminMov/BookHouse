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

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllGenres()
        {
            var result = await _genreService.GetAllGenres();
            //return Ok(result);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var result = await _genreService.GenreGetByID(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBook([FromBody] GenreAddDTO genreAddDTO)
        {
            var result = await _genreService.GenreAdd(genreAddDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateGenre([FromBody] GenreUpdateDTO genreUpdateDTO, int id)
        {
            var result = await _genreService.GenreUpdate(genreUpdateDTO, id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var result = await _genreService.GenreDelete(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
