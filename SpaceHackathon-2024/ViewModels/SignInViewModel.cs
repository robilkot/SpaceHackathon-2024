using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SpaceHackathon_2024.ViewModels;

public partial class SignInViewModel : ObservableObject
{
    [ObservableProperty]
    private string _username;

    [ObservableProperty]
    private string _password;

    [RelayCommand]
    private void SignIn()
    {
        // Здесь можно добавить логику для выполнения входа в систему
        // с использованием Username и Password
    }

    [RelayCommand]
    private async void SignUp()
    {
        await Shell.Current.GoToAsync($"SignUpPage");
    }
}