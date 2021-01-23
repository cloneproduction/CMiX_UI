using CMiX.MVVM.Controls;
using ColorMine.ColorSpaces;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace CMiX.MVVM.Tools.Converters
{
    public class DataTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
          CultureInfo culture)
        {
            return value?.GetType() ?? Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
          CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
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

    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = null;
            if (value != null)
                result = value.ToString();
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class IntToStringWithMaxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = null;
            if (!double.IsNaN((double)value))
            {
                var val = (double)value;
                var integer = System.Convert.ToInt32(val);
                result = integer.ToString();
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var st = (string)value;
            var max = int.Parse((string)parameter, System.Globalization.CultureInfo.InvariantCulture);

            if (!string.IsNullOrEmpty(st))
            {
                int b = System.Convert.ToInt32(value);
                if (b < 0)
                    b = 0;
                else if (b > max)
                    b = max;
                return b;
            }
            else
                return 0;
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
                result = Math.Pow(2, integer).ToString();
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class ByteToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            byte b = (byte)value;

            return b.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var st = (string)value;

            if(!string.IsNullOrEmpty(st))
            {
                int b = System.Convert.ToInt32(value);
                if(b < 0)
                    return System.Convert.ToByte(0);
                else if(b > 255)
                    return System.Convert.ToByte(255);
                else
                    return System.Convert.ToByte(value);
            }
            else
                return System.Convert.ToByte(0);
        }
    }

    public class DoubleToStringPercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return String.Format("{0:0.}%", (double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            double b = (double)value;
            return String.Format("{0:0.000}", b);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            double resultDouble;
            if (double.TryParse(strValue, out resultDouble))
                return resultDouble;
            else
                return DependencyProperty.UnsetValue;
        }
    }

    public class DoubleToPersistantStringConverter : IValueConverter
    {
        private string lastConvertBackString;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is double)) return null;

            var stringValue = lastConvertBackString ?? value.ToString();
            lastConvertBackString = null;

            return stringValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is string)) return null;
            double result;
            if (double.TryParse((string)value, out result))
            {
                lastConvertBackString = (string)value;
                return result;
            }
            return null;
        }
    }

    public class ByteToNormalizedDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte b = (byte)value;
            return (double)b / 255;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (byte)((double)value * 255);
        }
    }

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

    public class ColorToHSVControledConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = (Color)ColorConverter.ConvertFromString((string)parameter);
            var hsv = new Rgb() { R = color.R, G = color.G, B = color.B }.To<Hsv>();

            if (values[0] != DependencyProperty.UnsetValue)
                hsv.S = (double)values[0];
            if(values[1] != DependencyProperty.UnsetValue)
                hsv.V = (double)values[1];

            var rgb = hsv.To<Rgb>();

            return Color.FromRgb((byte)rgb.R, (byte)rgb.G, (byte)rgb.B);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class ColorToHueValueOnlyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var rgb = new Rgb();
            if (values[0] != DependencyProperty.UnsetValue && values[1] != DependencyProperty.UnsetValue)
            {
                if (!double.IsNaN((double)values[0]) && !double.IsNaN((double)values[1]))
                {

                    rgb = new Hsv() { H = (double)values[0], S = 1.0, V = (double)values[1] }.To<Rgb>();
                }
            }

            return Color.FromRgb((byte)rgb.R, (byte)rgb.G, (byte)rgb.B);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ValToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rgb = new Hsv() { H = 0.0, S = 0.0, V = (double)value }.To<Rgb>();
            return new Color() { R = (byte)rgb.R, G = (byte)rgb.G, B = (byte)rgb.B, ScA = 1.0f };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class HueToColorConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            var rgb = new Hsv() { H = (double)value, S = 1.0, V = 1.0 }.To<Rgb>();
            return new Color() {R = (byte)rgb.R, G = (byte)rgb.G, B = (byte)rgb.B, ScA=1.0f };
        }
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DoubleToInvertedConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return 1.0 - (double)value;
        }
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class DoubleToDividedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value / (double)parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * (double)parameter;
        }
    }

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

    public class ColorToHueOnlyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hue = (double)value;

            if (!double.IsNaN(hue))
            {
                var hsv = new Hsv() { H = 0.0, S = 1.0, V = 1.0 };
                hsv.H = hue;
                var rgb = hsv.To<Rgb>();
                return Color.FromRgb((byte)rgb.R, (byte)rgb.G, (byte)rgb.B);
            }
            else return Colors.Red;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    //public class ColorToSaturationOnlyConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        var saturation = (double)value;
    //        var rgb = new Hsv() { H = 0.0, S = saturation, V = 1.0 }.To<Rgb>();
    //        return Color.FromRgb((byte)rgb.R, (byte)rgb.G, (byte)rgb.B);
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class ColorToValueOnlyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (double)value;
            var rgb = new Hsv() { H = 0.0, S = 0.0, V = val }.To<Rgb>();
            return Color.FromRgb((byte)rgb.R, (byte)rgb.G, (byte)rgb.B);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorToHSVAsDoubleConverter : IValueConverter
    {
        double Hue, Sat, Val;
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colorIn = (Color)value;
            var hsv = new Rgb() { R = colorIn.R, G = colorIn.G, B = colorIn.B }.To<Hsv>();

            if(hsv.V > 0.008)
            {
                Hue = hsv.H;
                Sat = hsv.S;
            }

            Val = hsv.V;

            if ((string)parameter == "Hue")
                return Hue;
            else if ((string)parameter == "Saturation")
                return Sat;
            else if ((string)parameter == "Value")
                return Val;
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorToValueAsDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colorIn = (Color)value;
            var hsv = new Rgb() { R = colorIn.R, G = colorIn.G, B = colorIn.B }.To<Hsv>();
            return hsv.V;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorToSaturationAsDoubleConverter : IValueConverter
    {

        double Sat;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colorIn = (Color)value;
            var hsv = new Rgb() { R = colorIn.R, G = colorIn.G, B = colorIn.B }.To<Hsv>();

            if (hsv.V >= 0.03)
            {
                Sat = hsv.S;
            }

            Console.WriteLine("FromConverter Sat" + Sat);
            return Sat;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorToRedAsDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colorIn = (Color)value;
            var rgb = new Rgb() { R = colorIn.R, G = colorIn.G, B = colorIn.B };
            return colorIn.R;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorToGreenAsDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colorIn = (Color)value;
            return colorIn.G / 255;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorToBlueAsDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colorIn = (Color)value;
            return colorIn.B / 255;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


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

    public class ColorToDoubleConverter : IValueConverter
    {
        double hue, sat, val;
        double output = 0.0;
        Color colorOut = new Color();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color colorIn = (Color)value;
            System.Drawing.Color col = System.Drawing.Color.FromArgb(255, colorIn.R, colorIn.G, colorIn.B);
            ColorUtils.ColorToHSV(colorIn, out hue, out sat, out val);

            if ((string)parameter == "Hue")
                output = col.GetHue(); ;
            if ((string)parameter == "Sat")
                output = col.GetSaturation();
            if ((string)parameter == "Val")
                output = col.GetBrightness();

            return output;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double newvalue = (double)value;

            if ((string)parameter == "Hue")
                colorOut = ColorUtils.ColorFromHSV(newvalue, sat, val);
            if ((string)parameter == "Sat")
                colorOut = ColorUtils.ColorFromHSV(hue, newvalue, val);
            if ((string)parameter == "Val")
                colorOut = ColorUtils.ColorFromHSV(hue, sat, newvalue);

            return colorOut;
        }
    }

    public class ColorToColorComponentConverter : IValueConverter
    {
        float Red, Green, Blue, Alpha;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double componentout = 0.0;

            Color colorIn = (Color)value;
            Red = colorIn.ScR;
            Green = colorIn.ScG;
            Blue = colorIn.ScB;
            Alpha = colorIn.ScA;

            string param = (string)parameter;

            if (param == "Red")
                componentout = (double)Red;

            if (param == "Green")
                componentout = (double)Green;

            if (param == "Blue")
                componentout = (double)Blue;

            if (param == "Alpha")
                componentout = (double)Alpha;

            return componentout;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double newcomponent = (double)value;
            string param = (string)parameter;
            Color colorOut = new Color();

            if (param == "Red")
                colorOut = Color.FromScRgb(Alpha, (float)newcomponent, Green, Blue);

            if (param == "Green")
                colorOut = Color.FromScRgb(Alpha, Red, (float)newcomponent, Blue);

            if (param == "Blue")
                colorOut = Color.FromScRgb(Alpha, Red, Green, (float)newcomponent);

            if (param == "Alpha")
                colorOut = Color.FromScRgb((float)newcomponent, Red, Green, Blue);

            return colorOut;
        }
    }

    public class ColorToSliderBgColorConverter : IValueConverter
    {
        float Red, Green, Blue, Alpha;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color colorOut = new Color();
            Color colorIn = (Color)value;

            Red = colorIn.ScR;
            Green = colorIn.ScG;
            Blue = colorIn.ScB;
            Alpha = colorIn.ScA;

            string param = (string)parameter;

            if (param == "RedRight")
                colorOut = Color.FromScRgb(Alpha, 1.0f, Green, Blue) ;

            if (param == "RedLeft")
                colorOut = Color.FromScRgb(Alpha, 0.0f, Green, Blue);

            if (param == "GreenRight")
                colorOut = Color.FromScRgb(Alpha, Red, 1.0f, Blue);

            if (param == "GreenLeft")
                colorOut = Color.FromScRgb(Alpha, Red, 0.0f, Blue);

            if (param == "BlueRight")
                colorOut = Color.FromScRgb(Alpha, Red, Green, 1.0f);

            if (param == "BlueLeft")
                colorOut = Color.FromScRgb(Alpha, Red, Green, 0.0f);

            if (param == "Alpha")
                colorOut = Color.FromScRgb(1.0f, Red, Green, Blue);

            return colorOut;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    #region COLOR CONVERTERS


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

    public class ColorToStringConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            Color colorValue = (Color)value;
            return ColorNames.GetColorName(colorValue);
        }
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorToHexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hexCode = System.Convert.ToString(value);
            try
            {
                var color = (Color)ColorConverter.ConvertFromString(hexCode);
                return color;
            }
            catch
            {
                return null;
            }

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hexCode = System.Convert.ToString(value);
            try
            {
                return hexCode;
            }
            catch
            {
                return null;
            }
        }
    }
    #endregion


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
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
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
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
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
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string EnumString;
            try
            {
                EnumString = Enum.GetName((value.GetType()), value);
                return value.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        // No need to implement converting back on a one-way binding 
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }

    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }

    public class AlwaysVisibleConverter : IValueConverter
    {
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
    }

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


    //EXTENSION AND CONVERTER FOR TREEVIEW STYLE
    public static class TreeViewItemExtensions
    {
        public static int GetDepth(this TreeViewItem item)
        {
            TreeViewItem parent;
            while ((parent = GetParent(item)) != null)
            {
                return GetDepth(parent) + 1;
            }
            return 0;
        }

        private static TreeViewItem GetParent(TreeViewItem item)
        {
            var parent = VisualTreeHelper.GetParent(item);
            while (!(parent is TreeViewItem || parent is TreeView))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as TreeViewItem;
        }
    }

    public class LeftMarginMultiplierConverter : IValueConverter
    {
        public double Length { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as TreeViewItem;
            if (item == null)
                return new Thickness(0);

            return new Thickness(Length * item.GetDepth(), 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class EntityVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool isParentVisible = true;
            bool isVisible = true;

            if (values[0] is bool)
                isParentVisible = (bool)values[0];

            if (values[1] is bool)
                isVisible = (bool)values[1];

            if (!isParentVisible || !isVisible)
                return false;
            else
                return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            bool visibility = (bool)value;
            object[] ret = new object[2];

            ret[0] = visibility;
            ret[1] = visibility;
            return ret;
        }
    }

    public class ItemToParentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return VisualTreeHelper.GetParent(value as TreeViewItem);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}