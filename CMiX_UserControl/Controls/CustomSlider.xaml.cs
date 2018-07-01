using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace CMiX.Controls
{
    public partial class CustomSlider : UserControl
    {
        public CustomSlider()
        {
            InitializeComponent();

            CMiXSlider.ApplyTemplate();
            Thumb thumb = (CMiXSlider.Template.FindName("PART_Track", CMiXSlider) as Track).Thumb;
            thumb.MouseEnter += new MouseEventHandler(thumb_MouseEnter);
        }

        #region Properties
        public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(double), typeof(CustomSlider), new PropertyMetadata(0.0));
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
        DependencyProperty.Register("Orientation", typeof(string), typeof(CustomSlider), new PropertyMetadata("Horizontal"));
        public string Orientation
        {
            get { return (string)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty =
        DependencyProperty.Register("Caption", typeof(string), typeof(CustomSlider), new PropertyMetadata(""));
        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }
        #endregion

        private void thumb_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.MouseDevice.Captured == null)
            {
                MouseButtonEventArgs args = new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.Left);
                args.RoutedEvent =  MouseLeftButtonDownEvent;
                (sender as Thumb).RaiseEvent(args);
            }
        }

        private void AddToSlider_Click(object sender, RoutedEventArgs e)
        {
            Value += CMiXSlider.SmallChange;
            if (Value >= CMiXSlider.Maximum)
            {
                Value = CMiXSlider.Maximum;
            }
        }

        private void SubToSlider_Click(object sender, RoutedEventArgs e)
        {
            Value -= CMiXSlider.SmallChange;
            if (Value <= CMiXSlider.Minimum)
            {
                Value = CMiXSlider.Minimum;
            }
        }

        public event EventHandler SliderValueChanged;
        protected virtual void OnSliderValueChanged(EventArgs e)
        {
            var handler = SliderValueChanged;
            if (handler != null)
                handler(this, e);
        }

        private void CMiXSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            OnSliderValueChanged(e);
        }

        private void CMiXSlider_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Add)
            {
                if((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                {
                    Value += CMiXSlider.LargeChange;
                }
                else if ((Keyboard.Modifiers & ModifierKeys.None) == ModifierKeys.None)
                {
                    Value += CMiXSlider.SmallChange;
                }

            }
            if (e.Key == Key.Subtract)
            {
                if((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                {
                    Value -= CMiXSlider.LargeChange;
                }
                else if ((Keyboard.Modifiers & ModifierKeys.None) == ModifierKeys.None)
                {
                    Value -= CMiXSlider.SmallChange;
                }
            }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            //SliderPopup.IsOpen = true;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            //SliderPopup.IsOpen = false;
        }
    }
}
