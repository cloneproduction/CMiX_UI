using System;
using System.Globalization;
using System.Windows.Data;

namespace CMiX.Core.Presentation.ValueConverters
{
    public class DoubleToStringPercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return String.Format("{0:0.}%", (double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}