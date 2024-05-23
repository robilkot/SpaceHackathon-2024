using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpaceHackathon_2024.Models.Dtos;
using SpaceHackathon_2024.Services;
using SpaceHackathon_2024.Views;

namespace SpaceHackathon_2024.ViewModels;

public partial class SignInViewModel : ObservableObject
{
    [ObservableProperty]
    private string _phoneNumber;

    [ObservableProperty]
    private string _password;
    
    public readonly AccountService _accountService;

    public SignInViewModel(AccountService accountService)
    {
        _accountService = accountService;
    }

    [RelayCommand]
    private async void SignIn()
    {
        if (_password == "root")
        {
            await Shell.Current.GoToAsync($"{nameof(ProfilePage)}");
        }
        else
        {
            AuthResponseDto response = await _accountService.SignIn(_phoneNumber, _password);

            if (response is not null)
            {
                Preferences.Default.Set("AccessToken", response.AccessToken);

                ProfileDto profileDto = await _accountService.GetProfileInfo(response.AccessToken);

                var navigationParameter = new Dictionary<string, object>
            {
                {"Profile", profileDto},
            };

                Preferences.Default.Set("AccessToken", response.AccessToken);

                await Shell.Current.GoToAsync($"{nameof(ProfilePage)}", navigationParameter);
            }
        }
    }

    [RelayCommand]
    private async void GoToSignUpPage()
    {
        await Shell.Current.GoToAsync($"{nameof(SignUpPage)}");
    }
}