﻿using BookHouseAPI.Application.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookHouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthoService _authoService;

        public AuthController(IAuthoService authoService)
        {
            this._authoService = authoService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(string userNameOrEmail = "Ibbbo", string password = "Ibbb!234")
        {

            var data = await _authoService.LoginAsync(userNameOrEmail, password);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPost("refresh-token-login")]
        public async Task<IActionResult> RefreshTokenLogin(string refreshToken)
        {
            var data = await _authoService.LoginWithRefreshTokenAsync(refreshToken);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin,User")]
        public async Task<IActionResult> LogOut(string userNameOrEmail)
        {
            var data = await _authoService.LogOut(userNameOrEmail);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPost("password-reset-token")]
        public async Task<IActionResult> PasswordReset(string email, string currentPas, string newPas)
        {
            var data = await _authoService.PasswordResetAsnyc(email, currentPas, newPas);
            return StatusCode(data.StatusCode, data);
        }
    }
}
