using System;
using System.Globalization;
using System.Windows.Data;

namespace CMiX.Core.Presentation.ValueConverters
{
    public class DoubleToInvertedConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return 1.0 - (double)value;
        }
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}