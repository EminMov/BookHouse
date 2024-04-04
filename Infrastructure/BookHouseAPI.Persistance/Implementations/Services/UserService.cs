using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.UserDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using BookHouseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Persistance.Implementetions.Services
{
    public class UserService : IUserService
    {
        public Task<ResponseModel<bool>> AssignRoleToUserAsync(string userId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<CreateUserResponseDTO>> CreateUserAsync(CreateUserDTO newUser)
        {
            throw new NotImplementedException();
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
