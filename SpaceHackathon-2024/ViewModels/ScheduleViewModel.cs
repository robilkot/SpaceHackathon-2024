using CommunityToolkit.Mvvm.ComponentModel;
using SpaceHackathon_2024.Models;
using SpaceHackathon_2024.Models.Enums;
using System.Collections.ObjectModel;

namespace SpaceHackathon_2024.ViewModels
{
    public partial class ScheduleViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<WeeklySchedule> _weeks;

        public ScheduleViewModel()
        {
            var weekly = new WeeklySchedule
            {
                Days = [
                new() {
                    Type = DayTypes.Working,
                    Description = string.Empty,
                    Date = DateTime.Now,
                },
                new() {
                    Type = DayTypes.Working,
                    Description = string.Empty,
                    Date = DateTime.Now,
                },
                new() {
                    Type = DayTypes.Working,
                    Description = string.Empty,
                    Date = DateTime.Now,
                },
                new() {
                    Type = DayTypes.Holiday,
                    Description = string.Empty,
                    Date = DateTime.Now,
                },
                new() {
                    Type = DayTypes.Working,
                    Description = "Тут нет выходного!",
                    Date = DateTime.Now,
                },
                new() {
                    Type = DayTypes.Weekend,
                    Description = string.Empty,
                    Date = DateTime.Now,
                },
                new() {
                    Type = DayTypes.Weekend,
                    Description = string.Empty,
                    Date = DateTime.Now,
                }]
            };

            var weekly2 = new WeeklySchedule
            {
                Days = [
                new() {
                    Type = DayTypes.Working,
                    Description = string.Empty,
                    Date = DateTime.Now,
                },
                new() {
                    Type = DayTypes.Vacation,
                    Description = string.Empty,
                    Date = DateTime.Now,
                },
                new() {
                    Type = DayTypes.Vacation,
                    Description = string.Empty,
                    Date = DateTime.Now,
                },
                new() {
                    Type = DayTypes.Vacation,
                    Description = "День отпуска перенесён по заявке #1220",
                    Date = DateTime.Now,
                },
                new() {
                    Type = DayTypes.Working,
                    Description = string.Empty,
                    Date = DateTime.Now,
                },
                new() {
                    Type = DayTypes.Working,
                    Description = "Смена вместо Никиты Хорошуна по заявке #1150",
                    Date = DateTime.Now,
                },
                new() {
                    Type = DayTypes.Weekend,
                    Description = string.Empty,
                    Date = DateTime.Now,
                }]
            };

            Weeks = [weekly, weekly2];
        }
    }
}