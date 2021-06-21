﻿using ColorMine.ColorSpaces;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace CMiX.Core.Presentation.ValueConverters
{
    public class ColorToHueSatOnlyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var rgb = new Rgb();
            if(!double.IsNaN((double)values[0]) && !double.IsNaN((double)values[1]))
            {
                if (values[0] != DependencyProperty.UnsetValue && values[1] != DependencyProperty.UnsetValue)
                {
                    rgb = new Hsv() { H = (double)values[0], S = (double)values[1], V = 1.0 }.To<Rgb>();
                }
                    
            }

            return new Color() { R = (byte)rgb.R, G = (byte)rgb.G, B = (byte)rgb.B, ScA = 1.0f };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}