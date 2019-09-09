using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using CMiX.MVVM.Resources;

namespace CMiX.ColorPicker
{
    public class XYController : Control
    {
        static XYController()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(XYController), new FrameworkPropertyMetadata(typeof(XYController)));
            EventManager.RegisterClassHandler(typeof(XYController), Thumb.DragDeltaEvent, new DragDeltaEventHandler(XYController.OnThumbDragDelta));
        }

        #region Private Members
        private const string ThumbName = "PART_Thumb";
        private TranslateTransform m_thumbTransform = new TranslateTransform();
        private Thumb m_thumb;
        #endregion


        #region Dependency Properties
        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }
        public static readonly DependencyProperty SelectedColorProperty =
        DependencyProperty.Register("SelectedColor", typeof(Color), typeof(XYController),
            new PropertyMetadata(Colors.Transparent));


        public double ThumbPosX
        {
            get { return (double)GetValue(ThumbPosXProperty); }
            set { SetValue(ThumbPosXProperty, value); }
        }
        public static readonly DependencyProperty ThumbPosXProperty =
        DependencyProperty.Register("ThumbPosX", typeof(double), typeof(XYController),
            new PropertyMetadata(0.0, new PropertyChangedCallback(OnThumbPosChanged)));


        public double ThumbPosY
        {
            get { return (double)GetValue(ThumbPosYProperty); }
            set { SetValue(ThumbPosYProperty, value); }
        }
        public static readonly DependencyProperty ThumbPosYProperty =
        DependencyProperty.Register("ThumbPosY", typeof(double), typeof(XYController),
            new PropertyMetadata(0.0, new PropertyChangedCallback(OnThumbPosChanged)));


        private static void OnThumbPosChanged(DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
        {
            XYController xycontroller = relatedObject as XYController;
            if (xycontroller != null)
            {
                xycontroller.m_thumbTransform.X = xycontroller.ThumbPosX * xycontroller.ActualWidth;
                xycontroller.m_thumbTransform.Y = xycontroller.ThumbPosY * xycontroller.ActualHeight;
            }
        }
        #endregion

        private void UpdatePosition(double positionX, double positionY)
        {
            positionX = LimitValue(positionX, ActualWidth);
            positionY = LimitValue(positionY, ActualHeight);

            m_thumbTransform.X = positionX;
            m_thumbTransform.Y = positionY;

            ThumbPosX = map(positionX, 0, ActualWidth, 0, 1);
            ThumbPosY = map(positionY, 0, ActualHeight, 0, 1);
        }

        #region Event Handlers
        private void OnThumbDragDelta(DragDeltaEventArgs e)
        {
            double offsetX = m_thumbTransform.X + e.HorizontalChange;
            double offsetY = m_thumbTransform.Y + e.VerticalChange;
            UpdatePosition(offsetX, offsetY);
        }

        private static void OnThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            XYController hsvControl = sender as XYController;
            hsvControl.OnThumbDragDelta(e);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (m_thumb != null)
            {
                Point position = e.GetPosition(this);
                UpdatePosition(position.X, ActualHeight - position.Y); //Check canvas scale in style 
                m_thumb.RaiseEvent(e);
            }
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            m_thumbTransform.X = ThumbPosX * ActualWidth;
            m_thumbTransform.Y = ThumbPosY * ActualHeight;
            base.OnRenderSizeChanged(sizeInfo);
        }
        #endregion

        private double LimitValue(double value, double max)
        {
            if (value < 0)
                value = 0;
            if (value > max)
                value = max;
            return value;
        }

        private double map(double value, double fromLow, double fromHigh, double toLow, double toHigh)
        {
            return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            m_thumb = GetTemplateChild(ThumbName) as Thumb;
            if (m_thumb != null)
            {
                UpdatePosition(this.ThumbPosX, this.ThumbPosY);
                m_thumb.RenderTransform = m_thumbTransform;
            }
        }
    }
}