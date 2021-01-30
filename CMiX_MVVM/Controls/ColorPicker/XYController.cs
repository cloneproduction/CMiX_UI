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


        //private Point? _mouseDownPos;
        //private Point? _lastPoint;
        private double newValueX;
        private double newValueY;


        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            var mouseUpPos = e.GetPosition(this);

            double xPos = mouseUpPos.X;
            double yPos = mouseUpPos.Y;

            if (xPos >= ActualWidth)
                xPos = ActualWidth - 2;

            if (yPos >= ActualHeight)
                yPos = ActualHeight - 2;

            if (xPos <= 0)
                xPos = 0 + 1;

            if (yPos <= 0)
                yPos = 0 + 1;

            Point pointToScreen = this.PointToScreen(new Point(xPos, yPos));
            SetCursorPos(Convert.ToInt32(pointToScreen.X), Convert.ToInt32(pointToScreen.Y));
        }

        private void OnThumbDragDelta(DragDeltaEventArgs e)
        {
            var horizontalChange = e.HorizontalChange;
            var verticalChange = e.VerticalChange;

            m_thumbTransform.X += horizontalChange;
            m_thumbTransform.Y += verticalChange;

            if (m_thumbTransform.X >= ActualWidth)
                m_thumbTransform.X = ActualWidth;

            if (m_thumbTransform.X <= 0)
                m_thumbTransform.X = 0;

            if (m_thumbTransform.Y >= ActualHeight)
                m_thumbTransform.Y = ActualHeight;

            if (m_thumbTransform.Y <= 0)
                m_thumbTransform.Y = 0;

            var ValueXChange = Utils.Map(Math.Abs(horizontalChange), 0, ActualWidth, XMin, XMax);
            var ValueYChange = Utils.Map(Math.Abs(verticalChange), 0, ActualHeight, YMin, YMax);

            if (e.HorizontalChange > 0)
                this.ValueX += ValueXChange;
            else
                this.ValueX -= ValueXChange;

            if(e.VerticalChange > 0)
                this.ValueY += ValueYChange;
            else
                this.ValueY -= ValueYChange;

            if (ValueX >= XMax)
                ValueX = XMax;

            if (ValueX <= XMin)
                ValueX = XMin;

            if (ValueY >= YMax)
                ValueY = YMax;

            if (ValueY <= YMin)
                ValueY = YMin;

            Console.WriteLine("ValueX = "+ ValueX);
            Console.WriteLine("ValueY = " + ValueY);
        }

        private static void OnThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            XYController xyControl = sender as XYController;
            if(xyControl != null)
                xyControl.OnThumbDragDelta(e);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {

            var _mouseDownPos = e.GetPosition(this);
            m_thumbTransform.X = ((Point)_mouseDownPos).X;
            m_thumbTransform.Y = Utils.Map(((Point)_mouseDownPos).Y, 0, ActualHeight, ActualHeight, 0);

            if (m_thumb != null)
            {
                var currentPoint = GetMousePosition();
                var offset = currentPoint;// - _lastPoint.Value;

                newValueX = this.ValueX + offset.X * ((Math.Abs(this.XMin) + Math.Abs(this.XMax)) / ActualWidth);
                newValueY = this.ValueY + offset.Y * ((Math.Abs(this.YMin) + Math.Abs(this.YMax)) / -ActualHeight);
                m_thumb.RaiseEvent(e);
            }

            //_lastPoint = GetMousePosition();
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            m_thumbTransform.X = ValueX * ActualWidth;
            m_thumbTransform.Y = ValueY * ActualHeight;
            base.OnRenderSizeChanged(sizeInfo);
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            m_thumb = GetTemplateChild(ThumbName) as Thumb;
            if (m_thumb != null)
            {
                //UpdatePosition(this.ValueX, this.ValueY);
                m_thumb.RenderTransform = m_thumbTransform;
            }
        }
    }
}