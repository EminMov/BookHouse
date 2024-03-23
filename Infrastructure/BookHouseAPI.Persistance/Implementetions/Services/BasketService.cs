using AutoMapper;
using BookHouseAPI.Application.Abstractions.IUnitOfWork;
using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.BasketDTOs;
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
                    // Если корзина не найдена, возвращаем сообщение об ошибке
                    response.Success = false;
                    response.StatusCode = 404; // Not Found
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

                // Устанавливаем успешный результат и возвращаем BasketDTO
                response.Success = true;
                response.StatusCode = 200; // OK
                response.Data = basketDTO;
                response.Message = "Basket retrieved successfully";

                return response;
            }
            catch (Exception ex)
            {
                // Если произошла ошибка, возвращаем сообщение об ошибке
                response.Success = false;
                response.StatusCode = 500; // Internal Server Error
                response.Message = $"An error occurred while retrieving the basket: {ex.Message}";
                return response;
            }
        }

        public Task<ResponseModel<bool>> RemoveFromBasketAsync(int userId, int basketId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> UpdateBasketAsync(BasketUpdateDTO basketUpdate, int Id)
        {
            throw new NotImplementedException();
        }
    }
}
