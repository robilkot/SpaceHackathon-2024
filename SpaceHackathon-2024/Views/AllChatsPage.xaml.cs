using SpaceHackathon_2024.Models;
using SpaceHackathon_2024.ViewModels;

namespace SpaceHackathon_2024.Views;

public partial class AllChatsPage : ContentPage
{
	public AllChatsPage(AllChatsViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
	}

    private async void ProfileButton_Clicked(object sender, EventArgs e)
    {
        var navigationParameter = new Dictionary<string, object>
            {
                {"ShowBackButton", true },
                {"TargetUser", (User)(sender as ImageButton).CommandParameter }
            };

        await Shell.Current.GoToAsync(nameof(ChatPage), true, navigationParameter);
    }
}