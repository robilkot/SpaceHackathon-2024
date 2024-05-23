using SpaceHackathon_2024.Models;
using System.Globalization;

namespace SpaceHackathon_2024.Helpers.Converters
{
    public class DayToWeekDayStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not ScheduleDay day)
            {
                return null!;
            }

            string style = day.Date.DayOfWeek switch
            {
                DayOfWeek.Sunday => "Вс.",
                DayOfWeek.Monday => "Пн.",
                DayOfWeek.Tuesday => "Вт.",
                DayOfWeek.Wednesday => "Ср.",
                DayOfWeek.Thursday => "Чт.",
                DayOfWeek.Friday => "Пт.",
                DayOfWeek.Saturday => "Сб.",
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