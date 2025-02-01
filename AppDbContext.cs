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
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Event>().ToTable("Events");
        }

        public static void Seed(AppDbContext context)
        {
            // Ensure data base is created
            context.Database.EnsureCreated();

            //Clear existing data
            context.Users.RemoveRange(context.Users);
            context.Events.RemoveRange(context.Events);
            context.SaveChanges();

            // Seed initial data
            var users = new List<User>
            {
                new User
                {
                    Name = "Pai",
                    Color = "black",
                    TextColor = "white"
                },
                new User
                {
                    Name = "Mãe",
                    Color = "red",
                    TextColor = "black"
                },
                new User
                {
                    Name = "Filho",
                    Color = "orange",
                    TextColor = "black"
                },
                new User
                {
                    Name = "Filha",
                    Color = "sky",
                    TextColor = "white"
                }
            };

            var events = new List<Event>
            {
                new Event
                {
                    Title = "Meeting",
                    Start = DateTime.UtcNow,
                    End = DateTime.UtcNow.AddDays(1),
                    AllDay = false,
                    User = users[0]
                },
                new Event
                {
                    Title = "12:00 Lunch",
                    Start = DateTime.UtcNow.AddDays(2),
                    End = DateTime.UtcNow.AddDays(3),
                    AllDay = true,
                    User = users[1]
                },
                new Event
                {
                    Title = "Dinner",
                    Start = DateTime.UtcNow.AddDays(4),
                    End = DateTime.UtcNow.AddDays(5),
                    AllDay = false,
                    User = users[2]
                },
                new Event
                {
                                        Title = "Breakfast",
                    Start = DateTime.UtcNow.AddDays(6),
                    End = DateTime.UtcNow.AddDays(7),
                    AllDay = true,
                    User = users[3]
                },
                new Event
                {
                    Title = "Lunch",
                    Start = DateTime.UtcNow.AddDays(8),
                    End = DateTime.UtcNow.AddDays(9),
                    AllDay = false,
                    User = users[0]
                }
            };

            context.Users.AddRange(users);
            context.Events.AddRange(events);
            context.SaveChanges();
        }
    }
}
