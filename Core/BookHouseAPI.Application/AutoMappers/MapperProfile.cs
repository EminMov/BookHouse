using AutoMapper;
using BookHouseAPI.Application.DTOs.AuthorDTOs;
using BookHouseAPI.Application.DTOs.BasketDTOs;
using BookHouseAPI.Application.DTOs.BookDTOs;
using BookHouseAPI.Application.DTOs.GenreDTOs;
using BookHouseAPI.Application.DTOs.ReviewDTOs;
using BookHouseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.AutoMappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        { 
            CreateMap<Author, AuthorGetDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<Basket, BasketGetDTO>().ReverseMap();
            CreateMap<Review, ReviewGetDTO>().ReverseMap();

        }
    }
}
