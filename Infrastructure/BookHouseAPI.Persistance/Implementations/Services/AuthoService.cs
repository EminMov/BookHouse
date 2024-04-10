using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.TokenDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using BookHouseAPI.Domain.Entities;
using BookHouseAPI.Persistance.Implementetions.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Persistance.Implementations.Services
{
    public class AuthoService : IAuthoService
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;
        readonly IUserService _UserService;


        public AuthoService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenHandler tokenHandler, IUserService UserService2)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            this._tokenHandler = tokenHandler;
            this._UserService = UserService2;
        }
        public async Task<ResponseModel<TokenDTO>> LoginAsync(string userNameOrEmail, string password)
        {
            AppUser user = await _userManager.FindByNameAsync(userNameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(userNameOrEmail);

            if (user == null)
                return new()
                {
                    Data = null,
                    StatusCode = 400,
                    Success = false,
                    Message = "User not found"
                };

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);


            if (result.Succeeded)
            {
                TokenDTO tokenDTO = await _tokenHandler.CreateAccessToken(user);
                await _UserService.UpdateRefreshToken(tokenDTO.RefreshToken, user, tokenDTO.Expiration);
                return new()
                {
                    Success = true,
                    Data = tokenDTO,
                    StatusCode = 200,
                    Message = "OK"
                };
            }
            else
                return new() { Data = null, StatusCode = 401 };

        }

        public async Task<ResponseModel<TokenDTO>> LoginWithRefreshTokenAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(rf => rf.RefreshToken == refreshToken);

            if (user != null && user?.RefreshTokenEndTime > DateTime.UtcNow)
            {
                TokenDTO token = await _tokenHandler.CreateAccessToken(user);
                await _UserService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration);
                return new()
                {
                    Data = token,
                    StatusCode = 200,
                    Message = "Login process successfully completed",
                    Success = true
                };
            }
            else
            {
                return new()
                {
                    Data = null,
                    StatusCode = 401,
                    Success = false,
                    Message = "Refresh token has expired"
                };
            }
        }

        public async Task<ResponseModel<bool>> LogOut(string userNameOrEmail)
        {
            AppUser user = await _userManager.FindByNameAsync(userNameOrEmail);

            if (user == null)
                user = await _userManager.FindByEmailAsync(userNameOrEmail);

            if (user == null)
                return new()
                {
                    Data = false,
                    StatusCode = 400,
                    Success = false,
                    Message = "LogOut could not be completed"
                };

            user.RefreshTokenEndTime = null;
            user.RefreshToken = null;

            var result = await _userManager.UpdateAsync(user);
            await _signInManager.SignOutAsync();

            if (result.Succeeded)
            {
                return new()
                {
                    Data = true,
                    StatusCode = 200,
                    Success = true,
                    Message = "SignOut successfully completed"
                };
            }
            else
            {
                return new()
                {
                    Data = false,
                    StatusCode = 400,
                    Success = false,
                    Message = "SignOut could not be completed"
                };
            }

        }

        public async Task<ResponseModel<bool>> PasswordResetAsnyc(string email, string currentPas, string newPas)
        {
            ResponseModel<bool> response = new() { Data = false, StatusCode = 404 };
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {

                var data = await _userManager.ChangePasswordAsync(user, currentPas, newPas);

                if (data.Succeeded)
                {
                    response.Data = true;
                    response.StatusCode = 200;
                    response.Success = true;
                    response.Message = "Password successfully changed";
                    return response;
                }
            }

            return response;
        }
    }
}
