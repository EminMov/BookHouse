using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.GenreDTOs;
using BookHouseAPI.Application.DTOs.ReviewDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookHouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("get-by-book-id")]
        public async Task<IActionResult> GetReviewByBookId([FromQuery] int id)
        {
            var result = await _reviewService.GetReviewsByBookIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewAddDTO reviewAddDTO)
        {
            var result = await _reviewService.AddReviewAsync(reviewAddDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReview([FromBody] ReviewUpdateDTO reviewUpdateDTO, int id)
        {
            var result = await _reviewService.UpdateReviewAsync(id, reviewUpdateDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteReview([FromQuery] int id)
        {
            var result = await _reviewService.DeleteReviewAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
