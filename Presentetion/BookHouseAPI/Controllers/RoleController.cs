using BookHouseAPI.Application.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookHouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole(string role)
        {
            var result = await _roleService.CreateRoleAsync(role);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("delete-role")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _roleService.GetAllRolesAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var result = await _roleService.GetRoleByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateRole(string id, string name)
        {
            var result = await _roleService.UpdateRoleAsync(id, name);
            return StatusCode(result.StatusCode, result);
        }
    }
}
