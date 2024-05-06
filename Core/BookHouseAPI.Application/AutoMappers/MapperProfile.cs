using AutoMapper;
using BookHouseAPI.Application.DTOs.AuthorDTOs;
using BookHouseAPI.Application.DTOs.BasketDTOs;
using BookHouseAPI.Application.DTOs.BookDTOs;
using BookHouseAPI.Application.DTOs.GenreDTOs;
using BookHouseAPI.Application.DTOs.ReviewDTOs;
using BookHouseAPI.Application.DTOs.UserDTOs;
using BookHouseAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
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
            //CreateMap<List<Author>, List<AuthorGetDTO>>().ReverseMap();
            //CreateMap<ICollection<Book>, List<BookDTO>>().ReverseMap();

            //CreateMap<List<AuthorGetDTO>, List<Author>>().ReverseMap();
            //CreateMap<List<BookDTO>, ICollection<Book>>().ReverseMap();

            //CreateMap<Author, AuthorGetDTO>()
            //.ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books))
            //.ReverseMap();
            CreateMap<Book, BookGetDTO>().ReverseMap();
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<Basket, BasketGetDTO>().ReverseMap();
            CreateMap<Review, ReviewGetDTO>().ReverseMap();
            CreateMap<AppUser, UserGetDTO>().ReverseMap();
        }
    }
}
