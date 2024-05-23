using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.DTOs.BookDTOs
{
    public class BookMostSoldDTO
    {
        public int Id {  get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int GenreId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Rating {  get; set; }
        public int SalesCount { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
