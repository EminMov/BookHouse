using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public int Id { get; set; }
        public AppUser User { get; set; } 
        public string UserId { get; set; } 
        public ICollection<Book> Items { get; set; }
        public int BookId { get; set; }
        public int TotalItems { get; set; }
        public double TotalPrice { get; set; }
        public DateTime ModifyTime { get; set; }
        //public Order Order { get; set; }
        //public int OrderID { get; set; }
    }
}
