using AutoMapper;
using BookHouseAPI.Application.Abstractions.IUnitOfWork;
using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.OrderDTOs;
using BookHouseAPI.Application.DTOs.ReturnedBookDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using BookHouseAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Persistance.Implementetions.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper) 
        { 
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseModel<OrderAddDTO>> CreateOrderAsync(OrderAddDTO orderDTO)
        {
            var response = new ResponseModel<OrderAddDTO>();
            Order order = new Order();

            try
            {
                var userBasket = await _unitOfWork.GetRepository<Basket>()
                                                  .GetAll()
                                                  .Where(b => b.User.Id == orderDTO.UserId)
                                                  .Include(b => b.Items)
                                                  .FirstOrDefaultAsync();

                if (userBasket == null || !userBasket.Items.Any())
                {
                    response.Success = false;
                    response.StatusCode = 400;
                    response.Message = "Basket is empty or not found";
                    return response;
                }

                order.TotalPrice = userBasket.TotalPrice;
                order.Created = DateTime.Now;
                order.User = userBasket.User;

                // Переносим элементы корзины в заказ
                order.Basket = userBasket;
                order.Basket.Id = userBasket.Id;

                // Очищаем корзину после создания заказа
                userBasket.Items.Clear();
                userBasket.TotalItems = 0;
                userBasket.TotalPrice = 0;
                userBasket.ModifyTime = DateTime.Now;
                userBasket.Order = order;
                userBasket.Order.Id = order.Id;

                var addedOrder = await _unitOfWork.GetRepository<Order>().AddAsync(order);
                await _unitOfWork.SaveChangesAsync();

                response.Success = true;
                response.StatusCode = 200;
                response.Data = orderDTO;
                response.Message = "Order created successfully";

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = 500;
                response.Message = $"An error occurred while creating the order: {ex.Message}";
                return response;
            }
        }

        public async Task<ResponseModel<List<OrderGetDTO>>> GetOrdersByUserIdAsync(string userId)
        {
            var response = new ResponseModel<List<OrderGetDTO>>();

            try
            {
                var orders = await _unitOfWork.GetRepository<Order>()
                                              .GetAll()
                                              .Where(o => o.User.Id == userId)
                                              .ToListAsync();

                var ordersDTO = orders.Select(order => new OrderGetDTO
                {
                    UserName = order.User.FirstName,
                    UserId = order.User.Id,
                    TotalPrice = order.TotalPrice,
                    Created = order.Created
                    // тут есть проблема, не могу передать детали заказа, так как 

                }).ToList();

                response.Success = true;
                response.StatusCode = 200;
                response.Data = ordersDTO;
                response.Message = "Orders retrieved successfully";

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = 500;
                response.Message = $"An error occurred while retrieving orders: {ex.Message}";
                return response;
            }
        }

        public Task<ResponseModel<ReturnedBookDTO>> ReturnBookAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
