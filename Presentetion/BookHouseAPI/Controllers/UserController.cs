using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.UserDTOs;
using BookHouseAPI.Persistance.Implementetions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookHouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(CreateUserDTO newUser)
        {
            var result = await _userService.CreateUserAsync(newUser);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("assign-role-to-user")]
        public async Task<IActionResult> AssignRoleToUser(string userId, string[] roles)
        {
            var data = await _userService.AssignRoleToUserAsnyc(userId, roles);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var data = await _userService.GetAllUserAsync();
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("get-roles-to-user/{id}")]
        public async Task<IActionResult> GetRolesToUser(string id)
        {
            var data = await _userService.GetRolesToUserAsync(id);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(UserUpdateDTO model)
        {
            var data = await _userService.UpdateUserAsync(model);
            return StatusCode(data.StatusCode, data);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteToUser(string UserIdOrName)
        {
            var data = await _userService.DeleteUserAsync(UserIdOrName);
            return StatusCode(data.StatusCode, data);
        }
    }
}
