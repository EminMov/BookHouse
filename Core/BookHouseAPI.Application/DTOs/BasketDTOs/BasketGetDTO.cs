using BookHouseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.DTOs.BasketDTOs
{
    public class BasketGetDTO
    {
        public ICollection<Book> Items { get; set; }
        public int BookId { get; set; }
        public int TotalItems { get; set; }
        public List<Book> ItemPrices { get; set; }
        public double TotalPrice { get; set; }
        public DateTime ModifyTime { get; set; }
        public int OrderID { get; set; }

        //entitde olan naviqation prop yazmaga ehtiyaz yoxdu
    }
}
