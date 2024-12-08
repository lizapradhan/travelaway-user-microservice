using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace UserMicroservice.Core.API.Models
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }  // Table for users

        public UserDbContext(DbContextOptions<UserDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .Property(c => c.Gender)
            .HasConversion<int>() // Stores enum as string in the database
            .IsRequired();
        }
    }
}
