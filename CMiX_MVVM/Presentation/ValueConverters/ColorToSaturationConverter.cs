﻿using CMiX.Core.Tools;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CMiX.Core.Presentation.ValueConverters
{
    public class ColorToSaturationConverter : IValueConverter
    {
        double hue, sat, val;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color colorIn = (Color)value;
            ColorUtils.ColorToHSV(colorIn, out hue, out sat, out val);
            return sat;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double newsat = (double)value;
            return ColorUtils.ColorFromHSV(hue, newsat, val);
        }
    }
}