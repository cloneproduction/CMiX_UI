using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CMiX.MVVM.Controls
{
    public partial class EditableTextBox : UserControl
    {
        public EditableTextBox()
        {
            InitializeComponent();
            TextInput.Visibility = Visibility.Hidden;
            TextDisplay.Visibility = Visibility.Visible;
        }

        #region PROPERTIES
        public static readonly DependencyProperty CanEditProperty =
        DependencyProperty.Register("CanEdit", typeof(bool), typeof(EditableTextBox), new UIPropertyMetadata(false, new PropertyChangedCallback(CanEditProperty_PropertyChanged)));
        public bool CanEdit
        {
            get { return (bool)GetValue(CanEditProperty); }
            set { SetValue(CanEditProperty, value); }
        }

        private static void CanEditProperty_PropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        { 
            EditableTextBox textbox = obj as EditableTextBox;
            if (!textbox.CanEdit)
            {
                textbox.clickcount = 0;
            }
            if (textbox.CanEdit)
            {
                //textbox.clickcount = 0;
            }
                
        }

        [Bindable(true)]
        public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(EditableTextBox), new UIPropertyMetadata(new PropertyChangedCallback(TextProperty_PropertyChanged)));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static void TextProperty_PropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            EditableTextBox textbox = obj as EditableTextBox;
            var newtext = (string)e.NewValue;
            textbox.TextDisplay.Text = newtext;
            textbox.TextInput.Text = newtext;
        }

        public int clickcount = 0;

        public ItemsControl _ParentItemsControl { get; set; }
        #endregion

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

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            clickcount = 1;
            OnSwitchToNormalMode();

            //base.OnMouseDoubleClick(e);
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            clickcount++;
            if (CanEdit && clickcount > 1)
            {
                OnSwitchToEditingMode();
                TextInput.Focus();
                TextInput.SelectAll();
            }
            e.Handled = false ;
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            OnSwitchToNormalMode();
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            OnSwitchToNormalMode();
        }

        private void OnMouseDownOutsideElement(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            OnSwitchToNormalMode();
        }
        #endregion

        #region PRIVATE METHODS
        private void OnSwitchToEditingMode()
        {
            Mouse.Capture(this);
            AddHandler();
            TextDisplay.Visibility = Visibility.Hidden;
            TextInput.Visibility = Visibility.Visible;
            HookItemsControlEvents();

            Text = TextInput.Text;
        }

        private void OnSwitchToNormalMode(bool bCancelEdit = true)
        {
            TextDisplay.Text = TextInput.Text;
            TextDisplay.Visibility = Visibility.Visible;

            TextInput.Visibility = Visibility.Hidden;

            Mouse.RemovePreviewMouseDownOutsideCapturedElementHandler(this, OnMouseDownOutsideElement);
            ReleaseMouseCapture();
        }

        private void HookItemsControlEvents()
        {

            Mouse.AddPreviewMouseDownOutsideCapturedElementHandler(this, OnMouseDownOutsideElement);

            if (_ParentItemsControl != null)
            {
                // Handle events on parent control and determine whether to switch to Normal mode or stay in editing mode
                //_ParentItemsControl.AddHandler(ScrollViewer.ScrollChangedEvent, new RoutedEventHandler(this.OnScrollViewerChanged));
                _ParentItemsControl.AddHandler(ScrollViewer.MouseWheelEvent, new RoutedEventHandler((s, e) => this.OnSwitchToNormalMode()), true);

                _ParentItemsControl.MouseDown += new MouseButtonEventHandler((s, e) => this.OnSwitchToNormalMode());
                _ParentItemsControl.SizeChanged += new SizeChangedEventHandler((s, e) => this.OnSwitchToNormalMode());

                // Restrict text box to visible area of scrollviewer
                //this.ParentScrollViewer = this.GetDpObjectFromVisualTree(_ParentItemsControl, typeof(ScrollViewer)) as ScrollViewer;

                //if (this.ParentScrollViewer == null)
                //    this.ParentScrollViewer = FindVisualChild<ScrollViewer>(_ParentItemsControl);

                //Debug.Assert(this.ParentScrollViewer != null, "DEBUG ISSUE: No ScrollViewer found.");

                //if (this.ParentScrollViewer != null)
                //    _TextBox.MaxWidth = this.ParentScrollViewer.ViewportWidth;
            }
        }

        private void AddHandler()
        {
            AddHandler(Mouse.PreviewMouseDownOutsideCapturedElementEvent, new MouseButtonEventHandler(OnMouseDownOutsideElement), true);
        }

        private DependencyObject GetDpObjectFromVisualTree(DependencyObject startObject, Type type)
        {
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
        #endregion
    }
}