using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.AuthorDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookHouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthtorService _authorService;
        public AuthorController(IAuthtorService authorService) 
        { 
            _authorService = authorService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var result = await _authorService.GetAllAuthorsAsync();
            //return Ok(result);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var result = await _authorService.AuthorGetByIDAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("add")]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorAddDTO authorAddDTO)
        {
            var result = await _authorService.AuthorAddAsync(authorAddDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAuthor([FromBody] AuthorUpdateDTO authorUpdateDTO, int id) 
        {
            var result = await _authorService.AuthorUpdateAsync(authorUpdateDTO, id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAuthor(int id) 
        { 
            var result = await _authorService.AuthorDeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
