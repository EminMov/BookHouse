using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
    }
}
