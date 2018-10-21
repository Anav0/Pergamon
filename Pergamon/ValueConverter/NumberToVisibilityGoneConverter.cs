using System;
using System.Globalization;
using System.Windows;

namespace Pergamon
{
    public class NumberToVisibilityGoneConverter : BaseValueConverter<NumberToVisibilityGoneConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value >= 1)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
