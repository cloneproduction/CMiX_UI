﻿using CMiX.Core.Tools;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CMiX.Core.Presentation.ValueConverters
{
    public class ColorHueToDoubleConverter : IValueConverter
    {
        double hue, sat, val;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color colorIn = (Color)value;
            ColorUtils.ColorToHSV(colorIn, out hue, out sat, out val);
            return hue / 360;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double newhue = (double)value;
            Color colorOut = ColorUtils.ColorFromHSV(newhue * 360, sat, val);
            return colorOut;
        }
    }
}