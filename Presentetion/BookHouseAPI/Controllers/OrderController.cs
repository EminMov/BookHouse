﻿using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.OrderDTOs;
using BookHouseAPI.Application.DTOs.ReturnBookDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookHouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("get-order-by-user-id")]
        public async Task<IActionResult> GetOrdersByUserIdAsync(string userId)
        {
            var result = await _orderService.GetOrdersByUserIdAsync(userId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrderAsync(OrderAddDTO orderDTO)
        {
            var result = await _orderService.CreateOrderAsync(orderDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("return-book")]
        public async Task<IActionResult> ReturnBookAsync(ReturnBookDTO returnBookDTO)
        {
            var result = await _orderService.ReturnBookAsync(returnBookDTO);
            return StatusCode(result.StatusCode, result);
        }
    }
}
