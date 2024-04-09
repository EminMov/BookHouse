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
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService) 
        { 
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var result = await _authorService.GetAllAuthorsAsync();
            //return Ok(result);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetAuthorById([FromQuery] int id)
        {
            var result = await _authorService.AuthorGetByIDAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorAddDTO authorAddDTO)
        {
            var result = await _authorService.AuthorAddAsync(authorAddDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAuthor([FromBody] AuthorUpdateDTO authorUpdateDTO, int id) 
        {
            var result = await _authorService.AuthorUpdateAsync(authorUpdateDTO, id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("/{id}")]
        public async Task<IActionResult> DeleteAuthor([FromQuery] int id) 
        { 
            var result = await _authorService.AuthorDeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
