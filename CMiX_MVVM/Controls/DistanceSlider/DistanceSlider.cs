using CMiX.MVVM.Resources;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CMiX.MVVM.Controls
{
    public class DistanceSlider : Control
    {
        public DistanceSlider()
        {
            OnApplyTemplate();
        }
        
        //private EditableValue EditableValue { get; set; }
        private Border Border { get; set; }
        private Button SubButton { get; set; }
        private Button AddButton {get; set;}
        private TextBox ValueInput { get; set; }

        public override void OnApplyTemplate()
        {
            Border = GetTemplateChild("borderValueDisplay") as Border;
            //EditableValue = GetTemplateChild("editableValue") as EditableValue;
            ValueInput = GetTemplateChild("valueInput") as TextBox;

            SubButton = GetTemplateChild("SubButton") as Button;
            AddButton = GetTemplateChild("AddButton") as Button;

            if (ValueInput != null)
            {
                ValueInput.MouseLeave += View_OnMouseLeave;
                ValueInput.MouseEnter += View_OnMouseEnter;
            }

            if (Border != null)
            {
                Border.PreviewMouseLeftButtonDown += Border_MouseDown;
                Border.MouseMove += Border_MouseMove;
                Border.PreviewMouseLeftButtonUp += Border_MouseUp;
                Border.PreviewMouseRightButtonDown += Border_MouseRightButtonDown;
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

        private void Border_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            OnSwitchToNormalMode();
        }

        private void View_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
                Mouse.AddPreviewMouseDownHandler(parentWindow, ParentWindow_OnMouseDown);
        }

        private void View_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
                Mouse.RemovePreviewMouseDownHandler(parentWindow, ParentWindow_OnMouseDown);
        }

        private void ParentWindow_OnMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
                Mouse.RemovePreviewMouseDownHandler(parentWindow, ParentWindow_OnMouseDown);
            OnSwitchToNormalMode();
        }

        private void TextInput_GotFocus(object sender, RoutedEventArgs e)
        {
            ValueInput.MouseLeave += View_OnMouseLeave;
            ValueInput.MouseEnter += View_OnMouseEnter;
        }

        private void Text_OnLostFocus(object sender, RoutedEventArgs e)
        {
            ValueInput.MouseLeave -= View_OnMouseLeave;
            ValueInput.MouseEnter -= View_OnMouseEnter;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.Value += 0.001;
            e.Handled = true;
        }

        private void SubButton_Click(object sender, RoutedEventArgs e)
        {
            this.Value -= 0.001;
            e.Handled = true;
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


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsEditing == false)
            {
                _lastPoint = GetMousePosition();
                _mouseDownPos = e.GetPosition(this);
                Border.CaptureMouse();
            }
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDownPos != null)
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

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var mouseUpPos = e.GetPosition(this);
            Border.ReleaseMouseCapture();

            if (_mouseDownPos == mouseUpPos)
                OnSwitchToEditingMode();

            if (_mouseDownPos != null && IsEditing == false)
            {
                Point pointToScreen = this.PointToScreen(new Point(ActualWidth / 2, ActualHeight / 2));
                SetCursorPos(Convert.ToInt32(pointToScreen.X), Convert.ToInt32(pointToScreen.Y));
            }
            _mouseDownPos = null;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Enter)
            {
                OnSwitchToNormalMode();
                e.Handled = true;
                return;
            }
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            e.Handled = true;
            OnSwitchToNormalMode();
        }

        private void OnSwitchToEditingMode()
        {
            IsEditing = true;
            ValueInput.Focus();
            ValueInput.SelectAll();
        }

        private void OnSwitchToNormalMode(bool bCancelEdit = true)
        {
            IsEditing = false;
            Keyboard.ClearFocus();
            _mouseDownPos = null;
        }

        public static readonly DependencyProperty IsEditingProperty =
        DependencyProperty.Register("IsEditing", typeof(bool), typeof(DistanceSlider), new UIPropertyMetadata(false));
        public bool IsEditing
        {
            get { return (bool)GetValue(IsEditingProperty); }
            set { SetValue(IsEditingProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(double), typeof(DistanceSlider), new UIPropertyMetadata(0.0));
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty PositionProperty =
        DependencyProperty.Register("Position", typeof(ControlPosition), typeof(DistanceSlider), new UIPropertyMetadata(ControlPosition.Default));
        public ControlPosition Position
        {
            get { return (ControlPosition)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }
    }
}
