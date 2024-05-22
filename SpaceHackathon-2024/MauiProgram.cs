using Microsoft.Extensions.Logging;
using SpaceHackathon_2024.Services;
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
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
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
            // Services configuration
            services.AddHttpClient<AccountService>();
            
            // Pages configuration
            services.AddTransient<SignInPage>();
            services.AddTransient<SignInViewModel>();

            services.AddTransient<SignUpPage>();
            services.AddTransient<SignUpViewModel>();
            
            services.AddTransient<ChatViewModel>();
            services.AddTransient<ChatPage>();
            
            services.AddTransient<ProfileViewModel>();
            services.AddTransient<ProfilePage>();
            
        }
    }
}
