using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpaceHackathon_2024.Models.Dtos;
using SpaceHackathon_2024.Services;
using SpaceHackathon_2024.Views;

namespace SpaceHackathon_2024.ViewModels;

public partial class SignUpViewModel : ObservableObject
{
    [ObservableProperty]
    private string _username;

    [ObservableProperty]
    private string _phoneNumber;

    [ObservableProperty]
    private string _password;

    [ObservableProperty]
    private string confirmPassword;
    
    public readonly AccountService _accountService;

    public SignUpViewModel(AccountService accountService)
    {
        _accountService = accountService;
    }

    [RelayCommand]
    private async void SignUp()
    {
        if (confirmPassword != _password)
        {
            return;
        }
        
        AuthResponseDto response = await _accountService.
            SignUp(_phoneNumber, _password, _username);

        if (response != null)
        {
            Preferences.Default.Set("AccessToken", response.AccessToken);

            ProfileDto profileDto = await _accountService.GetProfileInfo(response.AccessToken);
            
            var navigationParameter = new Dictionary<string, object>
            {
                {"Profile", profileDto},
            };

            await Shell.Current.GoToAsync(nameof(AllNewsPage));

            //await Shell.Current.GoToAsync($"{nameof(ProfilePage)}", navigationParameter);
        }
    }

    [RelayCommand]
    private async void GoToSignInPage()
    {
        await Shell.Current.GoToAsync("..", false);
    }
}