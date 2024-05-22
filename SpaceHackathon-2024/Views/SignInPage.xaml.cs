using SpaceHackathon_2024.ViewModels;

namespace SpaceHackathon_2024.Views;

public partial class SignInPage : ContentPage
{
    private readonly SignInViewModel _viewModel;
    
    public SignInPage()
    {
        InitializeComponent();
        _viewModel = new SignInViewModel();
        BindingContext = _viewModel;
    }
}