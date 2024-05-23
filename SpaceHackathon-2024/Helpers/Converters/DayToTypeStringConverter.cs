using SpaceHackathon_2024.Models;
using SpaceHackathon_2024.Models.Enums;
using System.Globalization;

namespace SpaceHackathon_2024.Helpers.Converters
{
    public class DayToTypeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not ScheduleDay day) 
            {
                return null!;
            }

            string style = day.Type switch
            {
                DayTypes.Working => "Рабочий день",
                DayTypes.Holiday => "Праздник",
                DayTypes.Weekend => "Выходной",
                DayTypes.Vacation => "Отпуск",
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