using SpaceHackathon_2024.ViewModels;

namespace SpaceHackathon_2024.Views;

public partial class SignInPage : ContentPage
{
    private readonly SignInViewModel _viewModel;
    
    public SignInPage(SignInViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
        
        AppShell.SetNavBarIsVisible(this, false);
    }
}