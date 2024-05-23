using CommunityToolkit.Mvvm.ComponentModel;
using SpaceHackathon_2024.Models;
using System.Collections.ObjectModel;

namespace SpaceHackathon_2024.ViewModels
{
    public partial class ScheduleViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<WeeklySchedule> _weeks;

        public ScheduleViewModel()
        {
            Weeks = [new WeeklySchedule { Days = [new(), new(), new()] }];
        }
    }
}