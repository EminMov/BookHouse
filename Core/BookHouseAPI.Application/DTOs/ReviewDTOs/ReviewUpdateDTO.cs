﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.DTOs.ReviewDTOs
{
    public class ReviewUpdateDTO
    {
        public int Grade { get; set; }
        public string Comment { get; set; }
    }
}
