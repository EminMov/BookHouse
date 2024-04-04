using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.TokenDTOs;
using BookHouseAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Persistance.Implementetions.Services
{
    internal class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;
        readonly UserManager<AppUser> _userManager;

        public TokenHandler(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public Task<TokenDTO> CreateAccessToken(AppUser user)
        {
            throw new NotImplementedException();
        }

        public string CreateRefreshToken()
        {
            throw new NotImplementedException();
        }
    }
}
