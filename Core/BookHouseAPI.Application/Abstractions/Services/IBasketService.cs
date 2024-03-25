using BookHouseAPI.Application.DTOs.BasketDTOs;
using BookHouseAPI.Application.DTOs.BookDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Abstractions.Services
{
    public interface IBasketService
    {
        public Task<ResponseModel<BasketAddDTO>> AddToBasketAsync(BasketAddDTO basketDTO);
        public Task<ResponseModel<bool>> RemoveFromBasketAsync(string userId, int bookId);
        public Task<ResponseModel<bool>> UpdateBasketAsync(BasketUpdateDTO basketUpdate, int Id);
        public Task<ResponseModel<BasketGetDTO>> GetAllBasketAsync(string userId);
    }
}
