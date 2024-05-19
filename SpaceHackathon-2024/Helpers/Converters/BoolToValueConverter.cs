using System.Globalization;

namespace SpaceHackathon_2024.Helpers.Converters
{
    public class BoolToValueConverter : IValueConverter
    {
        public object? TrueValue { get; set; } = null;
        public object? FalseValue { get; set; } = null;
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool variable)
            {
                return variable ? TrueValue : FalseValue;
            }
            else throw new NotImplementedException();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
