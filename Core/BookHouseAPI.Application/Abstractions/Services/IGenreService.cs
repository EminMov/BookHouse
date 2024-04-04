using BookHouseAPI.Application.DTOs.AuthorDTOs;
using BookHouseAPI.Application.DTOs.GenreDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Abstractions.Services
{
    public interface IGenreService
    {
        public Task<ResponseModel<List<GenreDTO>>> GetAllGenres();
        public Task<ResponseModel<GenreDTO>> GenreGetByID(int Id);
        public Task<ResponseModel<GenreDTO>> GenreAdd(GenreDTO genreAdd);
        public Task<ResponseModel<bool>> GenreUpdate(GenreDTO genreUpdate, int Id);
        public Task<ResponseModel<bool>> GenreDelete(int Id);
    }
}
