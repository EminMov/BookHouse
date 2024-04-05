using BookHouseAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Abstractions.Services
{
    public interface IRoleService
    {
        Task<ResponseModel<object>> GetAllRolesAsync();
        Task<ResponseModel<object>> GetRoleByIdAsync(string id);
        Task<ResponseModel<bool>> CreateRoleAsync(string name);
        Task<ResponseModel<bool>> UpdateRoleAsync(string id, string name);
        Task<ResponseModel<bool>> DeleteRoleAsync(string id);
    }
}
