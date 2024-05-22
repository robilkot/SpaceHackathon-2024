using Microsoft.Extensions.Logging;
using SpaceHackathon_2024.ViewModels;
using SpaceHackathon_2024.Views;

namespace SpaceHackathon_2024
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("MTSText-Regular.ttf", "MTSTextRegular");
                    fonts.AddFont("MTSText-Bold.ttf", "MTSTextBold");
                });
            
            var services = builder.Services;

            ConfigureServices(services);

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        
        public static void ConfigureServices(IServiceCollection services)
        {
            // Pages configuration
            services.AddTransient<ProfileViewModel>();
            services.AddTransient<ProfilePage>();
        }
    }
}
