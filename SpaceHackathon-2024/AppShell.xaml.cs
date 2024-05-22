using SpaceHackathon_2024.Views;

namespace SpaceHackathon_2024
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            Routing.RegisterRoute("SignUpPage", typeof(SignUpPage));
            Routing.RegisterRoute("SignInPage", typeof(SignInPage));
            Routing.RegisterRoute("ProfilePage", typeof(ProfilePage));
            Routing.RegisterRoute("ChatPage", typeof(ChatPage));
        }
        
        private async void OnEnvelopeTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"ChatPage");
        }
    }
}
