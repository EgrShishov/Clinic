using System.Globalization;
using System.Windows.Data;

namespace AdminApp.Converters;
public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isVisible)
            return isVisible;

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isVisible)
            return isVisible;

        return false;
    }
}
