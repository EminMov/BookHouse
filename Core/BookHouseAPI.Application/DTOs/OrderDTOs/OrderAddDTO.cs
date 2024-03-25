using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.DTOs.OrderDTOs
{
    public class OrderAddDTO
    {
        public string UserId { get; set; }
        public double TotalPrice { get; set; }
        public DateTime Created { get; set; }
    }
}
