using SpaceHackathon_2024.ViewModels;

namespace SpaceHackathon_2024.Views;

public partial class NewsPage : ContentPage
{
    private readonly NewsViewModel _viewModel;
    public NewsPage(NewsViewModel newsViewModel)
	{
		InitializeComponent();

        BindingContext = _viewModel = newsViewModel;
    }
}