using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Domain.Entities
{
    public class Genre : BaseEntity
    {
        public ICollection<Book> Books { get; set; }
        public string Name { get; set; }
        public string GenreDescription { get; set; }
    }
}
