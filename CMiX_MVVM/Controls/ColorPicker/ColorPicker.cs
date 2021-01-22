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
            Console.WriteLine("ColorPicker OnSelectedColorChanged");
            Console.WriteLine("ColorPicker SelectedColor is " + ((ColorPicker)d).SelectedColor);
        }



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
            Console.WriteLine("ColorPicker OnRedChanged");
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
            Console.WriteLine("ColorPicker OnGreenChanged");
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
            Console.WriteLine("ColorPicker OnBlueChanged");
        }


        public static readonly DependencyProperty HueProperty =
            DependencyProperty.Register("Hue", typeof(double), typeof(ColorPicker), 
            new FrameworkPropertyMetadata(0.0,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnHueChanged));
        public double Hue
        {
            get { return (double)GetValue(HueProperty); }
            set { SetValue(HueProperty, value); }
        }

        private static void OnHueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("ColorPicker OnBlueChanged");
            var colorPicker = (ColorPicker)d;
            var selectedColor = colorPicker.SelectedColor;

            var hsv = new Rgb() { R = selectedColor.R, G = selectedColor.G, B = selectedColor.B }.To<Hsv>();
            hsv.H = (double)e.NewValue;// value;

            var rgb = hsv.To<Rgb>();
            colorPicker.Red = rgb.R;
            colorPicker.Green = rgb.G;
            colorPicker.Blue = rgb.B;

            colorPicker.SelectedColor = Color.FromRgb((byte)colorPicker.Red, (byte)colorPicker.Green, (byte)colorPicker.Blue);
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
            Console.WriteLine("ColorPicker OnSaturationChanged");
            var colorPicker = (ColorPicker)d;
            var selectedColor = colorPicker.SelectedColor;

            var hsv = new Rgb() { R = selectedColor.R, G = selectedColor.G, B = selectedColor.B }.To<Hsv>();
            hsv.S = (double)e.NewValue;// value;

            var rgb = hsv.To<Rgb>();
            colorPicker.Red = rgb.R;
            colorPicker.Green = rgb.G;
            colorPicker.Blue = rgb.B;

            colorPicker.SelectedColor = Color.FromRgb((byte)colorPicker.Red, (byte)colorPicker.Green, (byte)colorPicker.Blue);
        }



        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(ColorPicker),
            new FrameworkPropertyMetadata(0.0,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnValueChanged));
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("ColorPicker OnValueChanged");
            var colorPicker = (ColorPicker)d;
            var selectedColor = colorPicker.SelectedColor;

            var hsv = new Rgb() { R = selectedColor.R, G = selectedColor.G, B = selectedColor.B }.To<Hsv>();
            var value = (double)e.NewValue;

            hsv.V = value;

            var rgb = hsv.To<Rgb>();
            colorPicker.Red = rgb.R;
            colorPicker.Green = rgb.G;
            colorPicker.Blue = rgb.B;
            colorPicker.SelectedColor = Color.FromRgb((byte)colorPicker.Red, (byte)colorPicker.Green, (byte)colorPicker.Blue);
            if (value > 0)
            {

            }
            //else
            //{
            //    colorPicker.SelectedColor = Color.FromRgb(0, 0, 0);
            //}

            
        }
    }
}
