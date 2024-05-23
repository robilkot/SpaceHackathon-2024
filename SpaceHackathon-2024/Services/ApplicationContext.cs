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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sqlitePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"SpaceHakathon");
            Debug.WriteLine(sqlitePath);
            Directory.CreateDirectory(sqlitePath); var fileName = $"{sqlitePath}\fsh.db";
            if (!File.Exists(fileName))
                File.Create(fileName);

            optionsBuilder.UseSqlite($"Data Source={fileName}");
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

        public async Task InitUsersDb()
        {
            var users = new List<User>
            {
                new User { Name = "John", Surame = "Doe", AvatarURL = "https://example.com/avatar1.jpg", Position = "Developer", Department = "Engineering", BranchOffice = "New York" },
                new User { Name = "Alice", Surame = "Smith", AvatarURL = "https://example.com/avatar2.jpg", Position = "Designer", Department = "Design", BranchOffice = "London" },
                new User { Name = "Bob", Surame = "Johnson", AvatarURL = "https://example.com/avatar3.jpg", Position = "Manager", Department = "Management", BranchOffice = "Tokyo" },
                new User { Name = "Emily", Surame = "Williams", AvatarURL = "https://example.com/avatar4.jpg", Position = "Analyst", Department = "Finance", BranchOffice = "Sydney" },
                new User { Name = "Michael", Surame = "Brown", AvatarURL = "https://example.com/avatar5.jpg", Position = "Engineer", Department = "Engineering", BranchOffice = "Berlin" }
            };

            foreach (var user in users)
            {
                await AddUserAsync(user);
            }
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

            await InitUsersDb();

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
