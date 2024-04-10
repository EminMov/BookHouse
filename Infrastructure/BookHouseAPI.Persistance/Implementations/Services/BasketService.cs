using AutoMapper;
using BookHouseAPI.Application.Abstractions.IUnitOfWork;
using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.BasketDTOs;
using BookHouseAPI.Application.DTOs.BookDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using BookHouseAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Persistance.Implementetions.Services
{
    public class BasketService : IBasketService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BasketService(IUnitOfWork unitOfWork, IMapper mapper) 
        { 
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        

        public async Task<ResponseModel<BasketAddDTO>> AddToBasketAsync(BasketAddDTO basketAdd)
        {
            ResponseModel<BasketAddDTO> response = new ResponseModel<BasketAddDTO>();
            Basket basket = new Basket();
            

            var book = await _unitOfWork.GetRepository<Book>().GetByIdAsync(basketAdd.BookId);
            if (book == null)
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Data = null;
                response.Message = "Book not found";
                return response;
            }

            basket.Items.Add(book);
            basket.User.Id = basketAdd.UserId;
            basket.TotalItems += basket.Items.Count;
            basket.BookId = basketAdd.BookId;
            basket.TotalPrice += book.Price * basket.Items.Count;
            basket.ModifyTime = DateTime.Now;

            var addedBasketItem = await _unitOfWork.GetRepository<Basket>().AddAsync(basket);
            var savedData = await _unitOfWork.SaveChangesAsync();

            if (savedData > 0) 
            {
                response.Data = basketAdd;
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "Book added to basket successfully";
            }
            else
            {
                response.Success = false;
                response.StatusCode = 401;
                response.Data = basketAdd;
                response.Message = "Failed to add book to basket";
            }
            return response;
        }

        public async Task<ResponseModel<BasketGetDTO>> GetAllBasketAsync(string userId)
        {
            ResponseModel<BasketGetDTO> response = new ResponseModel<BasketGetDTO>();
            try
            {
                // Получаем корзину пользователя из базы данных
                var userBasket = await _unitOfWork.GetRepository<Basket>()
                                                  .GetAll()
                                                  .Include(b => b.Items)
                                                  .FirstOrDefaultAsync(b => b.User.Id == userId);

                if (userBasket == null)
                {
                    response.Success = false;
                    response.StatusCode = 404; 
                    response.Message = "Basket not found for the user";
                    return response;
                }

                // Создаем объект BasketDTO на основе полученной корзины
                var basketDTO = new BasketGetDTO
                {
                    User = userBasket.User,
                    Items = userBasket.Items.ToList(),
                    TotalItems = userBasket.TotalItems,
                    TotalPrice = userBasket.TotalPrice,
                    ModifyTime = userBasket.ModifyTime,
                    Order = userBasket.Order,
                    OrderID = userBasket.OrderID
                };

                response.Success = true;
                response.StatusCode = 200; 
                response.Data = basketDTO;
                response.Message = "Basket retrieved successfully";

                return response;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("Error: Get All Basket Async");
                Log.Error(ex.Message + ex.InnerException);
                return response;
            }
        }

        public async Task<ResponseModel<bool>> RemoveFromBasketAsync(string userId, int bookId)
        {
            var response = new ResponseModel<bool>();

            try
            {
                var userBasket = await _unitOfWork.GetRepository<Basket>()
                                                  .GetAll()
                                                  .Include(b => b.Items)
                                                  .FirstOrDefaultAsync(b => b.User.Id == userId);

                if (userBasket == null)
                {
                    response.Success = false;
                    response.StatusCode = 404; 
                    response.Message = "Basket not found for the user";
                    return response;
                }

                var bookToRemove = userBasket.Items.FirstOrDefault(item => item.Id == bookId);

                if (bookToRemove == null)
                {
                    response.Success = false;
                    response.StatusCode = 404; 
                    response.Message = "Book not found in the basket";
                    return response;
                }

                userBasket.Items.Remove(bookToRemove);
                userBasket.TotalItems -= 1; 
                userBasket.TotalPrice -= bookToRemove.Price;

                await _unitOfWork.SaveChangesAsync();

                response.Success = true;
                response.StatusCode = 200; 
                response.Data = true;
                response.Message = "Book removed from the basket successfully";

                return response;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("Error: Remove From Basket Async");
                Log.Error(ex.Message + ex.InnerException);
                return response;
            }
        }

        public async Task<ResponseModel<bool>> UpdateBasketAsync(BasketUpdateDTO basketUpdate, int Id)
        {
            var response = new ResponseModel<bool>();

            try
            {
                var userBasket = await _unitOfWork.GetRepository<Basket>()
                                                  .GetAll()
                                                  .Include(b => b.Items)
                                                  .FirstOrDefaultAsync(b => b.User.Id == basketUpdate.UserId);

                if (userBasket == null)
                {
                    response.Success = false;
                    response.StatusCode = 404;
                    response.Message = "Basket not found for the user";
                    return response;
                }

                userBasket.TotalItems = basketUpdate.TotalItems;
                userBasket.TotalPrice = basketUpdate.TotalPrice;
                userBasket.ModifyTime = DateTime.Now;
                await _unitOfWork.SaveChangesAsync();

                response.Success = true;
                response.StatusCode = 200; 
                response.Data = true;
                response.Message = "Basket updated successfully";

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = 500; 
                response.Message = $"An error occurred while updating the basket: {ex.Message}";
                return response;
            }
        }
    }
}
