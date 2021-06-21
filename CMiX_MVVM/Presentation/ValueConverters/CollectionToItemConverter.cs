﻿using CMiX.Core.Presentation.Controls;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace CMiX.Core.Presentation.ValueConverters
{
    public class CollectionToItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<AnimatedDouble> col = new ObservableCollection<AnimatedDouble>();
            int index = (int)parameter;
            return ((ObservableCollection < AnimatedDouble > )value)[3].AnimationPosition;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}