using SpaceHackathon_2024.ViewModels;

namespace SpaceHackathon_2024.Views
{
    public partial class ChatPage : ContentPage
    {
        private ChatViewModel _viewModel;

        public ChatPage()
        {
            InitializeComponent();
            _viewModel = new ChatViewModel();
            BindingContext = _viewModel;

            AppShell.SetBackButtonBehavior(this, new() { IsVisible = false });
        }

        private void SendMessageButton_Clicked(object sender, EventArgs e)
        {
            _viewModel.SendMessageCommand.Execute(null);
            MessageEntry.Unfocus();
            //MessagesCollectionView.ScrollTo(viewModel.Messages.Count - 1, -1, ScrollToPosition.End, true);
        }
    }
}