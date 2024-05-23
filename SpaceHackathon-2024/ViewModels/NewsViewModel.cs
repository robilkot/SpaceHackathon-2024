using CommunityToolkit.Mvvm.ComponentModel;
using SpaceHackathon_2024.Models;
using SpaceHackathon_2024.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

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
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SelectedNews = new News("Test News 1", currentDate, "This is a test news item.", "https://www.mtsbank.ru/upload/static/news/2020/IMG_0744.jpg");
            _appContext = appContext;
        }
    }
}
