﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.DTOs.ShopCartItemDTOs
{
    public class UpdateCartItemDTO
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
    }
}
