using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Domain.Entities
{
    public class AppUser : IdentityUser<string>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndTime { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Order> Orders { get; set; }

        public class AppRole : IdentityRole<string>
        {

        }

        public class AppUserRoles : IdentityUserRole<string>
        {

        }
    }
}
