using AutoMapper;
using BookHouseAPI.Application.Abstractions.IUnitOfWork;
using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.AuthorDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using BookHouseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Persistance.Implementetions.Services
{
    public class AuthorService : IAuthtorService
    {
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseModel<bool>> AuthorDeleteAsync(int Id)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            var data = await _unitOfWork.GetRepository<Author>().GetByIdAsync(Id);
            var result = _unitOfWork.GetRepository<Author>().Remove(data);
            var rawAffected = await _unitOfWork.SaveChangesAsync();
            if(rawAffected > 0)
            {
                response.Success = true;
                response.StatusCode = 200;
                response.Data = true;
                response.Message = "Author successfully deleted";
            }
            else
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Data = false;
                response.Message = "There are an error with save changes";
            }
            return response;
        }

        public async Task<ResponseModel<AuthorGetDTO>> AuthorGetByIDAsync(int Id)
        {
            ResponseModel<AuthorGetDTO> response = new ResponseModel<AuthorGetDTO>();
            var data = await _unitOfWork.GetRepository<Author>().GetByIdAsync(Id);
            if(data != null)
            {
                var get = _mapper.Map<AuthorGetDTO>(data);
                response.Success = true;
                response.StatusCode = 200;
                response.Data = get;
                response.Message = "Here is the author whose ID you indicated";
            }
            else
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Data = null;
                response.Message = "Using this ID, the author was not found";
            }
            return response;
        }

        public async Task<ResponseModel<AuthorAddDTO>> AuthorAddAsync(AuthorAddDTO authorAdd)
        {
            ResponseModel<AuthorAddDTO> response = new ResponseModel<AuthorAddDTO>();
            Author author = new Author();

            author.Name = authorAdd.Name;
            author.FirstName = authorAdd.FirstName;
            author.LastName = authorAdd.LastName;
            author.Country = authorAdd.Country;

            var data = _unitOfWork.GetRepository<Author>().AddAsync(author);
            var savedata = await _unitOfWork.SaveChangesAsync();

            if (savedata > 0)
            {
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "Author successfully added";
                response.Data = authorAdd;
            }
            else
            {
                response.Success = false;
                response.StatusCode = 401;
                response.Data = authorAdd;
                response.Message = "There are an error with save changes";
            }
            return response;
        }

        public async Task<ResponseModel<bool>> AuthorUpdateAsync(AuthorUpdateDTO authorUpdate, int Id)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            var data = await _unitOfWork.GetRepository<Author>().GetByIdAsync(Id);

            if (data != null)
            {
                data.Name = authorUpdate.Name;
                data.FirstName = authorUpdate.FirstName;
                data.LastName = authorUpdate.LastName;

                await _unitOfWork.GetRepository<Author>().AddAsync(data);
                var rawAffected = await _unitOfWork.SaveChangesAsync();
                if (rawAffected > 0)
                {
                    response.Success = true;
                    response.StatusCode = 200;
                    response.Data = true;
                    response.Message = "Author info successfully updated";
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
                response.StatusCode = 404;
                response.Data = false;
                response.Message = "Using this ID, the author was not found";
            }

            return response;
        }

        public async Task<ResponseModel<List<AuthorGetDTO>>> GetAllAuthorsAsync()
        {
            ResponseModel<List<AuthorGetDTO>> response = new ResponseModel<List<AuthorGetDTO>>();
            var data = _unitOfWork.GetRepository<Author>().GetAll();
            if (data != null )
            {
                var get = _mapper.Map<List<AuthorGetDTO>>(data);
                response.Data = get;
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "All authors";
            }
            else
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Data = null;
                response.Message = "There are an error with Get All Author";
            }
            return response;
        }
    }
}
