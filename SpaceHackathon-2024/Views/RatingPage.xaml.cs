using Microcharts;
using SpaceHackathon_2024.ViewModels;

namespace SpaceHackathon_2024.Views;

public partial class RatingPage : ContentPage
{
    private readonly RatingViewModel _viewModel;
    public RatingPage(RatingViewModel ratingViewModel)
	{
        InitializeComponent();

        BindingContext = _viewModel = ratingViewModel;
    }

    protected override void OnAppearing()
    {
        UpdateChart();
    }

    private void UpdateChart()
    {
        chartView.Chart = new BarChart
        {
            ShowYAxisText = false,
            MaxValue = 150,
            MinValue = 0,
            Entries = _viewModel.Entries,
        };
    }
}