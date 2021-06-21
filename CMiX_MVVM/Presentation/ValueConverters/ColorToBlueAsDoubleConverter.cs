﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CMiX.Core.Presentation.ValueConverters
{
    public class ColorToBlueAsDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colorIn = (Color)value;
            return ((double)colorIn.B) / 255d;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}