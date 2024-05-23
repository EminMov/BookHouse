using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.DTOs.AuthorDTOs
{
    public class AuthorGetMostPopDTO
    {
        public string FirstName { get; set; }
        public int SalesCount { get; set; }
    }
}
