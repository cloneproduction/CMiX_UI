using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CMiX.MVVM.Trash
{
    public class EditableTextBox : TextBox
    {
        static EditableTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EditableTextBox), new FrameworkPropertyMetadata(typeof(EditableTextBox)));
        }

        #region Dependency Properties
        public static readonly DependencyProperty IsEditingProperty =
        DependencyProperty.Register("IsEditing", typeof(bool), typeof(EditableTextBox), new UIPropertyMetadata(false));
        public bool IsEditing
        {
            get { return (bool)GetValue(IsEditingProperty); }
            set { SetValue(IsEditingProperty, value); }
        }
        #endregion

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            Mouse.Capture(this);
            OnSwitchToEditingMode();
            AddHandler();

            e.Handled = true;
            base.OnPreviewMouseLeftButtonDown(e);
        }

        private void OnSwitchToEditingMode()
        {
            if(!IsReadOnly && !IsEditing)
            {
                HookItemsControlEvents();
                IsEditing = true;
                Console.WriteLine("Is On Editing Mode");
            }
        }

        private void OnSwitchToNormalMode()
        {
            //if(IsEditing)
            //{
                IsEditing = false;
                Console.WriteLine("Is On Normal Mode");
                Mouse.RemovePreviewMouseDownOutsideCapturedElementHandler(this, OnMouseDownOutsideElement);
            //}

            ReleaseMouseCapture();
        }

        private void AddHandler()
        {
            AddHandler(Mouse.PreviewMouseDownOutsideCapturedElementEvent, new MouseButtonEventHandler(OnMouseDownOutsideElement), true);
        }

        private void OnMouseDownOutsideElement(object sender, MouseButtonEventArgs e)
        {

            e.Handled = true;
            OnSwitchToNormalMode();
            Console.WriteLine(" -- OnMouseDownOutsideElement");
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
        }



        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            OnSwitchToNormalMode();
            Console.WriteLine(" -- OnLostFocus");
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnLostKeyboardFocus(e);
            OnSwitchToNormalMode();
            Console.WriteLine(" -- OnLostKeyboardFocus");
        }



        private DependencyObject GetDpObjectFromVisualTree(DependencyObject startObject, Type type)
        {
            // Walk the visual tree to get the parent(ItemsControl)
            // of this control
            DependencyObject parent = startObject;
            while (parent != null)
            {
                if (type.IsInstanceOfType(parent))
                    break;
                else
                    parent = VisualTreeHelper.GetParent(parent);
            }
            return parent;
        }

        public Window _ParentItemsControl { get; set; }

        private void HookItemsControlEvents()
        {

            CaptureMouse();
            Mouse.AddPreviewMouseDownOutsideCapturedElementHandler(this, OnMouseDownOutsideElement);

            _ParentItemsControl = this.GetDpObjectFromVisualTree(this, typeof(Window)) as Window;
            Debug.Assert(_ParentItemsControl != null, "DEBUG ISSUE: No FolderTreeView found.");

            if (_ParentItemsControl != null)
            {
                _ParentItemsControl.MouseDown += new MouseButtonEventHandler((s, e) => this.OnSwitchToNormalMode());
                _ParentItemsControl.SizeChanged += new SizeChangedEventHandler((s, e) => this.OnSwitchToNormalMode());
            }
        }
    }
}