using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.DTOs.BasketDTOs
{
    public class BasketUpdateDTO
    {
        public string UserId { get; set; } 
        public int TotalItems { get; set; } 
        public double TotalPrice { get; set; } 
    }
}
