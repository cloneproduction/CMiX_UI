using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using CMiX.MVVM.Resources;

namespace CMiX.MVVM.Controls.ColorPicker
{
    public class XYColorControl : Control
    {
        static XYColorControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(XYColorControl), new FrameworkPropertyMetadata(typeof(XYColorControl)));
            EventManager.RegisterClassHandler(typeof(XYColorControl), Thumb.DragDeltaEvent, new DragDeltaEventHandler(XYColorControl.OnThumbDragDelta));
        }

        #region Private Members

        private const string ThumbName = "PART_Thumb";

        private TranslateTransform m_thumbTransform = new TranslateTransform();
        private Thumb m_thumb;
        private bool m_withinUpdate = false;

        #endregion

        #region Dependency Properties

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }
        public static readonly DependencyProperty SelectedColorProperty =
        DependencyProperty.Register("SelectedColor", typeof(Color), typeof(XYColorControl),
            new PropertyMetadata((Colors.Transparent), new PropertyChangedCallback(OnSelectedColorChanged)));



        public Point ThumbPosXY
        {
            get { return (Point)GetValue(ThumbPosXYProperty); }
            set { SetValue(ThumbPosXYProperty, value); }
        }
        public static readonly DependencyProperty ThumbPosXYProperty =
        DependencyProperty.Register("ThumbPosXY", typeof(Point), typeof(XYColorControl),
            new PropertyMetadata(new Point(0.0, 0.0), new PropertyChangedCallback(OnThumbPosXYChanged)));
        #endregion

        #region Routed Events
        public static readonly RoutedEvent ThumbPosXYChangedEvent = EventManager.RegisterRoutedEvent(
            "ThumbPosXYChanged",
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<Color>),
            typeof(XYColorControl)
        );

        public event RoutedPropertyChangedEventHandler<Color> ThumbPosXYChanged
        {
            add { AddHandler(ThumbPosXYChangedEvent, value); }
            remove { RemoveHandler(ThumbPosXYChangedEvent, value); }
        }


        public static readonly RoutedEvent SelectedColorChangedEvent = EventManager.RegisterRoutedEvent(
            "SelectedColorChanged",
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<Color>),
            typeof(XYColorControl)
        );

        public event RoutedPropertyChangedEventHandler<Color> SelectedColorChanged
        {
            add { AddHandler(SelectedColorChangedEvent, value); }
            remove { RemoveHandler(SelectedColorChangedEvent, value); }
        }

        #endregion

        #region Event Handlers
        private void OnThumbDragDelta(DragDeltaEventArgs e)
        {
            double offsetX = m_thumbTransform.X + e.HorizontalChange;
            double offsetY = m_thumbTransform.Y + e.VerticalChange;
            UpdatePositionAndSaturationAndValue(offsetX, offsetY);
        }

        private static void OnThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            XYColorControl hsvControl = sender as XYColorControl;
            hsvControl.OnThumbDragDelta(e);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (m_thumb != null)
            {
                Point position = e.GetPosition(this);
                UpdatePositionAndSaturationAndValue(position.X, position.Y);
                m_thumb.RaiseEvent(e);
            }
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            UpdateThumbPosition();
            base.OnRenderSizeChanged(sizeInfo);
        }

        private static void OnSelectedColorChanged(DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
        {
            XYColorControl hsvControl = relatedObject as XYColorControl;
            if (hsvControl != null && !hsvControl.m_withinUpdate)
            {
                hsvControl.UpdateThumbPosition();
            }
        }

        private static void OnThumbPosXYChanged(DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
        {
            XYColorControl hsvControl = relatedObject as XYColorControl;
            if (hsvControl != null && !hsvControl.m_withinUpdate)
            {
                hsvControl.UpdateThumbPosition();
            }
        }
        #endregion

        #region Private Methods
        private double LimitValue(double value, double max)
        {
            if (value < 0)
                value = 0;
            if (value > max)
                value = max;
            return value;
        }


        private void UpdatePositionAndSaturationAndValue(double positionX, double positionY)
        {
            positionX = LimitValue(positionX, ActualWidth);
            positionY = LimitValue(positionY, ActualHeight);

            m_thumbTransform.X = positionX;
            m_thumbTransform.Y = positionY;

            double hue, sat, val, none;
            ColorUtils.ColorToHSV(SelectedColor, out hue, out none, out none);

            sat = positionX / ActualWidth;
            val = 1 - positionY / ActualHeight;

            SelectedColor = ColorUtils.ColorFromHSV(hue, sat, val);
        }


        private void UpdateThumbPosition()
        {
            double hue, sat, val;

            ColorUtils.ColorToHSV(SelectedColor, out hue, out sat, out val);

            m_thumbTransform.X = sat * ActualWidth;
            m_thumbTransform.Y = (1 - val) * ActualHeight;
        }
        #endregion


        #region Overridden Members
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            m_thumb = GetTemplateChild(ThumbName) as Thumb;
            if (m_thumb != null)
            {
                UpdateThumbPosition();
                m_thumb.RenderTransform = m_thumbTransform;
            }
        }
        #endregion
    }

}


//private void UpdateSelectedColor()
//{
//    //Color oldColor = SelectedColor;
//    //Color newColor = ColorUtils.ConvertHsvToRgb(Hue, Saturation, Value);
//    //SelectedColor = newColor;
//    //ColorUtils.FireSelectedColorChangedEvent(this, SelectedColorChangedEvent, oldColor, newColor);
//}


//private static void OnHueChanged(
//    DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
//{
//    HsvControl hsvControl = relatedObject as HsvControl;
//    if (hsvControl != null && !hsvControl.m_withinUpdate)
//        hsvControl.UpdateSelectedColor();
//}

//private static void OnSaturationChanged(
//    DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
//{
//    HsvControl hsvControl = relatedObject as HsvControl;
//    if (hsvControl != null && !hsvControl.m_withinUpdate)
//        hsvControl.UpdateThumbPosition();
//}

//private static void OnValueChanged(
//    DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
//{
//    HsvControl hsvControl = relatedObject as HsvControl;
//    if (hsvControl != null && !hsvControl.m_withinUpdate)
//        hsvControl.UpdateThumbPosition();
//}

//public double Hue
//{
//    get { return (double)GetValue(HueProperty); }
//    set { SetValue(HueProperty, value); }
//}
//public static readonly DependencyProperty HueProperty =
//    DependencyProperty.Register("Hue", typeof(double), typeof(HsvControl),
//    new UIPropertyMetadata((double)0, new PropertyChangedCallback(OnHueChanged)));

//public double Saturation
//{
//    get { return (double)GetValue(SaturationProperty); }
//    set { SetValue(SaturationProperty, value); }
//}
//public static readonly DependencyProperty SaturationProperty =
//    DependencyProperty.Register("Saturation", typeof(double), typeof(HsvControl),
//    new UIPropertyMetadata((double)0, new PropertyChangedCallback(OnSaturationChanged)));

//public double Value
//{
//    get { return (double)GetValue(ValueProperty); }
//    set { SetValue(ValueProperty, value); }
//}
//public static readonly DependencyProperty ValueProperty =
//    DependencyProperty.Register("Value", typeof(double), typeof(HsvControl),
//        new UIPropertyMetadata((double)0, new PropertyChangedCallback(OnValueChanged)));