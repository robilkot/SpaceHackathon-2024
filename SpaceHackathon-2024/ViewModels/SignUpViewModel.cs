using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SpaceHackathon_2024.ViewModels;

public partial class SignUpViewModel : ObservableObject
{
    [ObservableProperty]
    private string username;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private string confirmPassword;

    [RelayCommand]
    private void SignUp()
    {
        // Здесь можно добавить логику для выполнения регистрации
        // с использованием Username, Email, Password и ConfirmPassword
    }

    [RelayCommand]
    private async void SignIn()
    {
        await Shell.Current.GoToAsync("..", animate:true);
    }
}