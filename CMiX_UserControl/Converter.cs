using ColorMine.ColorSpaces;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;

namespace CMiX
{
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
                result = Path.GetFileName(value.ToString());
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

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
}
