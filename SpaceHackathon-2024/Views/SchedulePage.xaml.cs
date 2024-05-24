using SpaceHackathon_2024.ViewModels;

namespace SpaceHackathon_2024.Views;

public partial class SchedulePage : ContentPage
{
    private readonly ScheduleViewModel _viewModel;
    public SchedulePage(ScheduleViewModel viewModel)
    {
        BindingContext = viewModel;
        _viewModel = viewModel;

        InitializeComponent();
    }
}