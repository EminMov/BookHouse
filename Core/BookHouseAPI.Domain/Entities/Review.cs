using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Domain.Entities
{
    public class Review : BaseEntity
    {
        public int Grade { get; set; }
        public string Comment { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public int BookID { get; set; }
        public Book Book { get; set; }
        public AppUser User { get; set; }
    }
}
