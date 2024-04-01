using BookHouseAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static BookHouseAPI.Domain.Entities.AppUser;

namespace BookHouseAPI.Persistance.Contexts
{
    public class BookContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public BookContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book>()
                .HasMany(x => x.BookAuthors)
                .WithOne(x => x.Book)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Author>()
                .HasMany(x => x.BookAuthors)
                .WithOne(x => x.Author)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
