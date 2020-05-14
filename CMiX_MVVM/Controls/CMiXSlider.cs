using CMiX.MVVM.Resources;
using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
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

        TextBox TextInput { get; set; }
        Border Border { get; set; }


        public override void OnApplyTemplate()
        {
            TextInput = GetTemplateChild("textInput") as TextBox;
            Border = GetTemplateChild("sliderBorder") as Border;

            if(TextInput != null)
            {
                TextInput.MouseLeave += View_OnMouseLeave;
                TextInput.MouseEnter += View_OnMouseEnter;
            }

            base.OnApplyTemplate();
        }

        private void View_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

            if (parentWindow != null)
            {
                Mouse.AddPreviewMouseDownHandler(parentWindow, ParentWindow_OnMouseDown);
            }
        }

        private void View_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

            if (parentWindow != null)
            {
                Mouse.RemovePreviewMouseDownHandler(parentWindow, ParentWindow_OnMouseDown);
            }
        }

        private void ParentWindow_OnMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            Window parentWindow = Window.GetWindow(this);

            if (parentWindow != null)
            {
                Mouse.RemovePreviewMouseDownHandler(parentWindow, ParentWindow_OnMouseDown);
            }
            OnSwitchToNormalMode();
        }


        private void TextInput_GotFocus(object sender, RoutedEventArgs e)
        {
            TextInput.MouseLeave += View_OnMouseLeave;
            TextInput.MouseEnter += View_OnMouseEnter;
        }

        private void Text_OnLostFocus(object sender, RoutedEventArgs e)
        {
            TextInput.MouseLeave -= View_OnMouseLeave;
            TextInput.MouseEnter -= View_OnMouseEnter;
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            if (IsEditing == false)
            {
                _lastPoint = GetMousePosition();
                _mouseDownPos = e.GetPosition(this);
                Border.CaptureMouse();
            }
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
            }
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            var mouseUpPos = e.GetPosition(this);
            Border.ReleaseMouseCapture();

            if (_mouseDownPos == mouseUpPos)
                OnSwitchToEditingMode();

            if (_mouseDownPos != null && IsEditing == false)
            {
                Point pointToScreen;
                double YPos = ActualHeight / 2;
                double XPos = 0;

                if (mouseUpPos.X > this.ActualWidth)
                    XPos = ActualWidth - 1;
                else if (mouseUpPos.X < 0)
                    XPos = 0;
                else
                    XPos = Utils.Map(this.Value, this.Minimum, this.Maximum, 0, ActualWidth);

                pointToScreen = this.PointToScreen(new Point(XPos, YPos));
                SetCursorPos(Convert.ToInt32(pointToScreen.X), Convert.ToInt32(pointToScreen.Y));
            }
            _mouseDownPos = null;
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

        #region EVENTS
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

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            e.Handled = true;
            OnSwitchToNormalMode();
        }

        public void OnMouseDownOutsideElement(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            OnSwitchToNormalMode();
        }
        #endregion

        private void OnSwitchToEditingMode()
        {
            IsEditing = true;
            TextInput.Focus();
            TextInput.SelectAll();
        }

        private void OnSwitchToNormalMode(bool bCancelEdit = true)
        {
            IsEditing = false;
            Keyboard.ClearFocus();
            _mouseDownPos = null;
        }

        private readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void TextInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        public static readonly DependencyProperty IsEditingProperty =
        DependencyProperty.Register("IsEditing", typeof(bool), typeof(CMiXSlider), new UIPropertyMetadata(false));
        public bool IsEditing
        {
            get { return (bool)GetValue(IsEditingProperty); }
            set { SetValue(IsEditingProperty, value); }
        }
    }
}