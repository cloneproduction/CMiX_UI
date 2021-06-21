﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CMiX.Core.Presentation.ValueConverters
{
    public sealed class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is Visibility.Collapsed)
            {
                return value == null ? Visibility.Collapsed : Visibility.Visible;
            }
            else
                return value == null ? Visibility.Hidden : Visibility.Visible; ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}