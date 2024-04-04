﻿using BookHouseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.DTOs.BookDTOs
{
    public class BookUpdateDTO
    {
        public double Price { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
