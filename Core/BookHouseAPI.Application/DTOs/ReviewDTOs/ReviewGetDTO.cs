using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.DTOs.ReviewDTOs
{
    public class ReviewGetDTO
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public string Comment { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public int BookID { get; set; }
    }
}
