using AutoMapper;
using BookHouseAPI.Application.Abstractions.IUnitOfWork;
using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.AuthorDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using BookHouseAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
            var data = await _unitOfWork.GetRepository<Author>().GetByIdAsync(Id);

            if (data is not null)
            {
                data.Name = authorUpdate.Name;
                data.FirstName = authorUpdate.FirstName;
                data.LastName = authorUpdate.LastName;
                data.Country = authorUpdate.Country;

                try
                {
                    _unitOfWork.GetRepository<Author>().Update(data);
                    await _unitOfWork.SaveChangesAsync();

                    return new ResponseModel<bool>
                    {
                        Success = true,
                        StatusCode = 200,
                        Data = true,
                        Message = "Author info successfully updated"
                    };
                }
                catch
                {
                    return new ResponseModel<bool>
                    {
                        StatusCode = 500,
                        Data = false,
                        Message = "Internal Server Errror"
                    };
                }
            }

            return new ResponseModel<bool>
            {
                StatusCode = 400,
                Data = false,
                Message = "User not found"
            };
        }

        public async Task<ResponseModel<List<AuthorGetDTO>>> GetAllAuthorsAsync()
        {
            var data = _unitOfWork.GetRepository<Author>()
                .GetAll()
                .Include(x => x.BookAuthors)
                .ThenInclude(x => x.Book);

            if (data is not null )
            {
                var get = _mapper.Map<List<AuthorGetDTO>>(data);

                return new ResponseModel<List<AuthorGetDTO>>
                {
                    Data = get,
                    Success = true,
                    StatusCode=200,
                    Message = "All authors"
                };
            }
            else
            {
                return new ResponseModel<List<AuthorGetDTO>>
                {
                    StatusCode = 400,
                    Message = "There are an error with Get All Author"
                };
            }
        }
    }
}
