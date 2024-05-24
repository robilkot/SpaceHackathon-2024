using CommunityToolkit.Mvvm.ComponentModel;
using SpaceHackathon_2024.Models;

namespace SpaceHackathon_2024.ViewModels
{
    [QueryProperty(nameof(SelectedNews), "SelectedNews")]
    public partial class NewsViewModel : ObservableObject
    {
        [ObservableProperty]
        private News? _selectedNews;
    }
}
