using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.Models.ResponseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookHouseAPI.Domain.Entities.AppUser;

namespace BookHouseAPI.Persistance.Implementetions.Services
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<AppRole> _roleManager;
        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<ResponseModel<bool>> CreateRoleAsync(string name)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>();

            IdentityResult identityResult = await _roleManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = name
            });

            if (identityResult.Succeeded)
            {
                responseModel.Data = identityResult.Succeeded;
                responseModel.StatusCode = 200;
                responseModel.Success = true;
                responseModel.Message = "Role successfully created";
                return responseModel;
            }
            else
            {
                responseModel.Data = false;
                responseModel.StatusCode = 400;
                responseModel.Success = false;
                responseModel.Message = "Such a role exist";
                return responseModel;
            }
        }

        public async Task<ResponseModel<bool>> DeleteRoleAsync(string id)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>();
            var data = await _roleManager.FindByIdAsync(id);
            var result = await _roleManager.DeleteAsync(data);
            if (result.Succeeded)
            {
                responseModel.Data = result.Succeeded;
                responseModel.StatusCode = 200;
                responseModel.Success = true;
                responseModel.Message = "Role successfully deleted";
                return responseModel;
            }
            else
            {
                responseModel.Data = false;
                responseModel.StatusCode = 400;
                responseModel.Success = false;
                responseModel.Message = "Role found but cant be deleted";
                return responseModel;
            }
        }

        public async Task<ResponseModel<object>> GetAllRolesAsync()
        {
            ResponseModel<object> responseModel = new();
            var data = await _roleManager.Roles.ToListAsync();
            if (data != null)
            {
                responseModel.Data = data;
                responseModel.StatusCode = 200;
                responseModel.Success = true;
                responseModel.Message = "All roles";
                return responseModel;
            }
            else
            {
                responseModel.Data = false;
                responseModel.StatusCode = 404;
                responseModel.Success = false;
                responseModel.Message = "Roles not found";
                return responseModel;
            }
        }

        public async Task<ResponseModel<object>> GetRoleByIdAsync(string id)
        {
            ResponseModel<object> responseModel = new();
            var data = await _roleManager.FindByIdAsync(id);
            if (data != null)
            {
                responseModel.Data = data;
                responseModel.StatusCode = 200;
                responseModel.Success = true;
                responseModel.Message = $"Role by this id={id}";
                return responseModel;
            }
            else
            {
                responseModel.Data = false;
                responseModel.StatusCode = 400;
                responseModel.Success = false;
                responseModel.Message = $"Role by id={id} not found";
                return responseModel;
            }
        }

        public async Task<ResponseModel<bool>> UpdateRoleAsync(string id, string name)
        {
            ResponseModel<bool> responseModel = new()
            {
                Data = false,
                StatusCode = 404,
                Success = false,
                Message = ""
            };
            var data = await _roleManager.FindByIdAsync(id);
            if (data == null)
            {
                return responseModel;
            }
            data.Name = name;
            var result = await _roleManager.UpdateAsync(data);
            if (result.Succeeded)
            {
                responseModel.Data = true;
                responseModel.StatusCode = 200;
                responseModel.Success = true;
                responseModel.Message = "Role successfully updated";
            }
            return responseModel;
        }
    }
}
