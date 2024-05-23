using SpaceHackathon_2024.Views;

namespace SpaceHackathon_2024
{
    public partial class App : Application
    {
        public readonly BackButtonBehavior VisibleBackButtonBehavior = new() { IsVisible = true };
        public readonly BackButtonBehavior InvisibleBackButtonBehavior = new() { IsVisible = false };

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            base.OnStart();
            Shell.Current.GoToAsync(nameof(AllNewsPage));
            return;

            if (!Preferences.Default.ContainsKey("AccessToken") || string.IsNullOrEmpty(Preferences.Default.Get<String>("AccessToken", string.Empty)))
            {
                Shell.Current.GoToAsync(nameof(SignInPage));
            }
        }
    }
}
