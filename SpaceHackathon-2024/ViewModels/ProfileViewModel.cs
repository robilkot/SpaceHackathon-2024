using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpaceHackathon_2024.Models;
using SpaceHackathon_2024.Services;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace SpaceHackathon_2024.ViewModels;

[QueryProperty(nameof(ShowBackButton), "ShowBackButton")]
[QueryProperty(nameof(UserSurname), "UserSurname")]
public partial class ProfileViewModel : ObservableObject
{
    private readonly ApplicationContext _context;

    [ObservableProperty]
    private bool _showBackButton = false;

    public string UserSurname
    {
        set
        {
            LoadUserAsync(value);
        }
    }

    [ObservableProperty]
    private User _currentUser;

    [ObservableProperty]
    private ObservableCollection<Hobby> hobbies;
    [ObservableProperty]
    private IList<KeyValueItem> _kpiItems;

    [ObservableProperty]
    private IList<KeyValueItem> _contactItems;

    [ObservableProperty]
    private IList<KeyValueItem> _personalItems;
    public ProfileViewModel(ApplicationContext context)
    {
        _context = context;

        Hobbies = new ObservableCollection<Hobby>();

        LoadUserAsync();

        KpiItems = new ObservableCollection<KeyValueItem>() { 
            new KeyValueItem("KPI за поледний месяц", CurrentUser.KPI.ToString()),
            new KeyValueItem("KPI за последнии 6 месяцев", CurrentUser.KPI.ToString()),
            new KeyValueItem("Позиция", CurrentUser.Position),
        };

        ContactItems = new ObservableCollection<KeyValueItem>() {
            new KeyValueItem("Email", CurrentUser.Email),
            new KeyValueItem("Мобильный телефон", CurrentUser.Phone.ToString()),
            new KeyValueItem("Telegram", CurrentUser.Telegram.ToString()),
            new KeyValueItem("Адрес офиса", CurrentUser.BranchOffice.ToString()),
        };

        PersonalItems = new ObservableCollection<KeyValueItem>() {
            new KeyValueItem("Имя", CurrentUser.Name.ToString()),
            new KeyValueItem("Фамилия", CurrentUser.Surname.ToString()),
            new KeyValueItem("Отчество", CurrentUser.ThirdName.ToString()),
            new KeyValueItem("Пол", CurrentUser.Gender.ToString()),
            new KeyValueItem("Дата рождения", CurrentUser.DateOfBirth.ToString()),
        };
    }

    public ProfileViewModel(User user)
    {
        CurrentUser = user;

        Hobbies = new ObservableCollection<Hobby>(user.Hobbies);
    }

    private async void LoadUserAsync(string? surname = null)
    {
        surname ??= Preferences.Default.Get("Surname", "no surname");
        
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

    [ObservableProperty]
    private bool _contactInfoExpanded;
    [ObservableProperty]
    private double _contactInfoExpandedRotation = 90;

    [RelayCommand]
    public void ContactInfoHeaderTapped()
    {
        ContactInfoExpanded = !ContactInfoExpanded;
        ContactInfoExpandedRotation = ContactInfoExpanded ? 270 : 90;
    }
}

