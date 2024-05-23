using SpaceHackathon_2024.ViewModels;

namespace SpaceHackathon_2024.Views;

public partial class ProfilePage : ContentPage
{
    private readonly ProfileViewModel _viewModel;

    public ProfilePage(ProfileViewModel profileViewModel)
    {
        InitializeComponent();

        BindingContext = _viewModel = profileViewModel;

        Shell.SetBackButtonBehavior(this, new() { IsVisible = false });

    }
}