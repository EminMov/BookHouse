using BookHouseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.DTOs.BookDTOs
{
    public class BookGetDTO
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int NumberOfPages { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string Language { get; set; }
        public double Price { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<Basket> Baskets { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
