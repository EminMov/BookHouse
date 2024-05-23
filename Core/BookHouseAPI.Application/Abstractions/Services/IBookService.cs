using BookHouseAPI.Application.DTOs.BookDTOs;
using BookHouseAPI.Application.Models.ResponseModels;

namespace BookHouseAPI.Application.Abstractions.Services
{
    public interface IBookService
    {
        public Task<ResponseModel<List<BookGetDTO>>> GetAllBooks();
        public Task<ResponseModel<BookGetDTO>> BookGetByID(int Id);
        public Task<ResponseModel<bool>> BookUpdate(BookUpdateDTO bookUpdate, int Id);
        public Task<ResponseModel<bool>> BookDelete(int bookId);
        public Task<ResponseModel<BookMostSoldDTO>> GetMostSoldBookAsync();
    }
}
