using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.DTOs.ReviewDTOs
{
    public class ReviewAddDTO
    {
        public Guid UserId { get; set; }
        public int BookId { get; set; }
        public int Grade { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
    }
}
