using SpaceHackathon_2024.Views;

namespace SpaceHackathon_2024
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            Routing.RegisterRoute(nameof(AllNewsPage), typeof(AllNewsPage));
            Routing.RegisterRoute(nameof(SearchColleagePage), typeof(SearchColleagePage));
            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(ChatPage), typeof(ChatPage));
            Routing.RegisterRoute(nameof(EventsPage), typeof(EventsPage));
            Routing.RegisterRoute(nameof(RatingPage), typeof(RatingPage));
            Routing.RegisterRoute(nameof(AllChatsPage), typeof(AllChatsPage));
            Routing.RegisterRoute(nameof(NewsPage), typeof(NewsPage));
        }
        
        private async void OnLogoTapped(object sender, EventArgs e)
        {
            if (Shell.Current.CurrentPage is not AllNewsPage)
                await Shell.Current.GoToAsync(nameof(AllNewsPage), true);
        }
    
        private async void OnEnvelopeTapped(object sender, EventArgs e)
        {
            if (Shell.Current.CurrentPage is not AllChatsPage)
                await Shell.Current.GoToAsync(nameof(AllChatsPage));
        }
    }
}
