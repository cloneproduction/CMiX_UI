﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CMiX.Core.Presentation.ValueConverters
{
    public class RadioButtonSelectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.Equals(parameter))
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.Equals(true))
            {
                return parameter;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}