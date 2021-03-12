using ColorMine.ColorSpaces;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CMiX.MVVM.Controls
{
    public class ColorPicker : Control
    {
        static ColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPicker), new FrameworkPropertyMetadata(typeof(ColorPicker)));
        }

        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorPicker),
            new FrameworkPropertyMetadata(Colors.Red,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnSelectedColorChanged));
        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        private static void OnSelectedColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Console.WriteLine("ColorPicker OnSelectedColorChanged");
        }


        //private bool IsUpdatingRGB = false;
        //private bool IsUpdatingHSV = false;


        public static readonly DependencyProperty RedProperty =
            DependencyProperty.Register("Red", typeof(double), typeof(ColorPicker), 
            new FrameworkPropertyMetadata(1.0, 
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnRedChanged));
        public double Red
        {
            get { return (double)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }

        private static void OnRedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorPicker colorPicker)
                colorPicker.OnRedChanged((double)e.OldValue, (double)e.NewValue);
        }

        private void OnRedChanged(double oldValue, double newValue)
        {
            //IsUpdatingRGB = true;
            //if (oldValue != newValue)
            //{
            //    if (!double.IsNaN(newValue))
            //    {
            //        if (!IsUpdatingHSV)
            //        {
            //            var rgb = new Rgb() { R = newValue, G = SelectedColor.G, B = SelectedColor.B };
            //            var hsv = rgb.To<Hsv>();
            //            Hue = hsv.H;
            //            Saturation = hsv.S;
            //            Value = hsv.V;
            //            SelectedColor = Color.FromRgb((byte)rgb.R, (byte)rgb.G, (byte)rgb.B);
            //        }
            //    }
            //}
            //IsUpdatingRGB = false;
        }


        public static readonly DependencyProperty GreenProperty =
            DependencyProperty.Register("Green", typeof(double), typeof(ColorPicker),
            new FrameworkPropertyMetadata(1.0,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnGreenChanged));
        public double Green
        {
            get { return (double)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }

        private static void OnGreenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorPicker colorPicker)
                colorPicker.OnGreenChanged((double)e.OldValue, (double)e.NewValue);        
        }



        private void OnGreenChanged(double oldValue, double newValue)
        {
            //IsUpdatingRGB = true;
            //if (oldValue != newValue)
            //{
            //    if (!double.IsNaN(newValue))
            //    {
            //        if (!IsUpdatingHSV)
            //        {
            //            var rgb = new Rgb() { R = SelectedColor.R, G = newValue, B = SelectedColor.B };
            //            var hsv = rgb.To<Hsv>();
            //            Hue = hsv.H;
            //            Saturation = hsv.S;
            //            Value = hsv.V;
            //            SelectedColor = Color.FromRgb((byte)rgb.R, (byte)rgb.G, (byte)rgb.B);
            //        }
            //    }
            //}
            //IsUpdatingRGB = false;
        }

        public static readonly DependencyProperty BlueProperty =
            DependencyProperty.Register("Blue", typeof(double), typeof(ColorPicker),
            new FrameworkPropertyMetadata(1.0,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnBlueChanged));
        public double Blue
        {
            get { return (double)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }

        private static void OnBlueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorPicker colorPicker)
                colorPicker.OnBlueChanged((double)e.OldValue, (double)e.NewValue);
        }

        private void OnBlueChanged(double oldValue, double newValue)
        {
            //IsUpdatingRGB = true;

            //if (oldValue != newValue)
            //{
            //    if (!double.IsNaN(newValue))
            //    {
            //        if (!IsUpdatingHSV)
            //        {
            //            var hsv = new Rgb() { R = SelectedColor.R, G = SelectedColor.G, B = newValue }.To<Hsv>();
            //            Hue = hsv.H;
            //            Saturation = hsv.S;
            //            Value = hsv.V;
            //            SelectedColor = Color.FromRgb(SelectedColor.R, SelectedColor.G, (byte)newValue);
            //        }
            //    }
            //}
            //IsUpdatingRGB = false;
        }

        public static readonly DependencyProperty HueProperty =
            DependencyProperty.Register("Hue", typeof(double), typeof(ColorPicker), 
            new FrameworkPropertyMetadata(250.0,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnHueChanged));
        public double Hue
        {
            get { return (double)GetValue(HueProperty); }
            set { SetValue(HueProperty, value); }
        }

        private static void OnHueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorPicker colorPicker)
                colorPicker.OnHueChanged((double)e.OldValue, (double)e.NewValue);
        }


        private void OnHueChanged(double oldValue, double newValue)
        {
            //IsUpdatingHSV = true;

            //if (!double.IsNaN(newValue) && newValue != oldValue)
            //{
            //    var hsv = new Rgb() { R = SelectedColor.R, G = SelectedColor.G, B = SelectedColor.B }.To<Hsv>();
            //    hsv.H = newValue;

            //    if (!IsUpdatingRGB)
            //    {
            //        var rgb = hsv.To<Rgb>();
            //        Red = rgb.R;
            //        Green = rgb.G;
            //        Blue = rgb.B;
            //        SelectedColor = Color.FromRgb((byte)rgb.R, (byte)rgb.G, (byte)rgb.B);
            //    }
            //}

            //IsUpdatingHSV = false;
        }


        public static readonly DependencyProperty SaturationProperty =
            DependencyProperty.Register("Saturation", typeof(double), typeof(ColorPicker),
            new FrameworkPropertyMetadata(0.0,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnSaturationChanged));
        public double Saturation
        {
            get { return (double)GetValue(SaturationProperty); }
            set { SetValue(SaturationProperty, value); }
        }

        private static void OnSaturationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorPicker colorPicker)
                colorPicker.OnSaturationChanged((double)e.OldValue, (double)e.NewValue);
        }

        private void OnSaturationChanged(double oldValue, double newValue)
        {
            //IsUpdatingHSV = true;

            //if (!double.IsNaN(newValue))
            //{
            //    var hsv = new Rgb() { R = SelectedColor.R, G = SelectedColor.G, B = SelectedColor.B }.To<Hsv>();
            //    hsv.S = newValue;

            //    if (!IsUpdatingRGB)
            //    {
            //        var rgb = hsv.To<Rgb>();
            //        Red = rgb.R;
            //        Green = rgb.G;
            //        Blue = rgb.B;
            //        SelectedColor = Color.FromRgb((byte)rgb.R, (byte)rgb.G, (byte)rgb.B);
            //    }
            //}

            //IsUpdatingHSV = false;
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(ColorPicker),
            new FrameworkPropertyMetadata(1.0,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnValueChanged));
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }


        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorPicker colorPicker)
                colorPicker.OnValueChanged((double)e.OldValue, (double)e.NewValue);
        }

        private void OnValueChanged(double oldValue, double newValue)
        {
            //IsUpdatingHSV = true;

            //if (!double.IsNaN(newValue))
            //{
            //    var hsv = new Rgb() { R = SelectedColor.R, G = SelectedColor.G, B = SelectedColor.B }.To<Hsv>();
            //    hsv.V = newValue;

            //    if (!IsUpdatingRGB)
            //    {
            //        var rgb = hsv.To<Rgb>();

            //        Red = rgb.R;
            //        Green = rgb.G;
            //        Blue = rgb.B;

            //        SelectedColor = Color.FromRgb((byte)rgb.R, (byte)rgb.G, (byte)rgb.B);
            //    }
            //}

            //IsUpdatingHSV = false;
        }
    }
}
