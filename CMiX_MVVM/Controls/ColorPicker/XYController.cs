using CMiX.MVVM.Tools;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace CMiX.MVVM.Controls
{
    public class XYController : Control
    {
        static XYController()
        {
            UIElement.VisibilityProperty.OverrideMetadata(typeof(XYController),new PropertyMetadata(Visibility.Visible, OnVisibililtyChanged));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(XYController), new FrameworkPropertyMetadata(typeof(XYController)));
            EventManager.RegisterClassHandler(typeof(XYController), Thumb.DragDeltaEvent, new DragDeltaEventHandler(XYController.OnThumbDragDelta));
        }

        private static void OnVisibililtyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OnThumbPosChanged(d, e);
        }

        private const string ThumbName = "PART_Thumb";
        private TranslateTransform m_thumbTransform = new TranslateTransform();
        private Thumb m_thumb;

        #region Dependency Properties
        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }
        public static readonly DependencyProperty SelectedColorProperty =
        DependencyProperty.Register("SelectedColor", typeof(Color), typeof(XYController),
            new FrameworkPropertyMetadata(Colors.Red, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public double ValueX
        {
            get { return (double)GetValue(ValueXProperty); }
            set { SetValue(ValueXProperty, value); }
        }
        public static readonly DependencyProperty ValueXProperty =
        DependencyProperty.Register("ValueX", typeof(double), typeof(XYController),
            new FrameworkPropertyMetadata(0.5,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnThumbPosChanged));


        public double ValueY
        {
            get { return (double)GetValue(ValueYProperty); }
            set { SetValue(ValueYProperty, value); }
        }
        public static readonly DependencyProperty ValueYProperty =
        DependencyProperty.Register("ValueY", typeof(double), typeof(XYController),
            new FrameworkPropertyMetadata(0.5,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnThumbPosChanged));



        private static void OnThumbPosChanged(DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
        {
            XYController xycontroller = relatedObject as XYController;
            if (xycontroller.Visibility == Visibility.Collapsed)
                return;

            if (xycontroller != null)
            {
                xycontroller.m_thumbTransform.X = Utils.Map(xycontroller.ValueX, xycontroller.XMin, xycontroller.XMax, 0, xycontroller.ActualWidth);
                xycontroller.m_thumbTransform.Y = Utils.Map(xycontroller.ValueY, xycontroller.YMin, xycontroller.YMax, 0, xycontroller.ActualHeight) ;
            }
        }

        public double BackgroundOpacity
        {
            get { return (double)GetValue(BackgroundOpacityProperty); }
            set { SetValue(BackgroundOpacityProperty, value); }
        }
        public static readonly DependencyProperty BackgroundOpacityProperty =
        DependencyProperty.Register("BackgroundOpacity", typeof(double), typeof(XYController),
            new FrameworkPropertyMetadata(1.0,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public double XMin
        {
            get { return (double)GetValue(XMinProperty); }
            set { SetValue(XMinProperty, value); }
        }
        public static readonly DependencyProperty XMinProperty =
        DependencyProperty.Register("XMin", typeof(double), typeof(XYController),
            new FrameworkPropertyMetadata(0.0,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double XMax
        {
            get { return (double)GetValue(XMaxProperty); }
            set { SetValue(XMaxProperty, value); }
        }
        public static readonly DependencyProperty XMaxProperty =
        DependencyProperty.Register("XMax", typeof(double), typeof(XYController),
            new FrameworkPropertyMetadata(1.0,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public double YMin
        {
            get { return (double)GetValue(YMinProperty); }
            set { SetValue(YMinProperty, value); }
        }
        public static readonly DependencyProperty YMinProperty =
        DependencyProperty.Register("YMin", typeof(double), typeof(XYController),
            new FrameworkPropertyMetadata(0.0,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double YMax
        {
            get { return (double)GetValue(YMaxProperty); }
            set { SetValue(YMaxProperty, value); }
        }
        public static readonly DependencyProperty YMaxProperty =
        DependencyProperty.Register("YMax", typeof(double), typeof(XYController),
            new FrameworkPropertyMetadata(1.0,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion



        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }


        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            //var mouseUpPos = e.GetPosition(this);

            //double YPos = Utils.Map(this.ValueY, YMin, YMax, ActualHeight, 0.0);
            //double XPos = Utils.Map(this.ValueX, XMin, XMax, 0, ActualWidth);

            //if (XPos >= ActualWidth)
            //    XPos -= 1;

            //if (XPos <= 0)
            //    XPos += 1;

            //if (YPos >= ActualHeight)
            //    YPos -= 1;

            //if (YPos <= ActualHeight)
            //    YPos += 1;

            //Point pointToScreen = this.PointToScreen(new Point(XPos, YPos));
            //SetCursorPos(Convert.ToInt32(pointToScreen.X), Convert.ToInt32(pointToScreen.Y));

            double XPos = Utils.Map(this.ValueX, XMin, XMax, 0, ActualWidth);
            double YPos = Utils.Map(this.ValueY, YMin, YMax, ActualHeight, 0.0);


            Point pointToScreen = this.PointToScreen(new Point(XPos, YPos));
            SetCursorPos(Convert.ToInt32(pointToScreen.X), Convert.ToInt32(pointToScreen.Y));
        }


        private void UpdatePosition(double positionX, double positionY)
        {
            if (this.Visibility == Visibility.Collapsed)
                return;

            if (positionX >= ActualWidth)
                positionX = ActualWidth;

            if (positionX <= 0)
                positionX = 0;

            if (positionY >= ActualHeight)
                positionY = ActualHeight;

            if (positionY <= 0)
                positionY = 0;

            m_thumbTransform.X = positionX;
            m_thumbTransform.Y = positionY;


            positionX = Utils.Map(positionX, 0, ActualWidth, XMin, XMax);
            positionY = Utils.Map(positionY, 0, ActualHeight, YMin, YMax);

            if (positionX >= XMax)
                positionX = XMax;

            if (positionX <= XMin)
                positionX = XMin;

            if (positionY >= YMax)
                positionY = YMax;

            if (positionY <= YMin)
                positionY = YMin;



            ValueX = positionX;
            ValueY = positionY;


        }

        private void OnThumbDragDelta(DragDeltaEventArgs e)
        {
            double offsetX = m_thumbTransform.X + e.HorizontalChange;
            double offsetY = m_thumbTransform.Y + e.VerticalChange;

            Console.WriteLine("offsetX " + offsetX);
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
            m_thumbTransform.X = ValueX * ActualWidth;
            m_thumbTransform.Y = ValueY * ActualHeight;
            base.OnRenderSizeChanged(sizeInfo);
        }


        private double LimitValue(double value, double max)
        {
            if (value <= 0)
                value = 0;
            if (value >= ActualWidth)
                value = ActualWidth;

            return value;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            m_thumb = GetTemplateChild(ThumbName) as Thumb;
            if (m_thumb != null)
            {
                UpdatePosition(this.ValueX, this.ValueY);
                m_thumb.RenderTransform = m_thumbTransform;
            }
        }
    }
}