using BookHouseAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
            .HasMany(a => a.Books) // Author has many Books, specifies the 'many' side of the relationship
            .WithOne(b => b.Author) // Book is associated with one Author, specifies the 'one' side of the relationship
            .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<Genre>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Genre)
                .HasForeignKey(b => b.GenreId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
