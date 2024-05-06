using BookHouseAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
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

            modelBuilder.Entity<Book>()
                .HasMany(a => a.Reviews)
                .WithOne(b => b.Book)
                .HasForeignKey(b => b.BookID);

            //modelBuilder.Entity<Basket>()
            //    .HasOne(b => b.Order)
            //    .WithOne(o => o.Basket)
            //    .HasForeignKey<Order>(o => o.BasketId);

            base.OnModelCreating(modelBuilder);

            var guidAdmin = Guid.NewGuid().ToString();
            var guidUser = Guid.NewGuid().ToString();
            var guidAdminCreat = Guid.NewGuid().ToString();
            // Role Seed Data
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole { Id = guidAdmin, Name = "Admin", NormalizedName = "ADMIN" },
                new AppRole { Id = guidUser, Name = "User", NormalizedName = "USER" }
                );

            var hasher = new PasswordHasher<AppUser>();

            var user = new AppUser
            {
                Id = guidAdminCreat,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "default",
                LastName = "default",
                BirthDate = DateTime.UtcNow,
                SecurityStamp = Guid.NewGuid().ToString(),
                //ConcurrencyStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true
            };

            user.PasswordHash = hasher.HashPassword(user, "Admin!23");

            modelBuilder.Entity<AppUser>().HasData(user);

            // User - Role Relationship Seed Data
            modelBuilder.Entity<AppUserRoles>().HasData(
                new AppUserRoles { UserId = guidAdminCreat, RoleId = guidAdmin } // Admin user is assigned the Admin role
            );
        }
    }
}
