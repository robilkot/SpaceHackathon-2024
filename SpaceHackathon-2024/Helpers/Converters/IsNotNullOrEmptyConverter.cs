using System.Globalization;

namespace SpaceHackathon_2024.Helpers.Converters
{
    /// <summary>
    ///     converter to check if an object is null or an empty list/IEnumerable.
    ///     if it is not null and NOT a list the object is read as a string and checked for 'NullOrWhiteSpace'
    /// </summary>
    public class IsNotNullOrEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return false;
            if (value is IEnumerable<object> enumerableValue)
                return enumerableValue.Any();
            return !string.IsNullOrWhiteSpace(value?.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}