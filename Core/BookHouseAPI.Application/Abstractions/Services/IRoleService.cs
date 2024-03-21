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
        Task<ResponseModel<object>> GetAllRoles();
        Task<ResponseModel<object>> GetRoleById(string id);
        Task<ResponseModel<bool>> CreateRole(string name);
        Task<ResponseModel<bool>> UpdateRole(string id, string name);
        Task<ResponseModel<bool>> DeleteRole(string id);
    }
}
