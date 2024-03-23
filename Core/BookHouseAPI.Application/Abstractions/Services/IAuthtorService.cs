using BookHouseAPI.Application.DTOs.AuthorDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Abstractions.Services
{
    public interface IAuthtorService
    {
        public Task<ResponseModel<List<AuthorGetDTO>>> GetAllAuthorsAsync();
        public Task<ResponseModel<AuthorGetDTO>> AuthorGetByIDAsync(int Id);
        public Task<ResponseModel<AuthorAddDTO>> AuthorAddAsync(AuthorAddDTO authorAdd);
        public Task<ResponseModel<bool>> AuthorUpdateAsync(AuthorUpdateDTO authorUpdate, int Id);
        public Task<ResponseModel<bool>> AuthorDeleteAsync(int Id);
    }
}
