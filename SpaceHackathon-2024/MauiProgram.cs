using Microsoft.Extensions.Logging;
using SpaceHackathon_2024.Services;
using SpaceHackathon_2024.ViewModels;
using SpaceHackathon_2024.Views;
using Microsoft.Extensions.DependencyInjection;
using Microcharts.Maui;

#if ANDROID
using SpaceHackathon_2024.Platforms.Android;
#endif
namespace SpaceHackathon_2024
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMicrocharts()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("MTSText-Regular.ttf", "MTSTextRegular");
                    fonts.AddFont("MTSText-Bold.ttf", "MTSTextBold");
                }).ConfigureMauiHandlers(handlers => {
                #if ANDROID
                    handlers.AddHandler(typeof(Shell),typeof(CustomShellRenderer));
                #endif
                });

            var services = builder.Services;

            ConfigureServices(services);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // Initialize test data
            InitializeTestData(app.Services);

            return app;
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            // Services configuration
            services.AddHttpClient<AccountService>();

            // Pages configuration
            services.AddSingleton<ApplicationContext>();

            services.AddTransient<NewsViewModel>();
            services.AddTransient<NewsPage>();

            services.AddTransient<SignInPage>();
            services.AddTransient<SignInViewModel>();

            services.AddTransient<SignUpPage>();
            services.AddTransient<SignUpViewModel>();

            services.AddTransient<ChatViewModel>();
            services.AddTransient<ChatPage>();

            services.AddTransient<ProfileViewModel>();
            services.AddTransient<ProfilePage>();

            services.AddTransient<AllNewsPage>();
            services.AddTransient<AllNewsViewModel>();

            services.AddTransient<RatingPage>();
            services.AddTransient<RatingViewModel>();

            services.AddTransient<StorePage>();
            services.AddTransient<StoreViewModel>();
        }

        private static void InitializeTestData(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                context.InitializeTestDataAsync().Wait();
            }
        }
    }
}
