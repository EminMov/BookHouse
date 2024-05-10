using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Domain.Entities
{
    public class ShopCart
    {
        public int ShopCartId { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public List<ShopCartItem> Items { get; set; }
    }
}
