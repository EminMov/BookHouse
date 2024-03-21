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
        public Task<ResponseModel<List<AuthorGetDTO>>> GetAllAuthors();
        public Task<ResponseModel<AuthorGetDTO>> AuthorGetByID(int Id);
        public Task<ResponseModel<AuthorAddDTO>> AuthorAdd(AuthorAddDTO authorAdd);
        public Task<ResponseModel<bool>> AuthorUpdate(AuthorUpdateDTO authorUpdate, int Id);
        public Task<ResponseModel<bool>> AuthorDelete(int Id);
    }
}
