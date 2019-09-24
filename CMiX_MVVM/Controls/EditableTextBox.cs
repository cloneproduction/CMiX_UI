using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CMiX.MVVM.Controls
{
    public class EditableTextBox : TextBox
    {
        static EditableTextBox()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(EditableTextBox), new FrameworkPropertyMetadata(typeof(EditableTextBox)));
        }

        #region Dependency Properties

        //public Color LeftColor
        //{
        //    get { return (Color)GetValue(LeftColorProperty); }
        //    set { SetValue(LeftColorProperty, value); }
        //}
        //public static readonly DependencyProperty LeftColorProperty =
        //DependencyProperty.Register("LeftColor", typeof(Color), typeof(ColorSlider), new UIPropertyMetadata(Colors.Black));

        //public Color RightColor
        //{
        //    get { return (Color)GetValue(RightColorProperty); }
        //    set { SetValue(RightColorProperty, value); }
        //}
        //public static readonly DependencyProperty RightColorProperty =
        //DependencyProperty.Register("RightColor", typeof(Color), typeof(ColorSlider), new UIPropertyMetadata(Colors.White));

        #endregion

        private bool _isPressed = false;

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            //_isPressed = false;
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            Mouse.Capture(this);
            AddHandler();

            _isPressed = true;

            e.Handled = true;
            base.OnPreviewMouseLeftButtonDown(e);
        }

        //protected override void OnMouseMove(MouseEventArgs e)
        //{

        //}

        private void AddHandler()
        {
            AddHandler(Mouse.PreviewMouseDownOutsideCapturedElementEvent, new MouseButtonEventHandler(HandleClickOutsideOfControl), true);
        }

        private void HandleClickOutsideOfControl(object sender, MouseButtonEventArgs e)
        {
            //_isPressed = false;
            ReleaseMouseCapture();
            Console.WriteLine("Click OutSide TextBox");
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
            //_isPressed = false;
        }
    }
}
