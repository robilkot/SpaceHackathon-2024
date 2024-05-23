using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpaceHackathon_2024.Models;
using SpaceHackathon_2024.Services;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace SpaceHackathon_2024.ViewModels;

public partial class ProfileViewModel : ObservableObject
{
    private readonly ApplicationContext _context;

    [ObservableProperty]
    private User currentUser;

    [ObservableProperty]
    private ObservableCollection<Hobby> hobbies;

    public ProfileViewModel(ApplicationContext context)
    {
        _context = context;
        Hobbies = new ObservableCollection<Hobby>();
        LoadUserAsync().ConfigureAwait(false);
    }

    public ProfileViewModel(User user)
    {
        CurrentUser = user;
        Hobbies = new ObservableCollection<Hobby>(user.Hobbies);
    }

    private async Task LoadUserAsync()
    {
        string surname = Preferences.Default.Get("Surname", "no surname");
        
        var userInDb =  await _context.Users.Where(u => u.Surname == surname).FirstAsync();

        if (userInDb is not null)
        {
            UpdateUser(userInDb);
        }
    }

    public void UpdateUser(User user)
    {
        CurrentUser = user;
        Hobbies.Clear();
        foreach (var hobby in user.Hobbies)
        {
            Hobbies.Add(hobby);
        }
    }

    [ObservableProperty]
    private bool _workInfoExpanded;
    
    [ObservableProperty]
    private double _workInfoExpandedRotation = 90;


    [ObservableProperty]
    private bool _ratingExpanded;
    
    [ObservableProperty]
    private double _ratingExpandedRotation = 90;

    [RelayCommand]
    public void WorkInfoHeaderTapped()
    {
        WorkInfoExpanded = !WorkInfoExpanded;
        WorkInfoExpandedRotation = WorkInfoExpanded ? 270 : 90;
    }

    [RelayCommand]
    public void RatingHeaderTapped()
    {
        RatingExpanded = !RatingExpanded;
        RatingExpandedRotation = RatingExpanded ? 270 : 90;
    }
}