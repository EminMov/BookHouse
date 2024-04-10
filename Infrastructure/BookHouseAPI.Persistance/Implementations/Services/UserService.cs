using AutoMapper;
using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.UserDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using BookHouseAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
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
        public async Task<ResponseModel<bool>> AssignRoleToUserAsnyc(string userId, string[] roles)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);

            ResponseModel<bool> resModel = new ResponseModel<bool>() { Data = false, StatusCode = 400 };

            try
            {
                if (user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, userRoles);
                    await _userManager.AddToRolesAsync(user, roles);

                    resModel.Data = true;
                    resModel.StatusCode = 200;
                    resModel.Success = true;
                    resModel.Message = "Role assigned successfully";

                    return resModel;
                }
                else
                {
                    return resModel;
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("Error: Assign Role To User");
                Log.Error(ex.Message + ex.InnerException);
                return resModel;
            }
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
            }, 
            newUser.Password);

            response.Data = new CreateUserResponseDTO { Success = result.Succeeded };
            response.StatusCode = result.Succeeded ? 200 : 400;
            response.Success = result.Succeeded;
            response.Message = "Registration completed successfully";

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

        public async Task<ResponseModel<bool>> DeleteUserAsync(string userIdOrName)
        {
            AppUser user = await _userManager.FindByIdAsync(userIdOrName);
            ResponseModel<bool> resModel = new ResponseModel<bool>() { Data = false, StatusCode = 400 };

            if (user == null)
                user = await _userManager.FindByNameAsync(userIdOrName);

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            try
            {
                var data = await _userManager.DeleteAsync(user);
                if (data.Succeeded)
                {
                    resModel.Data = true;
                    resModel.StatusCode = 200;
                    resModel.Success = true;
                    resModel.Message = "User successfully deleted";
                    return resModel;
                }
                else
                {
                    return resModel;
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("Error: Delet User Asyn");
                Log.Error(ex.Message + ex.InnerException);
                return resModel;
            }
        }

        public Task ForgetPasswordAsync(string userId, string refreshToken, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<List<UserGetDTO>>> GetAllUserAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            ResponseModel<List<UserGetDTO>> resModel = new ResponseModel<List<UserGetDTO>>() { Data = null, StatusCode = 400 };

            try
            {
                if (users != null && users.Count > 0)
                {
                    var data = _mapper.Map<List<UserGetDTO>>(users);

                    resModel.Data = data;
                    resModel.StatusCode = 200;
                    resModel.Success = true;
                    resModel.Message = "All users";
                    return resModel;

                }
                else
                {
                    resModel.Data = null;
                    resModel.StatusCode = 400;
                    resModel.Success = false;
                    resModel.Message = "Users not found";
                    return resModel;
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("Error GetAllUser");
                Log.Error(ex.Message + ex.InnerException);
                return resModel;
            }
        }

        public async Task<ResponseModel<string[]>> GetRolesToUserAsync(string userIdOrName)
        {
            ResponseModel<string[]> resModel = new ResponseModel<string[]>() { StatusCode = 400, Data = null };

            AppUser user = await _userManager.FindByIdAsync(userIdOrName);

            if (user == null)
                user = await _userManager.FindByNameAsync(userIdOrName);
            try
            {
                if (user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    resModel.Data = userRoles.ToArray();
                    resModel.StatusCode = 200;
                    resModel.Success = true;
                    resModel.Message = $"User roles {userRoles}"; 
                    return resModel;
                }
                return resModel;
            }
            catch (Exception ex)
            {

                await Console.Out.WriteLineAsync("Error: Delet User Asyn");
                Log.Error(ex.Message + ex.InnerException);
                return resModel;
            }
        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
                resetToken = Encoding.UTF8.GetString(tokenBytes);

                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndTime = accessTokenDate.AddMinutes(10);
                await _userManager.UpdateAsync(user);
            }

        }

        public async Task<ResponseModel<bool>> UpdateUserAsync(UserUpdateDTO updateUser)
        {
            AppUser user = await _userManager.FindByIdAsync(updateUser.UserId);

            ResponseModel<bool> resModel = new ResponseModel<bool>() { Data = false, StatusCode = 400 };

            if (user == null)
                user = await _userManager.FindByNameAsync(updateUser.UserName); //name update elese bele id verecek mecbur onda

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            try
            {
                user.UserName = updateUser.UserName;
                user.BirthDate = updateUser.BirthDate;
                user.Email = updateUser.Email;
                user.FirstName = updateUser.FirstName;
                user.LastName = updateUser.LastName;

                var data = await _userManager.UpdateAsync(user);

                if (data.Succeeded)
                {
                    resModel.Data = true;
                    resModel.StatusCode = 200;
                    resModel.Success = true;
                    resModel.Message = "User successfully updated";

                    return resModel;
                }
                else
                {
                    return resModel;
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("Error: Update User Asyn");
                Log.Error(ex.Message + ex.InnerException);
                return resModel;
            }
        }
    }
}
