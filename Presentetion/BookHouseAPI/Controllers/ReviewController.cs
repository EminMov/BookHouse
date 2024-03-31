using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.GenreDTOs;
using BookHouseAPI.Application.DTOs.ReviewDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookHouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("get-by-book-id/{id}")]
        public async Task<IActionResult> GetReviewByBookId(int id)
        {
            var result = await _reviewService.GetReviewsByBookIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("add-review")]
        public async Task<IActionResult> AddReview([FromBody] ReviewAddDTO reviewAddDTO)
        {
            var result = await _reviewService.AddReviewAsync(reviewAddDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("update-review")]
        public async Task<IActionResult> UpdateReview([FromBody] ReviewUpdateDTO reviewUpdateDTO, int id)
        {
            var result = await _reviewService.UpdateReviewAsync(id, reviewUpdateDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _reviewService.DeleteReviewAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
