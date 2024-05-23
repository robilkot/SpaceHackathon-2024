using CommunityToolkit.Mvvm.ComponentModel;
using SpaceHackathon_2024.Models;
using SpaceHackathon_2024.Services;

namespace SpaceHackathon_2024.ViewModels
{
    [QueryProperty(nameof(SelectedNews), "SelectedNews")]
    public partial class NewsViewModel : ObservableObject
    {
        private readonly ApplicationContext _appContext;

        [ObservableProperty]
        private News _selectedNews;

        public NewsViewModel(ApplicationContext appContext)
        {
            _appContext = appContext;
        }
    }
}
