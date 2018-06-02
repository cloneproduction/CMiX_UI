using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace ColorPicker
{
    public class SpectrumSlider : Slider
    {
        #region Public Methods

        static SpectrumSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SpectrumSlider), new FrameworkPropertyMetadata(typeof(SpectrumSlider)));
        }

        public SpectrumSlider()
        {
            SetBackground();
        }

        #endregion

        #region Dependency Properties

        public double Hue
        {
            get { return (double)GetValue(HueProperty); }
            set { SetValue(HueProperty, value); }
        }

        public static readonly DependencyProperty HueProperty =
            DependencyProperty.Register("Hue", typeof(double), typeof(SpectrumSlider),
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

        private void SetBackground()
        {
            LinearGradientBrush backgroundBrush = new LinearGradientBrush();
            backgroundBrush.StartPoint = new Point(1, 0.5);
            backgroundBrush.EndPoint = new Point(0, 0.5);

            const int spectrumColorCount = 30;

            Color[] spectrumColors = ColorUtils.GetSpectrumColors(spectrumColorCount);
            for (int i = 0; i < spectrumColorCount; ++i)
            {
                double offset = i * 1.0 / spectrumColorCount;
                GradientStop gradientStop = new GradientStop(spectrumColors[i], offset);
                backgroundBrush.GradientStops.Add(gradientStop);
            }
            Background = backgroundBrush;
        }

        private static void OnHuePropertyChanged(
            DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
        {
            SpectrumSlider spectrumSlider = relatedObject as SpectrumSlider;
            if (spectrumSlider != null && !spectrumSlider.m_withinChanging)
            {
                spectrumSlider.m_withinChanging = true;
                double hue = (double)e.NewValue;
                spectrumSlider.Value = 360 - hue;
                spectrumSlider.m_withinChanging = false;
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
                double d = 1.0d / this.ActualWidth * position.X;
                var p = this.Maximum * d;
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

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_isPressed)
            {
                Point position = e.GetPosition(this);
                double d = 1.0d / this.ActualWidth * position.X;
                var p = this.Maximum * d;
                this.Value = p;
            }
        }
    }
}