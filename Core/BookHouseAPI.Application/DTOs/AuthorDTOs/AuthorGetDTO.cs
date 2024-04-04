using BookHouseAPI.Application.DTOs.BookDTOs;
using BookHouseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.DTOs.AuthorDTOs
{
    public class AuthorGetDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Biography { get; set; }
        public int BooksCount { get; set; }
        public ICollection<BookDTO> Books { get; set; }
    }
}
