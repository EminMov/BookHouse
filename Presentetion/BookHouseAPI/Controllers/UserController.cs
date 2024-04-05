using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(CreateUserDTO newUser)
        {
            var result = await _userService.CreateUserAsync(newUser);
            return StatusCode(result.StatusCode, result);
        }
    }
}
