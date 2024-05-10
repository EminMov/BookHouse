using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.AuthorDTOs;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var result = await _authorService.GetAllAuthorsAsync();
            //return Ok(result);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("get-by-id")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAuthorById([FromQuery] int id)
        {
            var result = await _authorService.AuthorGetByIDAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorAddDTO authorAddDTO)
        {
            var result = await _authorService.AuthorAddAsync(authorAddDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAuthor([FromBody] AuthorUpdateDTO authorUpdateDTO, int id) 
        {
            var result = await _authorService.AuthorUpdateAsync(authorUpdateDTO, id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id) 
        { 
            var result = await _authorService.AuthorDeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
