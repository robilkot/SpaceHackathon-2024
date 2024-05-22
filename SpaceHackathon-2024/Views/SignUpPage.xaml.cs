using SpaceHackathon_2024.ViewModels;

namespace SpaceHackathon_2024.Views;

public partial class SignUpPage : ContentPage
{
    private readonly SignUpViewModel _viewModel;
    
    public SignUpPage()
    {
        InitializeComponent();
        _viewModel = new SignUpViewModel();
        BindingContext = _viewModel;
    }
}