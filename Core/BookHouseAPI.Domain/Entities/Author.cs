using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Domain.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        //public int Age { get; set; }
        //public DateTime Birthday { get; set; }
        //public DateTime RipDate { get; set; }
        //public string Gender { get; set; }
        public string Biography { get; set; } 
        public int BooksCount { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
