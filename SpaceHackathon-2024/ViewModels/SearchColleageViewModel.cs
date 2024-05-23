using CommunityToolkit.Mvvm.ComponentModel;
using SpaceHackathon_2024.Models.Dtos;
using SpaceHackathon_2024.Services;

namespace SpaceHackathon_2024.ViewModels;

public partial class SearchColleageViewModel : ObservableObject
{
    private readonly AccountService _accountService;

    public SearchColleageViewModel(AccountService accountService)
    {
        _accountService = accountService;
    }

    public Task<List<UserDto>> SearchColleage(string name)
    {
        return _accountService.SearchColleage(name);
    }
}