﻿using CommunityToolkit.Mvvm.ComponentModel;
using SpaceHackathon_2024.Models;
using SpaceHackathon_2024.Services;
using SpaceHackathon_2024.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SpaceHackathon_2024.ViewModels
{
    public partial class AllNewsViewModel : ObservableObject
    {
        private int _pageNumber = 1;

        private const int PageSize = 3;

        public ObservableCollection<News> News { get; set; } = new();

        public ICommand LoadMoreCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand NewsSelectedCommand { get; }

        private readonly ApplicationContext _appContext;

        [ObservableProperty]
        private bool _isBusy = false;

        [ObservableProperty]
        private bool _isRefreshing;

        public AllNewsViewModel(ApplicationContext appContext)
        {
            _appContext = appContext;

            _ = RefreshNewsAsync();

            LoadMoreCommand = new Command(async () => await LoadMoreNewsAsync());

            RefreshCommand = new Command(async () => await RefreshNewsAsync());

            NewsSelectedCommand = new Command<News>(OnNewsSelected);
        }
        async void OnNewsSelected(News selectedNews)
        {
            if (selectedNews == null)
                return;

            await Shell.Current.GoToAsync(nameof(NewsPage), true, new Dictionary<string, object>
            {
                { "SelectedNews", selectedNews }
            });
        }

        private async Task LoadMoreNewsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var newsItems = await _appContext.GetNewsAsync(_pageNumber, PageSize);

            foreach (var news in newsItems)
            {
                News.Add(news);
            }

            _pageNumber++;
            IsBusy = false;
        }

        private async Task RefreshNewsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            IsRefreshing = true;

            _pageNumber = 1;
            News.Clear();

            var newsItems = await _appContext.GetNewsAsync(_pageNumber, PageSize);

            foreach (var news in newsItems)
            {
                News.Add(news);
            }

            _pageNumber++;
            IsBusy = false;

            IsRefreshing = false;
        }
    }
}
