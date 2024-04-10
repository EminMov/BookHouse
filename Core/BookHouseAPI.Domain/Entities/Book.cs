

namespace BookHouseAPI.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public double Price { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public int ReviewId { get; set; }
        public Author Author { get; set; }
        public Genre Genre { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
