using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using SpaceHackathon_2024.Models;
using SpaceHackathon_2024.Services;

namespace SpaceHackathon_2024.ViewModels
{
    public partial class SearchColleageViewModel : ObservableObject
    {
        private readonly ApplicationContext _context;

        public SearchColleageViewModel(ApplicationContext context)
        {
            _context = context;
            SearchResults = new ObservableCollection<User>();
        }

        public ObservableCollection<User> SearchResults { get; set; }

        public async Task SearchColleageAsync(string name)
        {
            var matchingUsers = await _context.SearchUserByName(name);

            if (matchingUsers == null || matchingUsers.Count == 0)
                return;

            SearchResults.Clear();
            foreach (var user in matchingUsers)
            {
                SearchResults.Add(user);
            }
        }
    }
}