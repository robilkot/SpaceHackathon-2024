using SpaceHackathon_2024.ViewModels;

namespace SpaceHackathon_2024.Views;

public partial class NewsPage : ContentPage
{
    public NewsPage(NewsViewModel newsViewModel)
	{
		InitializeComponent();

        BindingContext = newsViewModel;
    }
}