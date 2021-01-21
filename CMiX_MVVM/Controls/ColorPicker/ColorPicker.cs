using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }
        public static readonly DependencyProperty SelectedColorProperty =
        DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorPicker), new UIPropertyMetadata(Colors.Red));

        public double Sat
        {
            get { return (double)GetValue(SatProperty); }
            set { SetValue(SatProperty, value); }
        }
        public static readonly DependencyProperty SatProperty =
        DependencyProperty.Register("Sat", typeof(double), typeof(ColorPicker), new UIPropertyMetadata(1.0));
    }
}
