
using System.Windows.Data;
using System.Windows.Media;

namespace DD_QLQuanNet.common
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var status = value as string;
            if (status == null)
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));

            switch (status)
            {
                case "Available":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00")); // Red
                case "In Use":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00")); // Yellow
                case "Maintenance":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000")); // Green
                default:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000")); // Black
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
