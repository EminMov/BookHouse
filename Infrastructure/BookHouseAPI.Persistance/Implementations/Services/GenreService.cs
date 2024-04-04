using AutoMapper;
using BookHouseAPI.Application.Abstractions.IUnitOfWork;
using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.AuthorDTOs;
using BookHouseAPI.Application.DTOs.GenreDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using BookHouseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Persistance.Implementetions.Services
{
    public class GenreService : IGenreService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GenreService(IUnitOfWork unitOfWork, IMapper mapper) 
        { 
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseModel<GenreDTO>> GenreAdd(GenreDTO genreAdd)
        {
            ResponseModel<GenreDTO> response = new ResponseModel<GenreDTO>();
            Genre genre = new Genre();

            genre.Name = genreAdd.Name;
            genre.GenreDescription = genreAdd.GenreDescription;

            var data = _unitOfWork.GetRepository<Genre>().AddAsync(genre);
            var savedata = await _unitOfWork.SaveChangesAsync();

            if (savedata > 0)
            {
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "Genre successfully added";
                response.Data = genreAdd;
            }
            else
            {
                response.Success = false;
                response.StatusCode = 401;
                response.Data = genreAdd;
                response.Message = "There are an error with save changes";
            }
            return response;
        }

        public async Task<ResponseModel<bool>> GenreDelete(int Id)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            var data = await _unitOfWork.GetRepository<Genre>().GetByIdAsync(Id);
            var result = _unitOfWork.GetRepository<Genre>().Remove(data);
            var rowAffected = await _unitOfWork.SaveChangesAsync();
            if (rowAffected > 0)
            {
                response.Success = true;
                response.StatusCode = 200;
                response.Data = true;
                response.Message = "Genre successfully deleted";
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

        public async Task<ResponseModel<GenreDTO>> GenreGetByID(int Id)
        {
            ResponseModel<GenreDTO> response = new ResponseModel<GenreDTO>();
            var data = await _unitOfWork.GetRepository<Genre>().GetByIdAsync(Id);
            if (data != null)
            {
                var get = _mapper.Map<GenreDTO>(data);
                response.Success = true;
                response.StatusCode = 200;
                response.Data = get;
                response.Message = "Here is the genre with ID you indicated";
            }
            else
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Data = null;
                response.Message = "Using this ID, the genre was not found";
            }
            return response;
        }

        public async Task<ResponseModel<bool>> GenreUpdate(GenreDTO genreUpdate, int Id)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            var data = await _unitOfWork.GetRepository<Genre>().GetByIdAsync(Id);

            if (data != null)
            {
                data.Name = genreUpdate.Name;
                data.GenreDescription = genreUpdate.GenreDescription;

                await _unitOfWork.GetRepository<Genre>().AddAsync(data);
                var rawAffected = await _unitOfWork.SaveChangesAsync();
                if (rawAffected > 0)
                {
                    response.Success = true;
                    response.StatusCode = 200;
                    response.Data = true;
                    response.Message = "Genre info successfully updated";
                }
                else
                {
                    response.Success = false;
                    response.StatusCode = 400;
                    response.Data = false;
                    response.Message = "There are an error in Save Changes";
                }
            }
            else
            {
                response.Success = false;
                response.StatusCode = 401;
                response.Data = false;
                response.Message = "Using this ID, the genre was not found";
            }

            return response;
        }

        public async Task<ResponseModel<List<GenreDTO>>> GetAllGenres()
        {
            ResponseModel<List<GenreDTO>> response = new ResponseModel<List<GenreDTO>>();
            var data = _unitOfWork.GetRepository<Genre>().GetAll();
            if (data != null)
            {
                var get = _mapper.Map<List<GenreDTO>>(data);
                response.Data = get;
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "All genres";
            }
            else
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Data = null;
                response.Message = "There are an error with Get All Genres";
            }
            return response;
        }
    }
}
