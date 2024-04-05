using AutoMapper;
using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.UserDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using BookHouseAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Persistance.Implementetions.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        private IMapper _mapper;
        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public Task<ResponseModel<bool>> AssignRoleToUserAsync(string userId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<CreateUserResponseDTO>> CreateUserAsync(CreateUserDTO newUser)
        {
            var response = new ResponseModel<CreateUserResponseDTO>();

            var id = Guid.NewGuid().ToString();

            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = id,
                UserName = newUser.UserName,
                Email = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
            }, newUser.Password);

            response.Data = new CreateUserResponseDTO { Success = result.Succeeded };
            response.StatusCode = result.Succeeded ? 200 : 400;

            if (!result.Succeeded)
            {
                response.Data.Message = string.Join(" \n ", result.Errors.Select(error => $"{error.Code} - {error.Description}"));
            }

            //burdan sorasi default olaraq rol vermekdi usere, bunu admin ile de eletdirmek olar ya da yri method icinde.

            AppUser user = await _userManager.FindByNameAsync(newUser.UserName);
            if (user == null)
                user = await _userManager.FindByEmailAsync(newUser.Email);
            if (user != null)
                await _userManager.AddToRoleAsync(user, "User");

            return response;
        }

        public Task<ResponseModel<bool>> DeleteUserAsync(string UserIdOrName)
        {
            throw new NotImplementedException();
        }

        public Task ForgetPasswordAsync(string userId, string refreshToken, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<UserGetDTO>>> GetAllUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<string[]>> GetRolesToUserAsync(string UserIdOrName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> UpdateUserAsync(UserUpdateDTO updateUser)
        {
            throw new NotImplementedException();
        }
    }
}
