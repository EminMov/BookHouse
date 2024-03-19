using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public ICollection<Book> Item { get; set; }
        public double ItemPrice { get; set; }
        public double TotalPrice { get; set; }
        public DateTime ModifyTime { get; set; }
        public string BasketStatus { get; set; }
        public Order Order { get; set; }
        public int OrderID { get; set; }
    }
}
