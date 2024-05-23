using SpaceHackathon_2024.ViewModels;

namespace SpaceHackathon_2024.Views;

public partial class AllNewsPage : ContentPage
{
	public AllNewsPage(AllNewsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
    {
        var scrollView = sender as ScrollView;
        if (scrollView != null && scrollView.ScrollY >= (scrollView.ContentSize.Height - scrollView.Height) * 0.8)
        {
            var viewModel = BindingContext as AllNewsViewModel;
            if (viewModel != null && !viewModel.IsBusy)
            {
                viewModel.LoadMoreCommand.Execute(null);
            }
        }
    }
}