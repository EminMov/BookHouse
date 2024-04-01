using BookHouseAPI.Domain.Entities.ManyToMany;

namespace BookHouseAPI.Domain.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string? Biography { get; set; } 
        public int BooksCount { get; set; }
        public ICollection<BookAuthors> BookAuthors { get; set; }
    }
}
