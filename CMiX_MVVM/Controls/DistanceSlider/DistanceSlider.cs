using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CMiX.MVVM.Controls
{
    public class DistanceSlider : UserControl
    {
        public DistanceSlider()
        {
            OnApplyTemplate();
        }

        private EditableValue EditableValue { get; set; }
        private Grid Grid { get; set; }
        private Button SubButton { get; set; }
        private Button AddButton {get; set;}

        public override void OnApplyTemplate()
        {
            Grid = GetTemplateChild("Pouet") as Grid;
            EditableValue = GetTemplateChild("editableValue") as EditableValue;
            SubButton = GetTemplateChild("SubButton") as Button;
            AddButton = GetTemplateChild("AddButton") as Button;

            if (Grid != null)
            {
                Grid.MouseDown += Grid_MouseDown;
                Grid.MouseMove += Grid_MouseMove;
                Grid.MouseUp += Grid_MouseUp;
            }

            if(SubButton != null)
            {
                AddButton.Click += AddButton_Click;
                SubButton.Click += SubButton_Click;
            }

            ScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            ScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

            base.OnApplyTemplate();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.Value += 0.001;
        }

        private void SubButton_Click(object sender, RoutedEventArgs e)
        {
            this.Value -= 0.001;
        }

        [Bindable(true)]
        public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(double), typeof(DistanceSlider), new FrameworkPropertyMetadata
        {
            BindsTwoWayByDefault = true,
            DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        });
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private int ScreenHeight;
        private int ScreenWidth;


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

        private double newValue;

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _lastPoint = GetMousePosition();
            _mouseDownPos = GetMousePosition();
            Grid.CaptureMouse();
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (_lastPoint != null)
            {
                var currentPoint = GetMousePosition();
                var offset = currentPoint - _lastPoint.Value;

                if (currentPoint.X >= ScreenWidth - 1)
                    SetCursorPos(0, Convert.ToInt32(currentPoint.Y));
                else if (currentPoint.X <= 0)
                    SetCursorPos(ScreenWidth - 1, Convert.ToInt32(currentPoint.Y));

                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                    newValue = this.Value + offset.X * 0.001;
                else
                    newValue = this.Value + offset.X * 0.01;


                this.Value = newValue;
                _lastPoint = GetMousePosition();
            }
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var mouseUpPos = GetMousePosition();

            if (_mouseDownPos != null)
                SetCursorPos(Convert.ToInt32(_mouseDownPos.Value.X), Convert.ToInt32(_mouseDownPos.Value.Y));

            if (_mouseDownPos == mouseUpPos)
                EditableValue.IsEditing = true;

            Grid.ReleaseMouseCapture();
            _lastPoint = null;
        }
    }
}
