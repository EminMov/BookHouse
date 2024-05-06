using BookHouseAPI.Application.DTOs.GenreDTOs;
using BookHouseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.DTOs.BookDTOs
{
    public class BookDTO
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int GenreId { get; set; }
    }
}
