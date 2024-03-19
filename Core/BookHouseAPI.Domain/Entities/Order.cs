using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Domain.Entities
{
    public class Order : BaseEntity
    {
        public AppUser User { get; set; }
        public DateTime Created { get; set; }
        public string OrderStatus { get; set; }
        public double TotalPrice { get; set; }
        public ICollection<Basket> Basket { get; set; }
    }
}
