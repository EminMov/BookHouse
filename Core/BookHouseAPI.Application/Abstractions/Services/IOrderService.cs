﻿using BookHouseAPI.Application.DTOs.OrderDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Abstractions.Services
{
    public interface IOrderService
    {
        public Task<ResponseModel<OrderAddDTO>> CreateOrderAsync(OrderAddDTO orderDTO);
        public Task<ResponseModel<List<OrderGetDTO>>> GetOrdersByUserIdAsync(int userId);
    }
}
