using Microcharts;
using SkiaSharp;

namespace SpaceHackathon_2024.Views;

public partial class RatingPage : ContentPage
{
    ChartEntry[] entries = new[]
    {
            new ChartEntry(212)
            {
                Label = "Windows",
                ValueLabel = "112",
            }
        };
    public RatingPage()
	{
        InitializeComponent();

        chartView.Chart = new BarChart
        {
            Entries = entries
        };
	}
}