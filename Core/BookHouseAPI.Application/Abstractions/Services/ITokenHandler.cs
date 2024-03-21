using BookHouseAPI.Application.DTOs.TokenDTOs;
using BookHouseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Abstractions.Services
{
    public interface ITokenHandler
    {
        public Task<TokenDTO> CreateAccessToken(AppUser user);
        public string CreateRefreshToken();
    }
}
