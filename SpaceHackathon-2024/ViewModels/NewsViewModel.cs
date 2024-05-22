using CommunityToolkit.Mvvm.ComponentModel;
using SpaceHackathon_2024.Models;
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
        private List<News> _news = new() { new("test", "test", "test"), new("test", "test", "test"), new("test", "test", "test") };

        public ObservableCollection<News> News1 { get; }
        public ICommand LoadMoreCommand { get; }

        [ObservableProperty]
        public bool isBusy;

        public NewsViewModel(string dbPath)
        {
            //_newsDatabase = new NewsDatabase(dbPath);
            News1 = new ObservableCollection<News>();
            LoadMoreCommand = new Command(async () => await LoadMoreNewsAsync());
        }

        private async Task LoadMoreNewsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var newsItems = await _newsDatabase.GetNewsAsync(_pageNumber, PageSize);
            foreach (var news in newsItems)
            {
                NewsItems.Add(news);
            }

            _pageNumber++;
            IsBusy = false;
        }

    }
}
