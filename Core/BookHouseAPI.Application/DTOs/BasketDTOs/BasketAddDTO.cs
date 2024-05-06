using BookHouseAPI.Application.DTOs.BookDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.DTOs.BasketDTOs
{
    public class BasketAddDTO
    {
        public string UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        //public int OrderID { get; set; }
    }
}
