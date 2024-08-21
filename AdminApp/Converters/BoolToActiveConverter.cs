using System.Globalization;
using System.Windows.Data;

namespace AdminApp.Converters;
public class BoolToActiveConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isActive)
            return isActive;

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isChecked)
            return isChecked;

        return false;
    }
}
