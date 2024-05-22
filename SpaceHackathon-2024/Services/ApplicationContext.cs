using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SpaceHackathon_2024.Models;

namespace SpaceHackathon_2024.Services
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<News> News => Set<News>();

        public DbSet<Event> Events => Set<Event>();

        public DbSet<StoreItem> StoreItems => Set<StoreItem>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile("ApplicationSettings.json")
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .Build();

            optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));
        }
    }
}
