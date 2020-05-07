using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CMiX.MVVM.Controls
{
    public class DistanceSlider : Slider
    {
        public DistanceSlider()
        {
            OnApplyTemplate();
        }

        public override void OnApplyTemplate()
        {
            var grid = GetTemplateChild("Pouet") as Grid;
            if (grid != null)
            {
                grid.MouseDown += Grid_MouseDown;
                grid.MouseMove += Grid_MouseMove;
                grid.MouseUp += Grid_MouseUp;
                grid.MouseEnter += Grid_MouseEnter;
                grid.MouseLeave += Grid_MouseLeave;
                grid.LostMouseCapture += Grid_LostMouseCapture;
            }

            base.OnApplyTemplate();
        }

        private void Grid_LostMouseCapture(object sender, MouseEventArgs e)
        {
            _lastPoint = null;
            //Console.WriteLine("Grid_LostMouseCapture");
            var grid = sender as Grid;
            //Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            mouseLeave = true;
            Mouse.OverrideCursor = Cursors.Arrow;
            Console.WriteLine("Grid_MouseLeave");
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.SizeWE;
            Console.WriteLine("Grid_MouseEnter");
        }


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

        private Point? _lastPoint;
        private Point? _mouseDownPos;
        private bool mouseLeave = false;

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Grid_MouseDown");
            var grid = sender as Grid;
            _lastPoint = GetMousePosition();
            grid.CaptureMouse();
            Mouse.OverrideCursor = Cursors.None;
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            //Console.WriteLine("Grid_MouseMove");
            if (_lastPoint != null)
            {

                var grid = sender as Grid;
                var currentPoint = GetMousePosition();
                var offset = currentPoint - _lastPoint.Value;

                //_lastPoint = currentPoint;
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                {
                    Console.WriteLine("ShiftKeyPressed");
                    offset *= 0.05;
                }

                this.Value = offset.X;
                var textBlock = GetTemplateChild("textBlock") as TextBlock;
                
                textBlock.Text = offset.X.ToString();

            }
        }



        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Grid_MouseUp");
            var grid = sender as Grid;

            SetCursorPos(Convert.ToInt32(_lastPoint.Value.X), Convert.ToInt32(_lastPoint.Value.Y));

            mouseLeave = false;
            _lastPoint = null;
            grid.ReleaseMouseCapture();

            if (!grid.IsMouseOver)
                Mouse.OverrideCursor = Cursors.Arrow;
            else
                Mouse.OverrideCursor = Cursors.SizeWE;
            //if(_mouseDownPos == GetMousePosition())
            //    Console.WriteLine("Mouse didn't move");

        }
    }
}
