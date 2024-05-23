using Microsoft.EntityFrameworkCore;
using SpaceHackathon_2024.Models;
using System.Diagnostics;

namespace SpaceHackathon_2024.Services
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<News> News => Set<News>();

        public DbSet<Event> Events => Set<Event>();

        public DbSet<StoreItem> StoreItems => Set<StoreItem>();

        public DbSet<Hobby> Hobbies => Set<Hobby>();

        public DbSet<ScheduleDay> ScheduleDays => Set<ScheduleDay>();

        public DbSet<WeeklySchedule> WeeklySchedules=> Set<WeeklySchedule>();

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

        public async Task<List<User>> GetTopUsersByKPIAsync(int count)
        {
            var topUsers = await Users
                .OrderByDescending(u => u.KPI)
                .Take(count)
                .ToListAsync();

            return topUsers;
        }
        public async Task<double> CalculateUserRatingAsync(double yourKPI)
        {
            var usersWithHigherOrEqualKPI = await Users
                .Where(u => u.KPI >= yourKPI)
                .CountAsync();

            var totalUsers = await Users
                .CountAsync();

            double rating = (double)usersWithHigherOrEqualKPI / totalUsers * 100;

            return rating;
        }

        public async Task<double?> GetKPIBySurnameAsync(string surname)
        {
            var user = await Users.FirstOrDefaultAsync(u => u.Surname == surname);
            return user?.KPI;
        }

        public IEnumerable<ScheduleDay> GetScheduleDays(DateTime beginDate, DateTime endDate)
        {
            var scheduleDays = ScheduleDays
                .Where(sd => sd.Date >= beginDate && sd.Date <= endDate)
                .ToList();

            return scheduleDays;
        }
        public async Task InitializeTestDataAsync()
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            if (!News.Any())
            {
                await AddNewsAsync(new News("Space Hakathon 2024!", "2024/05/22", "Трёхдневный конкурс инновационных решений", "https://optim.tildacdn.biz/tild3232-6366-4137-a334-333639306433/-/format/webp/10-2.jpeg"));
                await AddNewsAsync(new News("Новая линейка корпоративных планов!", "2024/05/18", "Бизнес S/M/L", "https://www.mts.by/upload/resize_cache/webp/iblock/b2e/d5lqk9luyh22jasilx5l26mcvdx5v9gi/1380_414_1/sml_tablet_1.webp"));
                await AddNewsAsync(new News("МТС Опрос", "2024/04/28", "Сервис для проведения маркетинговых исследований.", "https://www.mts.by/upload/resize_cache/webp/iblock/897/zu0fy5uupu20782rysewl0fvns48t6iu/Tablet-Resolution.webp"));
                await AddNewsAsync(new News("МТС Коммуникатор", currentDate, "SMS и Viber рассылки.", "https://www.mts.by/upload/resize_cache/webp/iblock/c69/1380_414_1/kommunikator_1200x463.webp"));
                await AddNewsAsync(new News("Test News 5", currentDate, "This is a test news item.", "https://www.mtsbank.ru/upload/static/news/2020/IMG_0744.jpg"));
                await AddNewsAsync(new News("Test News 6", currentDate, "This is a test news item.", "https://www.mtsbank.ru/upload/static/news/2020/IMG_0744.jpg"));
            }

            await AddUserAsync(new User { Name = "Nikita", Surname = "Kalabin", AvatarURL = "data:image/jpeg;base64,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", BranchOffice = "", Department = "Гикало 9, Минск", Position = "Программист", FullName = "Никита Калабин" });
            await AddUserAsync(new User { Name = "Nikita", Surname = "Kharashun", AvatarURL = "data:image/jpeg;base64,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", BranchOffice = "", Department = "Гикало 9, Минск", Position = "Программист", FullName = "Никита Калабин" });
            await AddUserAsync(new User { Name = "Egor", Surname = "Gokov", AvatarURL = "https://masterpiecer-images.s3.yandex.net/dc54f7f494f211ee8d7f7a2f0d1382ba:upscaled", BranchOffice = "Офис", Department = "Гикало 9, Минск", Position = "Программист", FullName = "Егор Гоков" });
            await AddUserAsync(new User { Name = "Timur", Surname = "Robilko", AvatarURL = "data:/jpeg;base64,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", BranchOffice = "", Department = "Гикало 9, Минск", Position = "Программист", FullName = "Никита Калабин" });


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