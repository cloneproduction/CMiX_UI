﻿using CMiX.Core.Tools;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CMiX.Core.Presentation.ValueConverters
{
    public class DoubleToColorSatConverter : IValueConverter
    {
        double hue, sat, val;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Console.WriteLine("DoubleToColorSatConverter");
            Color colorIn = (Color)value;
            ColorUtils.ColorToHSV(colorIn, out hue, out sat, out val);
            return 1-sat;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double newsat = (double)value;
            return ColorUtils.ColorFromHSV(hue, newsat, val); ;
        }
    }
}