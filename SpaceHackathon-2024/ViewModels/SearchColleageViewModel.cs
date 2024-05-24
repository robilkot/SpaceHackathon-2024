using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpaceHackathon_2024.Models;
using SpaceHackathon_2024.Services;

namespace SpaceHackathon_2024.ViewModels
{
    public partial class SearchColleageViewModel : ObservableObject
    {
        private readonly ApplicationContext _context;

        [ObservableProperty]
        private bool _searchSettingsExpanded = false;

        public SearchColleageViewModel(ApplicationContext context)
        {
            _context = context;
            SearchResults = new ObservableCollection<User>();
        }

        public ObservableCollection<User> SearchResults { get; set; }

        public async Task SearchColleageAsync(string name)
        {
            var matchingUsers = await _context.SearchUserByName(name);

            if (matchingUsers == null)
                return;

            SearchResults.Clear();
            foreach (var user in matchingUsers)
            {
                SearchResults.Add(user);
            }
        }

        [RelayCommand]
        public void ToggleExpander()
        {
            SearchSettingsExpanded = !SearchSettingsExpanded;
        }
    }
}