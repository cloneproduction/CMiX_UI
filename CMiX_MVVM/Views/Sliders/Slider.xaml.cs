using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace CMiX.MVVM.Views
{
    public partial class Slider : UserControl
    {
        public Slider()
        {
            InitializeComponent();
            CMiXSlider.ApplyTemplate();

            Thumb = (CMiXSlider.Template.FindName("PART_Track", CMiXSlider) as Track).Thumb;
            Thumb.MouseEnter += new MouseEventHandler(thumb_MouseEnter);
            CMiXSlider.PreviewMouseUp += CMiXSlider_PreviewMouseUp;
        }



        private Thumb Thumb { get; set; }

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

        private Point? _mouseDownPos;

        private void CMiXSlider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            var mouseUpPos = e.GetPosition(this);
            

            Console.WriteLine("CMiXSlider_PreviewMouseUp");

            if(_mouseDownPos != null)
            {
                Point pointToScreen;
                if (mouseUpPos.X > this.ActualWidth)
                {
                    pointToScreen = this.PointToScreen(new Point() { X = ActualWidth, Y = ActualHeight / 2 });
                }
                else if(mouseUpPos.X < 0)
                {
                    pointToScreen = this.PointToScreen(new Point() { X = 0, Y = ActualHeight / 2 });
                }
                else
                {
                    pointToScreen = this.PointToScreen(new Point() { X = mouseUpPos.X, Y = ActualHeight / 2 });
                }
                SetCursorPos(Convert.ToInt32(pointToScreen.X), Convert.ToInt32(pointToScreen.Y));
            }
        }


        private void thumb_MouseEnter(object sender, MouseEventArgs e)
        {
            
            if (e.LeftButton == MouseButtonState.Pressed && e.MouseDevice.Captured == null)
            {
                _mouseDownPos = e.GetPosition(this);
                MouseButtonEventArgs args = new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.Left);
                args.RoutedEvent = MouseLeftButtonDownEvent;
                (sender as Thumb).RaiseEvent(args);
            }
        }

        public static readonly DependencyProperty CaptionProperty =
        DependencyProperty.Register("Caption", typeof(string), typeof(Slider), new PropertyMetadata(""));
        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }
    }
}