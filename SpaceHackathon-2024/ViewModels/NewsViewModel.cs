using CommunityToolkit.Mvvm.ComponentModel;
using SpaceHackathon_2024.Models;

namespace SpaceHackathon_2024.ViewModels
{
    public partial class NewsViewModel : ObservableObject
    {
        [ObservableProperty]
        private List<News> _news = new() { new("test", "test", "test"), new("test", "test", "test"), new("test", "test", "test") };
    }
}
