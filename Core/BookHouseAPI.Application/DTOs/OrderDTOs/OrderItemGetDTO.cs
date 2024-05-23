using BookHouseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.DTOs.OrderDTOs
{
    public class OrderItemGetDTO
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
    }
}
