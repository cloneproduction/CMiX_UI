using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using CMiX.MVVM.Resources;

namespace CMiX.ColorPicker
{
    public class HueSlider : Slider
    {
        #region Public Methods

        static HueSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HueSlider), new FrameworkPropertyMetadata(typeof(HueSlider)));
        }

        public HueSlider()
        {

        }

        #endregion

        #region Dependency Properties

        public double Hue
        {
            get { return (double)GetValue(HueProperty); }
            set { SetValue(HueProperty, value); }
        }

        public static readonly DependencyProperty HueProperty =
            DependencyProperty.Register("Hue", typeof(double), typeof(HueSlider),
                new UIPropertyMetadata((double)0, new PropertyChangedCallback(OnHuePropertyChanged)));

        #endregion

        #region Private Members

        private bool m_withinChanging = false;

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


        private static void OnHuePropertyChanged(
            DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
        {
            HueSlider spectrumSlider = relatedObject as HueSlider;
            if (spectrumSlider != null && !spectrumSlider.m_withinChanging)
            {
                spectrumSlider.m_withinChanging = true;
                double hue = (double)e.NewValue;
                spectrumSlider.Value = 361 - hue;
                spectrumSlider.m_withinChanging = false;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_isPressed)
            {
                Point position = e.GetPosition(this);
                double d = 0.0;
                if (this.Orientation == Orientation.Vertical)
                {
                    d = -(1.0d / this.ActualHeight * position.Y) + 1.0;
                }

                else if (this.Orientation == Orientation.Horizontal)
                {
                    d = (1.0d / this.ActualWidth * position.X);
                }
                var p = this.Maximum * d;

                if (p >= this.Maximum)
                    p = this.Maximum;
                if (p <= this.Minimum)
                    p = this.Minimum;

                this.Value = p;
            }
        }
        #endregion

        private bool _isPressed = false;

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            Mouse.Capture(this);
            AddHandler();

            _isPressed = true;
            if (_isPressed)
            {
                Point position = e.GetPosition(this);
                double d = 0.0;

                if (this.Orientation == Orientation.Vertical)
                     d = -(1.0d / this.ActualHeight * position.Y) + 1.0;

                else if(this.Orientation == Orientation.Horizontal)
                    d = (1.0d / this.ActualWidth * position.X);

                var p = this.Maximum * d;

                if (p >= this.Maximum)
                    p = this.Maximum;
                if (p <= this.Minimum)
                    p = this.Minimum;

                this.Value = p;
            }
            e.Handled = true;
            base.OnPreviewMouseLeftButtonDown(e);
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

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
            _isPressed = false;
        }
    }
}