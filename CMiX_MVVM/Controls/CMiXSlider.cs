using CMiX.MVVM.Resources;
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
    public class CMiXSlider : Slider
    {
        public CMiXSlider()
        {
            OnApplyTemplate();
        }

        EditableValue EditableValue { get; set; }

        public override void OnApplyTemplate()
        {

            EditableValue = GetTemplateChild("editableValue") as EditableValue;

            base.OnApplyTemplate();
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            if (_mouseDownPos != null)
            {
                var currentPoint = GetMousePosition();
                var offset = currentPoint - _lastPoint.Value;

                newValue = this.Value + offset.X * ((Math.Abs(this.Minimum) + Math.Abs(this.Maximum)) / ActualWidth);

                if (newValue >= this.Maximum)
                    newValue = this.Maximum;
                else if (newValue <= this.Minimum)
                    newValue = this.Minimum;

                this.Value = newValue;
                _lastPoint = GetMousePosition();
                Console.WriteLine("OnPreviewMouseMove");
            }
            base.OnPreviewMouseMove(e);
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            Console.WriteLine("OnPreviewMouseUp");
            var mouseUpPos = e.GetPosition(this);

            if (_mouseDownPos != null)
            {
                Point pointToScreen;
                double YPos = ActualHeight / 2;
                double XPos = 0;

                if (mouseUpPos.X > this.ActualWidth)
                    XPos = ActualWidth;
                else if (mouseUpPos.X < 0)
                    XPos = 0;
                else
                    XPos = Utils.Map(this.Value, this.Minimum, this.Maximum, 0, ActualWidth);

                pointToScreen = this.PointToScreen(new Point(XPos, YPos));

                SetCursorPos(Convert.ToInt32(pointToScreen.X), Convert.ToInt32(pointToScreen.Y));
                
            }

            if (_mouseDownPos == mouseUpPos)
            {
                EditableValue.IsEditing = true;
            }

            _mouseDownPos = null;
            this.ReleaseMouseCapture();
            base.OnPreviewMouseUp(e);
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            _lastPoint = GetMousePosition();
            _mouseDownPos = e.GetPosition(this);
            this.CaptureMouse();
            base.OnPreviewMouseDown(e);
            Console.WriteLine("OnPreviewMouseDown");
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

        private Point? _mouseDownPos;
        private Point? _lastPoint;
        private double newValue;

        public static readonly DependencyProperty CaptionProperty =
        DependencyProperty.Register("Caption", typeof(string), typeof(Slider), new PropertyMetadata(""));
        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        //public static readonly DependencyProperty IsEditingProperty =
        //DependencyProperty.Register("IsEditing", typeof(bool), typeof(EditableValue), new UIPropertyMetadata(false, new PropertyChangedCallback(IsEditing_PropertyChanged)));
        //public bool IsEditing
        //{
        //    get { return (bool)GetValue(IsEditingProperty); }
        //    set{ SetValue(IsEditingProperty, value); }
        //}

        //private static void IsEditing_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
