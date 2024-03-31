using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.BasketDTOs;
using BookHouseAPI.Application.DTOs.ReviewDTOs;
using BookHouseAPI.Persistance.Implementetions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookHouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetBasketByUser(string id)
        {
            var result = await _basketService.GetAllBasketAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("add-to-basket")]
        public async Task<IActionResult> AddToBasket([FromBody] BasketAddDTO basketAddDTO)
        {
            var result = await _basketService.AddToBasketAsync(basketAddDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("update-basket")]
        public async Task<IActionResult> UpdateBasket([FromBody] BasketUpdateDTO basketUpdateDTO, int id)
        {
            var result = await _basketService.UpdateBasketAsync(basketUpdateDTO, id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> RemoveFromBasket(string userId, int bookId)
        {
            var result = await _basketService.RemoveFromBasketAsync(userId, bookId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
