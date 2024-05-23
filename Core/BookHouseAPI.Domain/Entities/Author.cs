

namespace BookHouseAPI.Domain.Entities
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string? Biography { get; set; } 
        public int BooksCount { get; set; }
        public int SalesCount { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
