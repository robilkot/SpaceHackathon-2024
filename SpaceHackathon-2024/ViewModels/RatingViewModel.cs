using CommunityToolkit.Mvvm.ComponentModel;
using Microcharts;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SpaceHackathon_2024.Models;
using SpaceHackathon_2024.Services;
using System.Collections.ObjectModel;


namespace SpaceHackathon_2024.ViewModels
{
    public partial class RatingViewModel : ObservableObject
    {
        const int TotalChartEntries = 15;
        const int MaxKpi = 150;

        // Фактический нижний KPI для чартов
        const int MinKpi = 10;

        private readonly Color _belowUserColor;
        private readonly Color _aboveUserColor;

        public ObservableCollection<ChartEntry> Entries { get; set; } = [];

        [ObservableProperty]
        private ObservableCollection<User> _bestEmployees = [];

        // Вы лучше чем Х процентов пользователей (0..1)
        [ObservableProperty]
        private double _betterThanOthersPercent;

        // Kpi юзера текущего
        [ObservableProperty]
        private double _lastMonthKpi;

        public RatingViewModel(ApplicationContext context)
        {
            _aboveUserColor = (Color)Application.Current!.Resources["ChartAboveUserColor"];
            _belowUserColor = (Color)Application.Current!.Resources["ChartBelowUserColor"];

            _ = InitializeAsync(context);
            UpdateChart();
        }

        private async Task InitializeAsync(ApplicationContext context)
        {
            BestEmployees = new ObservableCollection<User>(await context.GetTopUsersByKPIAsync(5));
            string surname = Preferences.Default.Get("Surname", string.Empty);
            LastMonthKpi = (double)await (context.GetKPIBySurnameAsync(surname));
            BetterThanOthersPercent = await(context.CalculateUserRatingAsync(LastMonthKpi));

            UpdateChart();
        }

        private void UpdateChart()
        {
            Entries.Clear();

            var currentUserEntryIndex = (int)Math.Floor((double)BetterThanOthersPercent / 100 * TotalChartEntries);

            var labelsStep = TotalChartEntries / 5;
            
            for(var i = 0; i < TotalChartEntries; i++)
            {
                var currentEntryKpi = ((MaxKpi - MinKpi) * i) / (float)TotalChartEntries + MinKpi;
                string valueLabel = i % labelsStep == 0 ? currentEntryKpi.ToString() : string.Empty;

                Entries.Add(
                    new(currentEntryKpi)
                    {
                        Label = valueLabel,
                        ValueLabel = string.Empty,
                        Color = (i > currentUserEntryIndex ? _aboveUserColor : _belowUserColor).ToSKColor(),
                    });
            }
        }
    }
}
