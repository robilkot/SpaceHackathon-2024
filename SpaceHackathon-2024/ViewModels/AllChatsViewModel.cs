using CommunityToolkit.Mvvm.ComponentModel;
using SpaceHackathon_2024.Models;
using SpaceHackathon_2024.Services;
using System.Collections.ObjectModel;

namespace SpaceHackathon_2024.ViewModels
{
    public class AllChatsViewModel : ObservableObject
    {
        public ObservableCollection<User> ActiveChats { get; set; }

        private ApplicationContext _context;

        public AllChatsViewModel(ApplicationContext context)
        {
            _context = context;

            ActiveChats = new ObservableCollection<User>(_context.Users);
        }
    }
}
