using SpaceHackathon_2024.ViewModels;

namespace SpaceHackathon_2024.Views
{
    public partial class ChatPage : ContentPage
    {
        private ChatViewModel _viewModel;

        public ChatPage(ChatViewModel viewModel)
        {
            InitializeComponent();
            
            BindingContext = _viewModel = viewModel;
        }

        private void SendMessageButton_Clicked(object sender, EventArgs e)
        {
            MessageEntry.Unfocus();
            _viewModel.SendMessageCommand.Execute(null);
        }
    }
}