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
}