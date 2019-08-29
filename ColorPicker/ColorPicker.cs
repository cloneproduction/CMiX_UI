using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using CMiX.MVVM.Resources;


namespace CMiX.ColorPicker
{
    [ValueConversion(typeof(double), typeof(String))]
    public class DoubleToIntegerStringConverter : IValueConverter
    {
        public Object Convert(
            Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            double doubleValue = (double)value;
            int intValue = (int)doubleValue;

            return intValue.ToString();
        }
        public Object ConvertBack(
            Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            String stringValue = (String)value;
            double doubleValue = 0;
            if (!Double.TryParse(stringValue, out doubleValue))
                doubleValue = 0;

            return doubleValue;
        }
    }

    public class ColorPicker : Control
    {
        #region Private Members

        private const string RedColorSliderName = "PART_RedColorSlider";
        private const string GreenColorSliderName = "PART_GreenColorSlider";
        private const string BlueColorSliderName = "PART_BlueColorSlider";
        private const string AlphaColorSliderName = "PART_AlphaColorSlider";

        private const string HsvControlName = "PART_HsvControl";
        private const string HueWheelName = "PART_HueWheel";
        private const string SatSliderName = "PART_SatSlider";
        private const string ValSliderName = "PART_ValSlider";
        private const string HueSliderName = "PART_HueSlider";
        private const string ColorHexName = "PART_ColorHex";
        private const string ColorPickerName = "PART_ColorPicker";

        private ColorSlider m_redColorSlider;
        private ColorSlider m_greenColorSlider;
        private ColorSlider m_blueColorSlider;
        private ColorSlider m_alphaColorSlider;
        private HSVSlider m_satSlider;
        private HSVSlider m_valSlider;
        private HueSlider m_hueSlider;
        private ColorHex m_colorHex;

        private HsvControl m_hsvControl;
        private HueWheel m_hueWheel;

        private bool m_withinChange;
        private bool m_templateApplied;
        #endregion

        #region Dependency Properties

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorPicker),
            new UIPropertyMetadata(Colors.Pink, new PropertyChangedCallback(OnSelectedColorPropertyChanged)));

        public bool FixedSliderColor
        {
            get { return (bool)GetValue(FixedSliderColorProperty); }
            set { SetValue(FixedSliderColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FixedSliderColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FixedSliderColorProperty =
            DependencyProperty.Register("FixedSliderColor", typeof(bool), typeof(ColorSlider),
            new UIPropertyMetadata(false, new PropertyChangedCallback(OnFixedSliderColorPropertyChanged)));

        #endregion

        #region Public Methods

        static ColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPicker), new FrameworkPropertyMetadata(typeof(ColorPicker)));
            
            // Register Event Handler for slider
            EventManager.RegisterClassHandler(typeof(ColorPicker), ColorSlider.ValueChangedEvent, new RoutedPropertyChangedEventHandler<double>(ColorPicker.OnSliderValueChanged));

            // Register Event Handler for Hsv Control
            EventManager.RegisterClassHandler(typeof(ColorPicker), HsvControl.SelectedColorChangedEvent, new RoutedPropertyChangedEventHandler<Color>(ColorPicker.OnHsvControlSelectedColorChanged));
        }

        #endregion

        #region Routed Events

        public static readonly RoutedEvent SelectedColorChangedEvent = EventManager.RegisterRoutedEvent(
            "SelectedColorChanged",
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<Color>),
            typeof(ColorPicker)
        );

        public event RoutedPropertyChangedEventHandler<Color> SelectedColorChanged
        {
            add { AddHandler(SelectedColorChangedEvent, value); }
            remove { RemoveHandler(SelectedColorChangedEvent, value); }
        }

        #endregion

        #region Event Handling

        private void OnSliderValueChanged(RoutedPropertyChangedEventArgs<double> e)
        {
            // Avoid endless loop
            if (m_withinChange)
                return;

            m_withinChange = true;
            if (e.OriginalSource == m_redColorSlider ||
                e.OriginalSource == m_greenColorSlider ||
                e.OriginalSource == m_blueColorSlider ||
                e.OriginalSource == m_alphaColorSlider)
            {
                double hue = m_hueSlider.Hue;
                UpdateHueWheel(hue);
                Color newColor = GetRgbColor();
                UpdateHsvControlColor(newColor);
                UpdateRgbSlider(newColor);
                UpdateHsvSliderColor(newColor);
                UpdateSelectedColor(newColor);
            }
            else if (e.OriginalSource == m_hueWheel)
            {
                double hue = m_hueWheel.Hue;
                UpdateHsvControlHue(hue);
                Color newColor = GetHsvControlColor();

                UpdateRgbSlider(newColor);
                UpdateHsvSliderColor(newColor);
                UpdateSelectedColor(newColor);
            }
            else if (e.OriginalSource == m_satSlider || e.OriginalSource == m_valSlider || e.OriginalSource == m_hueSlider)
            {
                double hue = m_hueSlider.Hue;
                Color newColor = GetHsvSliderColor();
                UpdateHueWheel(hue);
                UpdateHsvControlColor(newColor);
                UpdateHsvControlHue(hue);
                UpdateRgbSlider(newColor);
                UpdateSelectedColor(newColor);
            }
            m_withinChange = false;
        }

        private static void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ColorPicker colorPicker = (ColorPicker)sender;
            colorPicker.OnSliderValueChanged(e);
        }

        private void OnHsvControlSelectedColorChanged(RoutedPropertyChangedEventArgs<Color> e)
        {
            // Avoid endless loop
            if (m_withinChange)
                return;

            m_withinChange = true;

            Color newColor = GetHsvControlColor();
            UpdateRgbSlider(newColor);
            UpdateHsvSliderColor(newColor);
            UpdateSelectedColor(newColor);

            m_withinChange = false;
        }

        private static void OnHsvControlSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            ColorPicker colorPicker = sender as ColorPicker;
            colorPicker.OnHsvControlSelectedColorChanged(e);
        }

        private void OnSelectedColorPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (!m_templateApplied)
                return;

            // Avoid endless loop
            if (m_withinChange)
                return;

            m_withinChange = true;

            Color newColor = (Color)e.NewValue;
            UpdateControlColors(newColor);
            m_withinChange = false;
        }

        private static void OnSelectedColorPropertyChanged(DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
        {
            ColorPicker colorPicker = relatedObject as ColorPicker;
            colorPicker.OnSelectedColorPropertyChanged(e);
        }

        private static void OnFixedSliderColorPropertyChanged(DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
        {
            ColorPicker colorPicker = relatedObject as ColorPicker;
            colorPicker.UpdateColorSlidersBackground();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            m_redColorSlider = GetTemplateChild(RedColorSliderName) as ColorSlider;
            m_greenColorSlider = GetTemplateChild(GreenColorSliderName) as ColorSlider;
            m_blueColorSlider = GetTemplateChild(BlueColorSliderName) as ColorSlider;
            m_alphaColorSlider = GetTemplateChild(AlphaColorSliderName) as ColorSlider;

            m_satSlider = GetTemplateChild(SatSliderName) as HSVSlider;
            m_valSlider = GetTemplateChild(ValSliderName) as HSVSlider;

            m_colorHex = GetTemplateChild(ColorHexName) as ColorHex;

            m_hsvControl = GetTemplateChild(HsvControlName) as HsvControl;
            m_hueWheel = GetTemplateChild(HueWheelName) as HueWheel;
            m_hueSlider = GetTemplateChild(HueSliderName) as HueSlider;
            m_templateApplied = true;
            UpdateControlColors(SelectedColor);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == UIElement.IsVisibleProperty && (bool)e.NewValue == true)
                Focus();
            base.OnPropertyChanged(e);
        }

        #endregion

        #region Private Methods

        private void SetColorSliderBackground(ColorSlider colorSlider, Color leftColor, Color rightColor)
        {
            colorSlider.LeftColor = leftColor;
            colorSlider.RightColor = rightColor;
        }

        private void SetSatSliderBackground(HSVSlider colorSlider, Color leftColor, Color rightColor)
        {
            double hueLeft, saturationLeft, valueLeft;
            double hueRight, saturationRight, valueRight;
            ColorUtils.ConvertRgbToHsv(leftColor, out hueLeft, out saturationLeft, out valueLeft);
            if (saturationLeft != 0 && valueLeft != 0)
            {
                colorSlider.LeftColor = ColorUtils.ConvertHsvToRgb(hueLeft, 0.0, valueLeft);
            }
            ColorUtils.ConvertRgbToHsv(rightColor, out hueRight, out saturationRight, out valueRight);
            if (saturationRight != 0 && valueRight != 0)
            {
                colorSlider.RightColor = ColorUtils.ConvertHsvToRgb(hueRight, 1.0, valueRight);
            }
        }

        private void SetValSliderBackground(HSVSlider colorSlider, Color leftColor, Color rightColor)
        {
            double hueLeft, saturationLeft, valueLeft;
            double hueRight, saturationRight, valueRight;
            ColorUtils.ConvertRgbToHsv(leftColor, out hueLeft, out saturationLeft, out valueLeft);
            if (saturationLeft != 0 && valueLeft != 0)
            {
                colorSlider.LeftColor = ColorUtils.ConvertHsvToRgb(hueLeft, saturationLeft, 0.0);
            }
            ColorUtils.ConvertRgbToHsv(rightColor, out hueRight, out saturationRight, out valueRight);
            if (saturationRight != 0 && valueRight != 0)
            {
                colorSlider.RightColor = ColorUtils.ConvertHsvToRgb(hueRight, saturationRight, 1.0);
            }
        }

        private void UpdateColorSlidersBackground()
        {
            if (FixedSliderColor)
            {
                SetColorSliderBackground(m_redColorSlider, Colors.Red, Colors.Red);
                SetColorSliderBackground(m_greenColorSlider, Colors.Green, Colors.Green);
                SetColorSliderBackground(m_blueColorSlider, Colors.Blue, Colors.Blue);
                SetColorSliderBackground(m_alphaColorSlider, Colors.Transparent, Colors.White);
                SetSatSliderBackground(m_satSlider, Colors.Black, Colors.White);
                SetValSliderBackground(m_valSlider, Colors.Black, Colors.White);
            }
            else
            {
                byte red = SelectedColor.R;
                byte green = SelectedColor.G;
                byte blue = SelectedColor.B;
                SetColorSliderBackground(m_redColorSlider, Color.FromRgb(0, green, blue), Color.FromRgb(255, green, blue));
                SetColorSliderBackground(m_greenColorSlider, Color.FromRgb(red, 0, blue), Color.FromRgb(red, 255, blue));
                SetColorSliderBackground(m_blueColorSlider, Color.FromRgb(red, green, 0), Color.FromRgb(red, green, 255));
                SetColorSliderBackground(m_alphaColorSlider, Color.FromArgb(0, red, green, blue), Color.FromArgb(255, red, green, blue));
                SetSatSliderBackground(m_satSlider, Color.FromRgb(red, green, blue), Color.FromRgb(red, green, blue));
                SetValSliderBackground(m_valSlider, Colors.Black, Color.FromRgb(red, green, blue));
            }
        }

        private Color GetRgbColor()
        {
            return Color.FromArgb(
                (byte)m_alphaColorSlider.Value,
                (byte)m_redColorSlider.Value,
                (byte)m_greenColorSlider.Value,
                (byte)m_blueColorSlider.Value);
        }

        private Color GetHsvSliderColor()
        {
            Color hsvSliderColor = ColorUtils.ConvertHsvToRgb(m_hueSlider.Hue, m_satSlider.Value/255, m_valSlider.Value/255);
            hsvSliderColor.A = (byte)m_alphaColorSlider.Value;
            return hsvSliderColor;
        }

        private Color GetHsvControlColor()
        {
            Color hsvControlColor = m_hsvControl.SelectedColor;
            hsvControlColor.A = (byte)m_alphaColorSlider.Value;
            return hsvControlColor;
        }

        private void UpdateRgbSlider(Color newColor)
        {
            m_alphaColorSlider.Value = newColor.A;
            m_redColorSlider.Value = newColor.R;
            m_greenColorSlider.Value = newColor.G;
            m_blueColorSlider.Value = newColor.B;
        }

        private void UpdateHsvControlHue(double hue)
        {
            m_hsvControl.Hue = hue;
        }

        private void UpdateHueWheel(double hue)
        {
            //m_hueWheel.Hue = hue;
        }

        private void UpdateHsvSliderColor(Color newColor)
        {
            double hue, saturation, value;
            ColorUtils.ConvertRgbToHsv(newColor, out hue, out saturation, out value);
            m_satSlider.Value = saturation*255;
            m_valSlider.Value = value*255;
            if (saturation != 0 && value != 0)
            {
                m_hueSlider.Hue = hue;
            }

        }

        private void UpdateHsvControlColor(Color newColor)
        {
            double hue, saturation, value;

            ColorUtils.ConvertRgbToHsv(newColor, out hue, out saturation, out value);

            // if saturation == 0 or value == 1 hue don't count so we save the old hue
            if (saturation != 0 && value != 0)
                m_hsvControl.Hue = hue;

            m_hsvControl.Saturation = saturation;
            m_hsvControl.Value = value;
        }

        private void UpdateSelectedColor(Color newColor)
        {
            Color oldColor = SelectedColor;
            SelectedColor = newColor;
            if (!FixedSliderColor)
            {
                UpdateColorSlidersBackground();
            }
            ColorUtils.FireSelectedColorChangedEvent(this, SelectedColorChangedEvent, oldColor, newColor);
        }

        private void UpdateControlColors(Color newColor)
        {
            UpdateRgbSlider(newColor);
            UpdateHsvControlColor(newColor);
            UpdateColorSlidersBackground();
            UpdateHsvSliderColor(newColor);
        }

        #endregion
    }
}