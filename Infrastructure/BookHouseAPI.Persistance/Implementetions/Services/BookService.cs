using AutoMapper;
using BookHouseAPI.Application.Abstractions.IUnitOfWork;
using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.AuthorDTOs;
using BookHouseAPI.Application.DTOs.BookDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using BookHouseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Persistance.Implementetions.Services
{
    public class BookService : IBookService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BookService(IUnitOfWork unitOfWork, IMapper mapper) 
        { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseModel<BookAddDTO>> BookAdd(BookAddDTO bookAdd)
        {
            ResponseModel<BookAddDTO> response = new ResponseModel<BookAddDTO>();
            Book book = new Book();

            book.Authors = bookAdd.Authors;
            book.ISBN = bookAdd.ISBN;
            book.NumberOfPages = bookAdd.NumberOfPages;
            book.Description = bookAdd.Description;
            book.ReleaseDate = bookAdd.ReleaseDate;
            book.Price = bookAdd.Price;
            book.Title = bookAdd.Title;
            book.Language = bookAdd.Language;
            book.Genres = bookAdd.Genres;

            var data = _unitOfWork.GetRepository<Book>().AddAsync(book);
            var savedata = await _unitOfWork.SaveChangesAsync();

            if (savedata > 0)
            {
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "Book successfully added";
                response.Data = bookAdd;
            }
            else
            {
                response.Success = false;
                response.StatusCode = 401;
                response.Data = bookAdd;
                response.Message = "There are an error with save changes";
            }
            return response;
        }

        public async Task<ResponseModel<bool>> BookDelete(int Id)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            var data = await _unitOfWork.GetRepository<Book>().GetByIdAsync(Id);
            var result = _unitOfWork.GetRepository<Book>().Remove(data);
            var rawAffected = await _unitOfWork.SaveChangesAsync();

            if (rawAffected > 0)
            {
                response.Success = true;
                response.StatusCode = 200;
                response.Data = true;
                response.Message = "Book successfully deleted";
            }
            else
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Data = false;
                response.Message = "There are an error with Save Changes";
            }
            return response;
        }

        public async Task<ResponseModel<BookGetDTO>> BookGetByID(int Id)
        {
            ResponseModel<BookGetDTO> response = new ResponseModel<BookGetDTO>();
            var data = await _unitOfWork.GetRepository<Book>().GetByIdAsync(Id);
            if (data != null)
            {
                var get = _mapper.Map<BookGetDTO>(data);
                response.Success = true;
                response.StatusCode = 200;
                response.Data = get;
                response.Message = "Here is the book you are looking for";
            }
            else
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Data = null;
                response.Message = $"Could not found book with id = {Id}";
            }
            return response;
        }

        public async Task<ResponseModel<bool>> BookUpdate(BookUpdateDTO bookUpdate, int Id)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            var data = await _unitOfWork.GetRepository<Book>().GetByIdAsync(Id);

            if (data != null)
            {
                data.Price = bookUpdate.Price;
                data.Authors = bookUpdate.Authors;

                await _unitOfWork.GetRepository<Book>().AddAsync(data);
                var rawAffected = await _unitOfWork.SaveChangesAsync();
                if (rawAffected > 0)
                {
                    response.Success = true;
                    response.StatusCode = 200;
                    response.Data = true;
                    response.Message = "Book info successfully updated";
                }
                else
                {
                    response.Success = false;
                    response.StatusCode = 400;
                    response.Data = false;
                    response.Message = "There are ana error in Save Changes";
                }
            }
            else
            {
                response.Success = false;
                response.StatusCode = 401;
                response.Data = false;
                response.Message = "Using this ID, the book was not found";
            }

            return response;
        }

        public async Task<ResponseModel<List<BookGetDTO>>> GetAllBooks()
        {
            ResponseModel<List<BookGetDTO>> response = new ResponseModel<List<BookGetDTO>>();
            var data = _unitOfWork.GetRepository<Book>().GetAll();
            if (data != null)
            {
                var get = _mapper.Map<List<BookGetDTO>>(data);
                response.Data = get;
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "All books";
            }
            else
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Data = null;
                response.Message = "There are an error with Get All Books";
            }
            return response;
        }
    }
}
