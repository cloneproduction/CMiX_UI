using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using ColorMine.ColorSpaces;

namespace CMiX.Core.Tools
{
    public static class ColorNames
    {
        #region Public Methods

        static ColorNames()
        {
            m_colorNames = new Dictionary<Color, string>();
            //FillColorNames();
        }

        static public String GetColorName(Color colorToSeek)
        {
            if (m_colorNames.ContainsKey(colorToSeek))
                return m_colorNames[colorToSeek];
            else
                return colorToSeek.ToString();
        }

        #endregion

        #region Private Methods

        public static void FillColorNames()
        {
            Type colorsType = typeof(System.Windows.Media.Colors);
            PropertyInfo[] colorsProperties = colorsType.GetProperties();

            foreach (PropertyInfo colorProperty in colorsProperties)
            {
                String colorName = colorProperty.Name;
                Color color = (Color)colorProperty.GetValue(null, null);

                // Path - Aqua is the same as Magenta - so we add 1 to red to avoid collision
                if (colorName == "Aqua")
                    color.R++;

                if (colorName == "Fuchsia")
                    color.G++;

                m_colorNames.Add(color, colorName);
            }
        }

        #endregion

        #region Private Members

        static private Dictionary<Color, String> m_colorNames;

        #endregion


    }

    public static class ColorUtils
    {
        public static String[] GetColorNames()
        {
            Type colorsType = typeof(System.Windows.Media.Colors);
            PropertyInfo[] colorsProperties = colorsType.GetProperties();

            ColorConverter convertor = new ColorConverter();

            List<String> colorNames = new List<String>();
            foreach (PropertyInfo colorProperty in colorsProperties)
            {
                String colorName = colorProperty.Name;
                colorNames.Add(colorName);

                Color color = (Color)ColorConverter.ConvertFromString(colorName);
            }

            String[] colorNamesArray = new String[colorNames.Count];
            return colorNames.ToArray();
        }

        public static void FireSelectedColorChangedEvent(UIElement issuer, RoutedEvent routedEvent, Color oldColor, Color newColor)
        {
            RoutedPropertyChangedEventArgs<Color> newEventArgs =
                new RoutedPropertyChangedEventArgs<Color>(oldColor, newColor);
            newEventArgs.RoutedEvent = routedEvent;
            issuer.RaiseEvent(newEventArgs);
        }

        private static Color BuildColor(double red, double green, double blue, double m)
        {
            return Color.FromArgb(
                255,
                (byte)((red + m) * 255 + 0.5),
                (byte)((green + m) * 255 + 0.5),
                (byte)((blue + m) * 255 + 0.5));
        }

        public static void ConvertRgbToHsv(Color color, out double hue, out double saturation, out double value)
        {
            //Console.WriteLine("ConvertRgbToHsv");
            double red = color.R / 255.0;
            double green = color.G / 255.0;
            double blue = color.B / 255.0;
            double min = Math.Min(red, Math.Min(green, blue));
            double max = Math.Max(red, Math.Max(green, blue));

            value = max;
            double delta = max - min;

            if (value == 0)
                saturation = 0;
            else
                saturation = delta / max;

            if (saturation == 0)
                hue = 0;
            else
            {
                if (red == max)
                    hue = (green - blue) / delta;
                else if (green == max)
                    hue = 2 + (blue - red) / delta;
                else // blue == max
                    hue = 4 + (red - green) / delta;
            }
            hue *= 60;
            if (hue < 0)
                hue += 360;
        }

        public static Color ConvertHsvToRgb(double hue, double saturation, double value)
        {
            double q;
            double H = hue;
            double S = saturation;
            double L = value;

            if (L < 0.5)
            {
                q = L * (1 + S);
            }
            else
            {
                q = L + S - L * S;
            }
            double p = 2 * L - q;
            double hk = H / 360;

            // r,g,b colors
            double[] tc = new[]{hk + 1d / 3d, hk, hk - 1d / 3d};
            double[] colors = new[]{0.0, 0.0, 0.0};

            for (int color = 0; color < colors.Length; color++)
            {
                if (tc[color] < 0)
                {
                    tc[color] += 1;
                }
                if (tc[color] > 1)
                {
                    tc[color] -= 1;
                }

                if (tc[color] < 1d / 6d)
                {
                    colors[color] = p + (q - p) * 6 * tc[color];
                }
                else if (tc[color] >= 1d / 6d && tc[color] < 1d / 2d)
                {
                    colors[color] = q;
                }
                else if (tc[color] >= 1d / 2d && tc[color] < 2d / 3d)
                {
                    colors[color] = p + (q - p) * 6 * (2d / 3d - tc[color]);
                }
                else
                {
                    colors[color] = p;
                }

                colors[color] *= 255;
            }

            return Color.FromArgb(255, (byte)colors[0], (byte)colors[1], (byte)colors[2]);
            //double chroma = value * saturation;

            //if (hue == 360)
            //    hue = 0;

            //double hueTag = hue / 60;
            //double x = chroma * (1 - Math.Abs(hueTag % 2 - 1));
            //double m = value - chroma;
            //switch ((int)hueTag)
            //{
            //    case 0:
            //        return BuildColor(chroma, x, 0, m);
            //    case 1:
            //        return BuildColor(x, chroma, 0, m);
            //    case 2:
            //        return BuildColor(0, chroma, x, m);
            //    case 3:
            //        return BuildColor(0, x, chroma, m);
            //    case 4:
            //        return BuildColor(x, 0, chroma, m);
            //    default:
            //        return BuildColor(chroma, 0, x, m);
            //}
        }

        public static Color[] GetHueColors(int colorCount)
        {
            Color[] spectrumColors = new Color[colorCount];
            for (int i = 0; i < colorCount; ++i)
            {
                double hue = (i * 360.0) / colorCount;
                spectrumColors[i] = ConvertHsvToRgb(hue, /*saturation*/1.0, /*value*/1.0);
            }

            return spectrumColors;
        }

        public static bool TestColorConversion()
        {
            for (int i = 0; i <= 0xFFFFFF; ++i)
            {
                byte Red = (byte)(i & 0xFF);
                byte Green = (byte)((i & 0xFF00) >> 8);
                byte Blue = (byte)((i & 0xFF0000) >> 16);
                Color originalColor = Color.FromRgb(Red, Green, Blue);

                double hue, saturation, value;
                ConvertRgbToHsv(originalColor, out hue, out saturation, out value);

                Color resultColor = ConvertHsvToRgb(hue, saturation, value);
                if (originalColor != resultColor)
                    return false;
            }
            return true;
        }

        //public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        //{
        //    var rgb = new Rgb() { R = color.R, G = color.G, B = color.B };
        //    var hsv = rgb.To<Hsb>();

        //    hue = hsv.H;
        //    saturation = hsv.S;
        //    value = hsv.B;
        //}

        //public static Color ColorFromHSV(double hue, double saturation, double value)
        //{
        //    var hsv = new Hsb() { H = hue, S = saturation, B = value };
        //    var rgb = hsv.To<Rgb>();
        //    return Color.FromArgb(255, (byte)rgb.R, (byte)rgb.G, (byte)rgb.B);
        //}

        //public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        //{
        //    double h, s, v;
        //    int max = Math.Max(color.R, Math.Max(color.G, color.B));
        //    int min = Math.Min(color.R, Math.Min(color.G, color.B));

        //    hue = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B).GetHue();
        //    saturation = (max == 0) ? 0 : 1d - (1d * min / max);
        //    value = max / 255d;
        //}


        public static void ColorToHSV(Color rgb, out double H, out double S, out double V)
        {
            double max, min, delta;

            //g_return_if_fail(rgb != NULL);
            //g_return_if_fail(hsv != NULL);

            max = Max(rgb);
            min = Min(rgb);

            V = max/255;
            delta = max - min;

            if (delta > 0.0001)
            {

                S = delta / max;

                if (rgb.R == max)
                {
                    H = (rgb.G - rgb.B) / delta;
                    if (H < 0.0)
                        H += 6.0;
                }
                else if (rgb.G == max)
                {
                    H = 2.0 + (rgb.B - rgb.R) / delta;
                }
                else
                {
                    H = 4.0 + (rgb.R - rgb.G) / delta;
                }

                H /= 6.0 * 360;
            }
            else
            {
                S = 0.0;
                H = 0.0;
            }

        }

        public static Color ColorFromHSV(double H, double S, double V)
        {
            //Console.WriteLine("FROM HSV " + " H : " + H.ToString() + " S : " + S.ToString() + " V : " + V.ToString());
            int i;
            double f, w, q, t;

            double hue;
            byte value = (byte)(V * 255);
            Color color = new Color();
            //g_return_if_fail(rgb != NULL);
            //g_return_if_fail(hsv != NULL);

            if (S == 0.0)
            {
                color.R = value;
                color.G = value;
                color.B = value;
            }
            else
            {
                hue = H;

                if (hue == 360.0)
                    hue = 0.0;

                hue *= 6.0;

                i = (int)hue;
                f = hue - i;
                w = V * (1.0 - S);
                q = V * (1.0 - (S * f));
                t = V * (1.0 - (S * (1.0 - f)));

                switch (i)
                {
                    case 0:
                        color.R = value;
                        color.G = (byte)(t * 255);
                        color.B = (byte)(w * 255);
                        break;
                    case 1:
                        color.R = (byte)(q * 255);
                        color.G = value;
                        color.B = (byte)(w * 255);
                        break;
                    case 2:
                        color.R = (byte)(w * 255);
                        color.G = value;
                        color.B = (byte)(t * 255);
                        break;
                    case 3:
                        color.R = (byte)(w * 255);
                        color.G = (byte)(q * 255);
                        color.B = value;
                        break;
                    case 4:
                        color.R = (byte)(t * 255);
                        color.G = (byte)(w * 255);
                        color.B = value;
                        break;
                    case 5:
                        color.R = value;
                        color.G = (byte)(w * 255);
                        color.B = (byte)(q * 255);
                        break;
                }
            }
            //Console.WriteLine("FROM HSV " + " R : " + color.R.ToString() + " G : " + color.G.ToString() + " B : " + color.B.ToString());
            return color;
        }

        public static byte Max(Color rgb)
        {
            if (rgb.R > rgb.G)
                return (rgb.R > rgb.B) ? rgb.R : rgb.B;
            else
                return (rgb.G > rgb.B) ? rgb.G : rgb.B;
        }

        public static byte Min(Color rgb)
        {
            if (rgb.R > rgb.G)
                return (rgb.R < rgb.B) ? rgb.R : rgb.B;
            else
                return (rgb.G < rgb.B) ? rgb.G : rgb.B;
        }


        //public static Color ColorFromHSV(double hue, double saturation, double value)
        //{
        //    int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
        //    double f = hue / 60 - Math.Floor(hue / 60);

        //    byte R, G, B;

        //    value = value * 255;
        //    int v = Convert.ToInt32(value);
        //    int pv = Convert.ToInt32(value * (1 - saturation));
        //    int qv = Convert.ToInt32(value * (1 - f * saturation));
        //    int tv = Convert.ToInt32(value * (1 - (1 - f) * saturation));

        //    Console.WriteLine("ColorFromHSV : " + "hi : " + hi.ToString() + "f : " + f.ToString());
        //    Console.WriteLine("ColorFromHSV : " + "v : " + v.ToString() + " p : " + pv.ToString() + " q : " + qv.ToString() + " t : " + tv.ToString());

        //    if (hi == 0)
        //        return Color.FromArgb(255, (byte)v, (byte)tv, (byte)pv);
        //    else if (hi == 1)
        //        return Color.FromArgb(255, (byte)qv, (byte)v, (byte)pv);
        //    else if (hi == 2)
        //        return Color.FromArgb(255, (byte)pv, (byte)v, (byte)tv);
        //    else if (hi == 3)
        //        return Color.FromArgb(255, (byte)pv, (byte)qv, (byte)v);
        //    else if (hi == 4)
        //        return Color.FromArgb(255, (byte)tv, (byte)pv, (byte)v);
        //    else
        //        return Color.FromArgb(255, (byte)v, (byte)pv, (byte)qv);
        //}

        //public static Color ColorFromHSV(double hue, double saturation, double value)
        //{
        //    double H = hue;
        //    double V = value;
        //    double S = saturation;

        //    while (H < 0) { H += 360; };
        //    while (H >= 360) { H -= 360; };
        //    double R, G, B;

        //    if (V <= 0)
        //    {
        //        R = G = B = 0;
        //    }
        //    else if (S <= 0)
        //    {
        //        R = G = B = V;
        //    }
        //    else
        //    {
        //        double hf = H / 60.0;
        //        int i = (int)Math.Floor(hf);
        //        double f = hf - i;
        //        double pv = V * (1 - S);
        //        double qv = V * (1 - S * f);
        //        double tv = V * (1 - S * (1 - f));
        //        switch (i)
        //        {
        //            // Red is the dominant color

        //            case 0:
        //                R = V;
        //                G = tv;
        //                B = pv;
        //                break;

        //            // Green is the dominant color

        //            case 1:
        //                R = qv;
        //                G = V;
        //                B = pv;
        //                break;
        //            case 2:
        //                R = pv;
        //                G = V;
        //                B = tv;
        //                break;

        //            // Blue is the dominant color

        //            case 3:
        //                R = pv;
        //                G = qv;
        //                B = V;
        //                break;
        //            case 4:
        //                R = tv;
        //                G = pv;
        //                B = V;
        //                break;

        //            // Red is the dominant color

        //            case 5:
        //                R = V;
        //                G = pv;
        //                B = qv;
        //                break;

        //            // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

        //            case 6:
        //                R = V;
        //                G = tv;
        //                B = pv;
        //                break;
        //            case -1:
        //                R = V;
        //                G = pv;
        //                B = qv;
        //                break;

        //            // The color is not defined, we should throw an error.

        //            default:
        //                //LFATAL("i Value error in Pixel conversion, Value is %d", i);
        //                R = G = B = V; // Just pretend its black/white
        //                break;
        //        }
        //    }
        //    Color colorOut = new Color();
        //    colorOut.R = (byte)Clamp((int)(R * 255.0));
        //    colorOut.G = (byte)Clamp((int)(G * 255.0));
        //    colorOut.B = (byte)Clamp((int)(B * 255.0));

        //    return colorOut;
        //}

        //public static int Clamp(int i)
        //{
        //    if (i < 0) return 0;
        //    if (i > 255) return 255;
        //    return i;
        //}

    }

}