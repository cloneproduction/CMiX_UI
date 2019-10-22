using CMiX.MVVM.Resources;
using CMiX.MVVM.Resources;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CMiX.MVVM.Controls
{
    public sealed class HueWheel : Slider
    {
        public HueWheel()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(HueWheel), new FrameworkPropertyMetadata(typeof(HueWheel)));
        }

        private bool _isPressed = false;
        private bool m_withinChanging = false;

        #region Dependency Properties
        public static readonly DependencyProperty HueProperty =
            DependencyProperty.Register("Hue", typeof(double), typeof(HueWheel),
            new UIPropertyMetadata((double)0, new PropertyChangedCallback(OnHuePropertyChanged)));
        public double Hue
        {
            get { return (double)GetValue(HueProperty); }
            set { SetValue(HueProperty, value); }
        }


        #endregion

        #region Protected Methods

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            if (!m_withinChanging && !BindingOperations.IsDataBound(this, HueProperty))
            {
                m_withinChanging = true;
                Hue = 360 - newValue;
                m_withinChanging = false;
            }
        }

        #endregion

        #region Private Methods
        private static void OnHuePropertyChanged(DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
        {
            HueWheel hueWheel = relatedObject as HueWheel;

            if (hueWheel != null && !hueWheel.m_withinChanging)
            {
                hueWheel.m_withinChanging = true;
                double hue = (double)e.NewValue;
                hueWheel.Value = hue;
                hueWheel.m_withinChanging = false;
            }
        }

        #endregion


        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_isPressed)
            {
                const double RADIUS = 150;
                Point newPos = e.GetPosition(this);
                double angle = MyHelper.GetAngleR(newPos, RADIUS);
                this.Value = (this.Maximum - this.Minimum) * angle / (2 * Math.PI);
                Hue = this.Value;
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            Mouse.Capture(this);
            AddHandler();
            _isPressed = true;

            if (_isPressed)
            {
                const double RADIUS = 150;
                Point newPos = e.GetPosition(this);
                double angle = MyHelper.GetAngleR(newPos, RADIUS);
                this.Value = (this.Maximum - this.Minimum) * angle / (2 * Math.PI);
                Hue = this.Value;
            }

        }

        private void AddHandler()
        {
            AddHandler(Mouse.PreviewMouseUpOutsideCapturedElementEvent, new MouseButtonEventHandler(HandleClickOutsideOfControl), true);
        }

        private void HandleClickOutsideOfControl(object sender, MouseButtonEventArgs e)
        {
            _isPressed = false;
            ReleaseMouseCapture();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            _isPressed = false;
        }
    }

    //public class ValueTextConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter,
    //              System.Globalization.CultureInfo culture)
    //    {
    //        double v = (double)value;
    //        return String.Format("{0:F2}", v);
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter,
    //        System.Globalization.CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}


}
