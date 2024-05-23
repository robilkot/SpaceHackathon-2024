using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.SignalR.Client;
using SpaceHackathon_2024.Models;

namespace SpaceHackathon_2024.ViewModels
{
    public partial class ChatViewModel : ObservableObject
    {
        private HubConnection _hubConnection;
        
        private const string hostAddr = "192.168.191.95";
        
        private const string _url = "http://10.0.2.2:5040";
    
        private const string _extendedUrl = $"http://{hostAddr}:5040";

        public ObservableCollection<Message> Messages { get; } = new();

        [ObservableProperty]
        private string _newMessage;

        public ICommand SendMessageCommand { get; }

        public ChatViewModel()
        {
            SendMessageCommand = new Command(SendMessageAsync);

            InitializeSignalR();
        }

        private async void InitializeSignalR()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{_extendedUrl}/chatHub")
                .Build();

            _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                bool isCurrentUser = (user == "я");
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
                await _hubConnection.SendAsync("SendPrivateMessage", "я", NewMessage);
                NewMessage = string.Empty;
            }
        }
    }
}
