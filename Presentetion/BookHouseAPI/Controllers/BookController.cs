using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.AuthorDTOs;
using BookHouseAPI.Application.DTOs.BookDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookHouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _authorService;
        public BookController(IBookService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _authorService.GetAllBooks();
            //return Ok(result);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var result = await _authorService.BookGetByID(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBook([FromBody] BookAddDTO bookAddDTO)
        {
            var result = await _authorService.BookAdd(bookAddDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateBook([FromBody] BookUpdateDTO bookUpdateDTO, int id)
        {
            var result = await _authorService.BookUpdate(bookUpdateDTO, id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _authorService.BookDelete(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
