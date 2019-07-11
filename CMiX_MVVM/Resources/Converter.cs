using CMiX_MVVM.Resources;
using ColorMine.ColorSpaces;
using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace CMiX.MVVM.Resources
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

    public class AltBackgroundConverter : IValueConverter
    {
        bool isAlternate;
        SolidColorBrush even = new SolidColorBrush(Color.FromArgb(255, 45, 45, 45));
        SolidColorBrush odd = new SolidColorBrush(Color.FromArgb(255, 40, 40, 40));

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            isAlternate = !isAlternate;
            return isAlternate ? even : odd;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PathToFilenameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = null;
            if (value != null)
            {
                //if (string.IsNullOrWhiteSpace(path) == false)
                result = Path.GetFileNameWithoutExtension(value.ToString());
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    [Obsolete("Prefer using a BPM property in the viewmodel")]
    public class PeriodToBPMConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double doubleType = 60000 / (double)value ;
            return doubleType.ToString("0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = null;
            if (value != null)
            {
                result = value.ToString();
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class IndexToCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = null;
            int integer = (int)value;
            if (value != null)
            {
                //if (string.IsNullOrWhiteSpace(path) == false)
                result = Math.Pow(2, integer).ToString();
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class ColorToHueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorToDoubleConverter : IValueConverter
    {
        private Color _lastColor;
        private Hsv _lastHsv;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            Color color = (Color)value;
            _lastColor = color;

            Rgb colrgb = new Rgb();
            colrgb.R = color.R;
            colrgb.G = color.G;
            colrgb.B = color.B;

            Hsv colhsv = colrgb.To<Hsv>();
            _lastHsv = colhsv;

            switch ((string)parameter)
            {
                case "r": return colhsv.H;
                case "g": return colhsv.S;
                case "b": return colhsv.V;
                //case "a": return color.A;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = _lastColor;
            Hsv colhsv = _lastHsv;
            var intensity = (double)value;

            Rgb col = new Rgb { R = 0, B = 0, G = 0 };
            col.R = color.R;
            col.G = color.G;
            col.B = color.B;

            switch ((string)parameter)
            {
                case "r":
                    colhsv.H = intensity;
                    //color.R = intensity;
                    break;
                case "g":
                    colhsv.S = intensity;
                    //color.G = intensity;
                    break;
                case "b":
                    colhsv.V = intensity;
                    //color.B = intensity;
                    break;
                case "a":
                    //color.A = intensity;
                    break;
            }
            col = colhsv.To<Rgb>();
            color.R = (byte)col.R;
            color.G = (byte)col.G;
            color.B = (byte)col.B;

            _lastColor = color;
            _lastHsv = colhsv;
            return color;
        }
    }

    public class ColorToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string color = (string)value;
            Color Output = Utils.HexStringToColor(color);
            return Output;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = (Color)value;
            string Output = Utils.ColorToHexString(color);
            return Output;
        }
    }

    public class ColorToBrushConverter : IValueConverter
    {
        SolidColorBrush _red = new SolidColorBrush(),
                        _green = new SolidColorBrush(),
                        _blue = new SolidColorBrush(),
                        _alpha = new SolidColorBrush(),
                        _all = new SolidColorBrush();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color)value;
            switch ((string)parameter)
            {
                case "r":
                    _red.Color = Color.FromRgb(color.R, 0, 0);
                    return _red;
                case "g":
                    _green.Color = Color.FromRgb(0, color.G, 0);
                    return _green;
                case "b":
                    _blue.Color = Color.FromRgb(0, 0, color.B);
                    return _blue;
                case "a":
                    _alpha.Color = Color.FromArgb(color.A,
                    128, 128, 128);
                    return _alpha;
                case "all":
                    _all.Color = Color.FromArgb(color.A, color.R, color.G, color.B);
                    return _all;

            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = (String)value;
            bool Output = bool.Parse(s);
            return Output;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = (bool)value;
            string Output = b.ToString();
            return Output;
        }
    }

    public class RadioButtonCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }

    public class BoolInverterConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
                return !(bool)value;
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }

    public class BoolToVisibilityConverter : IValueConverter
    {
        private object GetVisibility(object value)
        {
            if (!(value is bool))
                return Visibility.Collapsed;
            bool objValue = (bool)value;
            if (objValue)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetVisibility(value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string EnumString;
            try
            {
                EnumString = Enum.GetName((value.GetType()), value);
                return value.ToString();
                //return EnumString;
            }
            catch
            {
                return string.Empty;
            }
        }

        // No need to implement converting back on a one-way binding 
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString();
        }
    }

    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }

    public class AlwaysVisibleConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value,
                              Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public sealed class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RangeValueToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = null;
            double range = System.Convert.ToDouble(value);
            if (value != null)
            {
                result = (range / 100).ToString("0.00");
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = null;
            double range = System.Convert.ToDouble(value);
            if (value != null)
            {
                result = (range * 100).ToString("0.00");
            }
            return result;
        }
    }

    public class IntToNormalizedDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0.00;
            double range = System.Convert.ToDouble(value);
            if (value != null)
            {
                result = (range * 100);
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0.00;
            double range = System.Convert.ToDouble(value);
            if (value != null)
            {
                result = (range / 100);
            }
            return result;
        }
    }

    public class ValueAngleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double value = (double)values[0];
            double minimum = (double)values[1];
            double maximum = (double)values[2];
            return MyHelper.GetAngle(value, maximum, minimum);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}