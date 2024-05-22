using SpaceHackathon_2024.ViewModels;

namespace SpaceHackathon_2024.Views;

public partial class SignUpPage : ContentPage
{
    private readonly SignUpViewModel _viewModel;
    
    public SignUpPage(SignUpViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
        AppShell.SetNavBarIsVisible(this, false);
    }
}