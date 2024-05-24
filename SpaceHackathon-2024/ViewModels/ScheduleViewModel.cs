using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using SpaceHackathon_2024.Models;
using SpaceHackathon_2024.Models.Enums;
using SpaceHackathon_2024.Services;
using System.Collections.ObjectModel;

namespace SpaceHackathon_2024.ViewModels
{
    public partial class ScheduleViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<WeeklySchedule> _weeks;

        public ScheduleViewModel(ApplicationContext context)
        {
            Weeks = new ObservableCollection<WeeklySchedule>();

            // Получаем текущую дату и определяем день недели (понедельник - первый день недели)
            DateTime today = DateTime.Now.Date;
            int daysUntilMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
            var startDate = DateOnly.FromDateTime(today.AddDays(-((int)today.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7));

            // Create weeks for the next 4 weeks
            for (int i = 0; i < 4; i++)
            {
                DateOnly currentWeekStartDate = startDate.AddDays(i * 7);
                DateOnly endDate = currentWeekStartDate.AddDays(6);

                // Get days from the database
                var days = context.GetScheduleDays(currentWeekStartDate, endDate);

                // Create WeeklySchedule and add it to the Weeks collection
                var weeklySchedule = new WeeklySchedule
                {
                    StartDay = currentWeekStartDate,
                    EndDay = endDate,
                    Days = new List<ScheduleDay>(days)
                };

                Weeks.Add(weeklySchedule);
            }
        }
    }
}