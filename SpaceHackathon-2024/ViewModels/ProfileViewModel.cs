using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SpaceHackathon_2024.ViewModels;

public partial class ProfileViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _workInfoExpanded;
    [ObservableProperty]
    private double _workInfoExpandedRotation = 90;


    [ObservableProperty]
    private bool _ratingExpanded;
    [ObservableProperty]
    private double _ratingExpandedRotation = 90;

    [RelayCommand]
    public void WorkInfoHeaderTapped()
    {
        WorkInfoExpanded = !WorkInfoExpanded;
        WorkInfoExpandedRotation = WorkInfoExpanded ? 270 : 90;
    }

    [RelayCommand]
    public void RatingHeaderTapped()
    {
        RatingExpanded = !RatingExpanded;
        RatingExpandedRotation = RatingExpanded ? 270 : 90;
    }
}