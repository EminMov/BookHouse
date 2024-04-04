using BookHouseAPI.Application.DTOs.BookDTOs;
using BookHouseAPI.Application.DTOs.GenreDTOs;
using BookHouseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.DTOs.AuthorDTOs
{
    public class AuthorAddDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Biography { get; set; }
        public List<BookDTO> Books { get; set; }
        //public List<GenreAddDTO> Genres { get; set; }
    }
}
