using SpaceHackathon_2024.Views;

namespace SpaceHackathon_2024
{
    public partial class AppShell : Shell
    {
        public bool _isOnChatPage = false;

        public AppShell()
        {
            InitializeComponent();
            
            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));
            Routing.RegisterRoute(nameof(NewsPage), typeof(NewsPage));
            Routing.RegisterRoute(nameof(AllNewsPage), typeof(AllNewsPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(ChatPage), typeof(ChatPage));
            Routing.RegisterRoute(nameof(EventsPage), typeof(EventsPage));
        }
        
        private async void OnEnvelopeTapped(object sender, EventArgs e)
        {
            if (Shell.Current.CurrentPage is not ChatPage)
            {
                _isOnChatPage = true;
                await Shell.Current.GoToAsync($"{nameof(ChatPage)}");
            }
            else
                _isOnChatPage = false;
        }
    }
}
