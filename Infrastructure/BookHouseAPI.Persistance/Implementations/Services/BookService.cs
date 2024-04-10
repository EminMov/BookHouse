﻿using AutoMapper;
using BookHouseAPI.Application.Abstractions.IUnitOfWork;
using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.BookDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using BookHouseAPI.Domain.Entities;

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

        public async Task<ResponseModel<BookDTO>> BookGetByID(int Id)
        {
            ResponseModel<BookDTO> response = new ResponseModel<BookDTO>();
            var data = await _unitOfWork.GetRepository<Book>().GetByIdAsync(Id);
            if (data != null)
            {
                var get = _mapper.Map<BookDTO>(data);
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
                data.Description = bookUpdate.Description;
                data.ReleaseDate = bookUpdate.ReleaseDate;

                _unitOfWork.GetRepository<Book>().Update(data);
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

        public async Task<ResponseModel<List<BookDTO>>> GetAllBooks()
        {
            ResponseModel<List<BookDTO>> response = new ResponseModel<List<BookDTO>>();
            var data = _unitOfWork.GetRepository<Book>().GetAll();
            if (data != null)
            {
                var get = _mapper.Map<List<BookDTO>>(data);
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
