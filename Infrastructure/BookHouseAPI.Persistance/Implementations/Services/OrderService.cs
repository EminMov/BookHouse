using AutoMapper;
using BookHouseAPI.Application.Abstractions.IUnitOfWork;
using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.OrderDTOs;
using BookHouseAPI.Application.DTOs.ReturnBookDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using BookHouseAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;
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
                //userBasket.Items.Clear();
                //userBasket.TotalItems = 0;
                //userBasket.TotalPrice = 0;
                //userBasket.ModifyTime = DateTime.Now;
                //userBasket.Order = order;
                //userBasket.Order.Id = order.Id;

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
                await Console.Out.WriteLineAsync("Error: Create User Async");
                Log.Error(ex.Message + ex.InnerException);
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
                    // тут есть проблема, не могу передать детали заказа, такие как количество книг, какие именно книги  

                }).ToList();

                response.Success = true;
                response.StatusCode = 200;
                response.Data = ordersDTO;
                response.Message = "Orders retrieved successfully";

                return response;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("Error: Get Order By UserId Async");
                Log.Error(ex.Message + ex.InnerException);
                return response;
            }
        }

        public async Task<ResponseModel<ReturnedBookDTO>> ReturnBookAsync(ReturnBookDTO returnBookDTO)
        {
            var response = new ResponseModel<ReturnedBookDTO>();

            try
            {
                var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(returnBookDTO.OrderId);

                if (order == null || order.User.Id != returnBookDTO.UserId)
                {
                    response.Success = false;
                    response.StatusCode = 404;
                    response.Message = "Order not found or does not belong to the user";
                    return response;
                }

                var bookToRemove = order.Basket.Items.FirstOrDefault(b => b.Id == returnBookDTO.BookId);

                if (bookToRemove == null)
                {
                    response.Success = false;
                    response.StatusCode = 404;
                    response.Message = "Book not found in the order";
                    return response;
                }

                order.Basket.Items.Remove(bookToRemove);
                await _unitOfWork.SaveChangesAsync();

                var returnedBookDTO = new ReturnedBookDTO
                {
                    BookId = bookToRemove.Id,
                    Title = bookToRemove.Title
                };

                response.Success = true;
                response.StatusCode = 200;
                response.Data = returnedBookDTO;
                response.Message = "Book returned successfully";

                return response;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("Error: Return Book Async");
                Log.Error(ex.Message + ex.InnerException);
                return response;
            }
        }
    }
}
