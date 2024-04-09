using BookHouseAPI.Application.DTOs.UserDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using BookHouseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate);
        public Task ForgetPasswordAsync(string userId, string refreshToken, string newPassword);
        public Task<ResponseModel<bool>> AssignRoleToUserAsnyc(string userId, string[] roles);
        public Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        public Task<ResponseModel<List<UserGetDTO>>> GetAllUserAsync();
        public Task<ResponseModel<CreateUserResponseDTO>> CreateUserAsync(CreateUserDTO newUser);
        public Task<ResponseModel<bool>> UpdateUserAsync(UserUpdateDTO updateUser);
        public Task<ResponseModel<bool>> DeleteUserAsync(string userIdOrName);
        public Task<ResponseModel<string[]>> GetRolesToUserAsync(string userIdOrName);
    }
}
