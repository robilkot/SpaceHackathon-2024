using SpaceHackathon_2024.Models;
using SpaceHackathon_2024.Models.Enums;
using System.Globalization;

namespace SpaceHackathon_2024.Helpers.Converters
{
    public class DayToTileStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not ScheduleDay day) 
            {
                return null!;
            }

            Style style = day.Type switch
            {
                DayTypes.Working => (Style)Application.Current!.Resources["WorkingDayTile"],
                DayTypes.Holiday => (Style)Application.Current!.Resources["HolidayTile"],
                DayTypes.Weekend => (Style)Application.Current!.Resources["WeekendTile"],
                DayTypes.Vacation => (Style)Application.Current!.Resources["VacationTile"],
                _ => null!
            };

            return style;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}