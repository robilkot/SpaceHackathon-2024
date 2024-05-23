using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using SpaceHackathon_2024.Models;
using SpaceHackathon_2024.Services;

namespace SpaceHackathon_2024.ViewModels
{
    public partial class ChatViewModel : ObservableObject
    {
        private HubConnection _hubConnection;

        private readonly ApplicationContext _context;
        
        private const string hostAddr = "192.168.191.95";
        
        private const string _url = "http://10.0.2.2:10010";
    
        private const string _extendedUrl = $"http://{hostAddr}:10010";

        private User AppUser;

        public ObservableCollection<Message> Messages { get; } = new();

        [ObservableProperty]
        private string _newMessage;

        public ICommand SendMessageCommand { get; }

        public ChatViewModel(ApplicationContext context)
        {
            SendMessageCommand = new Command(SendMessageAsync);

            _context = context;
            
            LoadUserAsync().ConfigureAwait(false);;
            
            InitializeSignalR();
        }
        
        private async Task LoadUserAsync()
        {
            string surname = Preferences.Default.Get("Surname", "no surname");
        
            AppUser = await _context.Users.Where(u => u.Surname == surname).FirstAsync();
        }

        private async void InitializeSignalR()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{_url}/chatHub")
                .Build();

            _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                bool isCurrentUser = (user == AppUser.Name);
                Messages.Add(new Message { Author = user, Text = message, IsUserMessage = isCurrentUser });
            });

            try
            {
                await _hubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                // Handle connection errors
            }
        }

        private async void SendMessageAsync()
        {
            if (!string.IsNullOrEmpty(NewMessage))
            {
                await _hubConnection.SendAsync("SendPrivateMessage", AppUser.Name, NewMessage);
                NewMessage = string.Empty;
            }
        }
    }
}
