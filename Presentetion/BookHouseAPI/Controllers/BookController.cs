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
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _bookService.GetAllBooks();
            //return Ok(result);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetBookById([FromQuery] int id)
        {
            var result = await _bookService.BookGetByID(id);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateBook([FromBody] BookUpdateDTO bookUpdateDTO, int id)
        {
            var result = await _bookService.BookUpdate(bookUpdateDTO, id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBook([FromQuery] int id)
        {
            var result = await _bookService.BookDelete(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
