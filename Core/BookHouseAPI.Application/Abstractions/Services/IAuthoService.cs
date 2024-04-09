using BookHouseAPI.Application.DTOs.TokenDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Abstractions.Services
{
    public interface IAuthoService
    {
        Task<ResponseModel<TokenDTO>> LoginAsync(string userNameOrEmail, string password);
        Task<ResponseModel<TokenDTO>> LoginWithRefreshTokenAsync(string refreshToken);
        Task<ResponseModel<bool>> LogOut(string userNameOrEmail);

        public Task<ResponseModel<bool>> PasswordResetAsnyc(string email, string currentPas, string newPas);
    }
}
