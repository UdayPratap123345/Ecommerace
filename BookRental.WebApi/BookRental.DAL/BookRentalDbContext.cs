using BookRental.Model;
using Microsoft.EntityFrameworkCore;

namespace BookRental.DAL
{
    public class BookRentalDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        public BookRentalDbContext(DbContextOptions<BookRentalDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships, constraints, etc. here if needed

            // Example: Configuring a relationship between User and Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict); // Modify the delete behavior as needed

            // Add other configurations for your models' relationships

            modelBuilder.Entity<Book>().HasData(
        new Book { BookId = 1, BookName = "Book 1", Rating = 4.5, IsBookAvailable = true, Genre = "Fiction", Description = "Description 1", LentByUserId = 1, CoverImage = "https://www.ingramspark.com/hs-fs/hubfs/TheSumofAllThings_cover_June21_option4(1).jpg?width=1125&name=TheSumofAllThings_cover_June21_option4(1).jpg" },
        new Book { BookId = 2, BookName = "Book 2", Rating = 3.8, IsBookAvailable = false, Genre = "Non-fiction", Description = "Description 2", LentByUserId = 2, CoverImage = "https://www.creativindiecovers.com/wp-content/uploads/2012/02/9780718155209.jpg" }
        // Add more initial book data as needed
    );

            // Seed initial data for Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Admin" },
                new Role { RoleId = 2, RoleName = "User" }
            // Add more initial role data as needed
            );

            // Seed initial data for Users
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, RoleId = 2, Token_Available = 10, UserName = "Aashu", Email = "aashu@example.com", Password = "password" },
                new User { UserId = 2, RoleId = 2, Token_Available = 5, UserName = "Uday", Email = "uday@example.com", Password = "password" }
            // Add more initial user data as needed
            );

            // Configure relationships if required

            base.OnModelCreating(modelBuilder);
        }
    }
}
