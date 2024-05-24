using Microsoft.EntityFrameworkCore;
using SpaceHackathon_2024.Models;
using SpaceHackathon_2024.Models.Enums;
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

        public IEnumerable<ScheduleDay> GetScheduleDays(DateOnly beginDate, DateOnly endDate)
        {
            var scheduleDays = ScheduleDays
                .Where(sd => sd.Date >= beginDate && sd.Date <= endDate)
                .ToList();

            return scheduleDays;
        }

        public async Task GenerateWeeklySchedulesAsync()
        {
            // Получаем текущую дату и определяем день недели (понедельник - первый день недели)
            DateTime today = DateTime.Now.Date;
            int daysUntilMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
            var startDate = DateOnly.FromDateTime(today.AddDays(-((int)today.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7));

            // Генерируем расписание на 4 недели
            for (int i = 0; i < 4; i++)
            {
                // Рассчитываем начальный день для текущей недели
                var currentWeekStartDate = startDate.AddDays(i * 7);
                var endDate = currentWeekStartDate.AddDays(6); // Неделя состоит из 7 дней

                // Создаем список дней расписания для текущей недели
                for (int j = 0; j < 7; j++)
                {
                    var currentDate = currentWeekStartDate.AddDays(j);
                    var dayType = GetDayType(currentDate, j, i);
                    var scheduleDay = new ScheduleDay
                    {
                        Date = currentDate,
                        Type = dayType,
                        ShiftBegin = default(DateTime), // Здесь нужно заполнить начальное и конечное время смены в соответствии с вашими требованиями
                        ShiftEnd = default(DateTime), // Здесь нужно заполнить начальное и конечное время смены в соответствии с вашими требованиями
                        Description = GetDayDescription(dayType)
                    };
                    ScheduleDays.Add(scheduleDay);
                }
            }
        }



        private DayTypes GetDayType(DateOnly date, int j, int i)
        {
            // Примерная логика выбора типа дня (может быть уточнена)
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return DayTypes.Weekend;
            }
            else if (i == 1 && j == 3)
            {
                return DayTypes.Holiday;
            }
            else if (i == 2 && j == 5)
            {
                return DayTypes.Holiday;
            }
            else if (i == 1 && j == 2)
            {
                return DayTypes.Vacation;
            }
            else
            {
                return DayTypes.Working;
            }
        }

        private string GetDayDescription(DayTypes dayType)
        {
            // Здесь можно задать описание для каждого типа дня (например, выходной, праздник, рабочий и т.д.)
            switch (dayType)
            {
                case DayTypes.Working:
                    return "";
                case DayTypes.Weekend:
                    return "";
                case DayTypes.Vacation:
                    return "Вас заменят на рабочем месте";
                case DayTypes.Holiday:
                    return "";
                default:
                    return string.Empty;
            }
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

            await AddUserAsync(new User { KPI = 91d, Name = "Nikita", Surname = "Kalabin", AvatarURL = "https://distribution.faceit-cdn.net/images/f9b1e39c-bab2-49e6-8018-7ca56c33986b.jpeg", BranchOffice = "Департамент Пиццы", Department = "Гикало 9, Минск", Position = "Программист", FullName = "Никита Калабин" });
            await AddUserAsync(new User { KPI = 93d, Name = "Nikita", Surname = "Kharashun", AvatarURL = "https://fikiwiki.com/uploads/posts/2022-02/1645019125_27-fikiwiki-com-p-kartinki-dlya-stima-na-avu-28.jpg", BranchOffice = "На галере", Department = "Заушановский пер. 12, Минск", Position = "Программист", FullName = "Никита Хорошун" });
            await AddUserAsync(new User { KPI = 90d, Name = "Egor", Surname = "Gokov", AvatarURL = "https://masterpiecer-images.s3.yandex.net/dc54f7f494f211ee8d7f7a2f0d1382ba:upscaled", BranchOffice = "Офис", Department = "Белинского 54, Минск", Position = "Программист", FullName = "Егор Гоков" });
            await AddUserAsync(new User { KPI = 110d, Name = "Timur", Surname = "Robilko", AvatarURL = "https://ru-static.z-dn.net/files/d3b/3cc3216ecef61bd569d2250340f708a0.png", BranchOffice = "Дома :)", Department = "Секрет, Минск", Position = "Программист", FullName = "Тимур Робилко" });


            if (!StoreItems.Any())
            {
                await AddStoreItemAsync(new StoreItem { Name = "Test Item 1", Description = "This is a test item.", Cost = 9, ImageURL = " " });
                await AddStoreItemAsync(new StoreItem { Name = "Test Item 2", Description = "This is another test item.", Cost = 19, ImageURL = " " });
            }

            if (!ScheduleDays.Any())
            {
                await GenerateWeeklySchedulesAsync();
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
