namespace BookHouseAPI.Domain.Entities.ManyToMany
{
    public class BookAuthors
    {
        public int Id { get; set; }
        public Book? Book { get; set; }
        public Author? Author { get; set; }
    }
}
