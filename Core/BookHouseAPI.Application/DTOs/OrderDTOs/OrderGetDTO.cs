using BookHouseAPI.Application.DTOs.BasketDTOs;
using BookHouseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.DTOs.OrderDTOs
{
    public class OrderGetDTO
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
        public double TotalPrice { get; set; }
    }
}
