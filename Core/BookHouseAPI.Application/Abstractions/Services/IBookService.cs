using BookHouseAPI.Application.DTOs.AuthorDTOs;
using BookHouseAPI.Application.DTOs.BookDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Abstractions.Services
{
    public interface IBookService
    {
        public Task<ResponseModel<BookAddDTO>> BookAdd(BookAddDTO bookAdd);
        public Task<ResponseModel<List<BookGetDTO>>> GetAllBooks();
        public Task<ResponseModel<BookGetDTO>> BookGetByID(int Id);
        public Task<ResponseModel<bool>> BookUpdate(BookUpdateDTO bookUpdate, int Id);
        public Task<ResponseModel<bool>> BookDelete(int Id);
    }
}
