using CommunityToolkit.Mvvm.ComponentModel;
using SpaceHackathon_2024.Models;
using SpaceHackathon_2024.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SpaceHackathon_2024.ViewModels
{
    public partial class NewsViewModel : ObservableObject
    {
        private bool _isBusy;

        private int _pageNumber = 1;

        private const int PageSize = 10;

        [ObservableProperty]
        private List<News> _news = new();

        public ObservableCollection<News> News1 { get; } = new();

        public ICommand LoadMoreCommand { get; }

        private ApplicationContext _appContext;

        [ObservableProperty]
        public bool isBusy;

        public NewsViewModel(ApplicationContext appContext)
        {
            _appContext = appContext;

            LoadMoreCommand = new Command(async () => await LoadMoreNewsAsync());
        }

        private async Task LoadMoreNewsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var newsItems = await _appContext.GetNewsAsync(_pageNumber, PageSize);
            foreach (var news in newsItems)
            {
                _appContext.News.Add(news);
            }

            _pageNumber++;
            IsBusy = false;
        }

    }
}
