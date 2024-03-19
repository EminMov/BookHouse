using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Domain.Entities
{
    public class Genre : BaseEntity
    {
        public Book Book { get; set; }
        public int BookID { get; set; }
        public string Name { get; set; }
        public string GenreDescription { get; set; }
    }
}
