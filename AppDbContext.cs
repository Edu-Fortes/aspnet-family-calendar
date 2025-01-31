using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-many relationship between User and Event
            modelBuilder.Entity<User>()
                .HasMany(user => user.Events)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Event>().ToTable("Events");
        }
    }
}
