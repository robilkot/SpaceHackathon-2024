using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpaceHackathon_2024.Models;
using SpaceHackathon_2024.Services;
using SpaceHackathon_2024.Views;
using System.Collections.ObjectModel;

namespace SpaceHackathon_2024.ViewModels
{
    public partial class AllChatsViewModel : ObservableObject
    {
        public ObservableCollection<User> ActiveChats { get; set; }

        private ApplicationContext _context;

        public AllChatsViewModel(ApplicationContext context)
        {
            _context = context;

            ActiveChats = new ObservableCollection<User>(_context.Users);
        }

        [RelayCommand]
        public async Task OpenChat(User user)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                {"ShowBackButton", true },
                {"TargetUser", user }
            };

            await Shell.Current.GoToAsync(nameof(ChatPage), true, navigationParameter);
        }
    }
}
