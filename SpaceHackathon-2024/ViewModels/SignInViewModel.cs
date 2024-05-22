using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SpaceHackathon_2024.ViewModels;

public partial class SignInViewModel : ObservableObject
{
    [ObservableProperty]
    private string _phoneNumber;

    [ObservableProperty]
    private string _password;

    [RelayCommand]
    private void SignIn()
    {
        
    }

    [RelayCommand]
    private async void GoToSignUpPage()
    {
        await Shell.Current.GoToAsync($"SignUpPage");
    }
}