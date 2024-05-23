using Microcharts;
using SkiaSharp;
using SpaceHackathon_2024.ViewModels;

namespace SpaceHackathon_2024.Views;

public partial class RatingPage : ContentPage
{
    private readonly RatingViewModel _viewModel;
    public RatingPage(RatingViewModel ratingViewModel)
	{
        InitializeComponent();

        BindingContext = _viewModel = ratingViewModel;

        chartView.Chart = new BarChart
        {
            Entries = _viewModel.Entries
        };
    }
}