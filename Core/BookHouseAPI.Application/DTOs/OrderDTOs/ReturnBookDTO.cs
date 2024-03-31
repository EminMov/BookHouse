using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.DTOs.ReturnBookDTOs
{
    public class ReturnBookDTO
    {
        public int BookId { get; set; }
        public string UserId { get; set; }
        public int OrderId { get; set; }
    }
}
