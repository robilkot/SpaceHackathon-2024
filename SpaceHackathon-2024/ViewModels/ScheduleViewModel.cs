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

            // Create weeks for the next 4 weeks
            for (int i = 0; i < 4; i++)
            {
                DateOnly startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(i * 7).Date);
                DateOnly endDate = startDate.AddDays(6);

                // Get days from the database
                var days = context.GetScheduleDays(startDate, endDate);

                // Create WeeklySchedule and add it to the Weeks collection
                var weeklySchedule = new WeeklySchedule
                {
                    StartDay = startDate,
                    EndDay = endDate,
                    Days = new List<ScheduleDay>(days)
                };

                Weeks.Add(weeklySchedule);
            }
        }
    }
}