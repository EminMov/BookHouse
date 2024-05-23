using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.BasketDTOs;
using BookHouseAPI.Application.DTOs.ReviewDTOs;
using BookHouseAPI.Persistance.Implementetions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetBasketByUser([FromQuery] string id)
        {
            Log.Error("Error in GetById method");//bu zaten default yazir tarixi
            var result = await _basketService.GetAllBasketAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("add-to-basket")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> AddToBasket([FromBody] BasketAddDTO basketAddDTO)
        {
            
            var result = await _basketService.AddToBasketAsync(basketAddDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdateBasket([FromBody] BasketUpdateDTO basketUpdateDTO, int id)
        {
            var result = await _basketService.UpdateBasketAsync(basketUpdateDTO, id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> RemoveFromBasket([FromQuery] string userId, int bookId)
        {
            var result = await _basketService.RemoveFromBasketAsync(userId, bookId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
