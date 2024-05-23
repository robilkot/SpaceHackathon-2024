using Microsoft.EntityFrameworkCore;
using SpaceHackathon_2024.Models;
using System.Diagnostics;

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

        public DbSet<Hobby> Hobbies => Set<Hobby>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sqlitePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"SpaceHakathon");
            Debug.WriteLine(sqlitePath);
            Directory.CreateDirectory(sqlitePath); var fileName = $"{sqlitePath}\fsh.db";
            if (!File.Exists(fileName))
                File.Create(fileName);

            optionsBuilder.UseSqlite($"Data Source={fileName}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Hobbies)
                .WithMany(h => h.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserHobby",
                    j => j.HasOne<Hobby>().WithMany().HasForeignKey("HobbyId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId")
                );

            base.OnModelCreating(modelBuilder);
        }

        public async Task AddNewsAsync(News news)
        {
            News.Add(news);
            await SaveChangesAsync();
        }

        public async Task AddUserAsync(User user)
        {
            Users.Add(user);
            await SaveChangesAsync();
        }

        public async Task<List<News>> GetNewsAsync(int pageNumber, int pageSize)
        {
            int skipCount = (pageNumber - 1) * pageSize;
            var newsItems = await News
                .OrderByDescending(n => n.PublishDate)
                .Skip(skipCount)
                .Take(pageSize)
                .ToListAsync();

            return newsItems;
        }

        public async Task AddStoreItemAsync(StoreItem storeItem)
        {
            StoreItems.Add(storeItem);
            await SaveChangesAsync();
        }
        
        public async Task<List<StoreItem>> GetStoreItemsAsync(int pageNumber, int pageSize)
        {
            int skipCount = (pageNumber - 1) * pageSize;

            var storeItems = await StoreItems
                .OrderByDescending(n => n.Name)
                .Skip(skipCount)
                .Take(pageSize)
                .ToListAsync();

            return storeItems;
        }

        public async Task<List<User>?> SearchUserByName(string name)
        {
            var matchingUsers = await Users
                .Where(u => u.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();

            return matchingUsers;
        }

        public async Task<List<User>?> SearchUserByHobby(string hobbyName)
        {
            var matchingUsers = await Users
                .Where(u => u.Hobbies.Any(h => h.Name.ToLower().Contains(hobbyName.ToLower())))
                .ToListAsync();

            return matchingUsers;
        }

        public async Task<User?> GetRandomUserAsync()
        {
            var userCount = await Users.CountAsync();
            if (userCount == 0)
                return null;

            var random = new Random();
            var randomIndex = random.Next(0, userCount);
            var randomUser = await Users
                                .Include(u => u.Hobbies)
                                .Skip(randomIndex)
                                .FirstOrDefaultAsync();

            return randomUser;
        }

        public async Task InitializeTestDataAsync()
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (!News.Any())
            {
                await AddNewsAsync(new News("Test News 1", currentDate, "This is a test news item.", "https://www.mtsbank.ru/upload/static/news/2020/IMG_0744.jpg"));
                await AddNewsAsync(new News("Test News 2", currentDate, "This is a test news item.", "https://www.mtsbank.ru/upload/static/news/2020/IMG_0744.jpg"));
                await AddNewsAsync(new News("Test News 3", currentDate, "This is a test news item.", "https://www.mtsbank.ru/upload/static/news/2020/IMG_0744.jpg"));
                await AddNewsAsync(new News("Test News 4", currentDate, "This is a test news item.", "https://www.mtsbank.ru/upload/static/news/2020/IMG_0744.jpg"));
                await AddNewsAsync(new News("Test News 5", currentDate, "This is a test news item.", "https://www.mtsbank.ru/upload/static/news/2020/IMG_0744.jpg"));
                await AddNewsAsync(new News("Test News 6", currentDate, "This is a test news item.", "https://www.mtsbank.ru/upload/static/news/2020/IMG_0744.jpg"));
                await AddNewsAsync(new News("Test News 7", currentDate, "This is a test news item.", "https://www.mtsbank.ru/upload/static/news/2020/IMG_0744.jpg"));
                await AddNewsAsync(new News("Test News 8", currentDate, "This is a test news item.", "https://www.mtsbank.ru/upload/static/news/2020/IMG_0744.jpg"));
                await AddNewsAsync(new News("Test News 9", currentDate, "This is a test news item.", "https://www.mtsbank.ru/upload/static/news/2020/IMG_0744.jpg"));
            }

            if (!Users.Any())
            {
                await AddUserAsync(new User{ Name = "Никита", Surame = "Калабин", AvatarURL = "", Position = "", Department = "", BranchOffice = ""});
                await AddUserAsync(new User { Name = "John", Surame = "Doe", AvatarURL = "https://example.com/avatar1.jpg", Position = "Developer", Department = "Engineering", BranchOffice = "New York" });
                await AddUserAsync(new User { Name = "Alice", Surame = "Smith", AvatarURL = "https://example.com/avatar2.jpg", Position = "Designer", Department = "Design", BranchOffice = "London" });
            }

            if (!StoreItems.Any())
            {
                await AddStoreItemAsync(new StoreItem { Name = "Test Item 1", Description = "This is a test item.", Cost = 9, ImageURL = " " });
                await AddStoreItemAsync(new StoreItem { Name = "Test Item 2", Description = "This is another test item.", Cost = 19, ImageURL = " " });
            }

            await SaveChangesAsync();
        }

        public async Task ClearAllTablesAsync()
        {
            Users.RemoveRange(Users);
            News.RemoveRange(News);
            Events.RemoveRange(Events);
            StoreItems.RemoveRange(StoreItems);

            await SaveChangesAsync();
        }
    }
}
