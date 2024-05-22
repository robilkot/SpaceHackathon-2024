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
                .WithUrl("http://10.0.2.2:5040/chatHub")
                .Build();

            _hubConnection.On<string>("Receive", (message) =>
            {
                Messages.Add(new Message { Text = message, IsUserMessage = false });
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
                await _hubConnection.SendAsync("Send", NewMessage);
                Messages.Add(new Message {Author  = null, Text = NewMessage, IsUserMessage = true });
                NewMessage = string.Empty;
            }
        }
    }
}