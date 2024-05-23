using CommunityToolkit.Mvvm.ComponentModel;
using Microcharts;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SpaceHackathon_2024.Models;
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
        private int _betterThanOthersPercent;

        // Kpi юзера текущего
        [ObservableProperty]
        private int _lastMonthKpi;

        public RatingViewModel()
        {
            // todo delete this
            BestEmployees.Add(new());
            BestEmployees.Add(new());
            BestEmployees.Add(new());
            BestEmployees.Add(new());
            BestEmployees.Add(new());
            BestEmployees.Add(new());

            LastMonthKpi = 89;
            // todo: get this from backend
            BetterThanOthersPercent = 73;

            _aboveUserColor = (Color)Application.Current!.Resources["ChartAboveUserColor"];
            _belowUserColor = (Color)Application.Current!.Resources["ChartBelowUserColor"];

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
