using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CMiX
{
    public partial class FileSelector : UserControl
    {
        public FileSelector()
        {
            InitializeComponent();
        }

        #region Properties
        public static readonly DependencyProperty MouseDownProperty =
        DependencyProperty.Register("MouseDown", typeof(bool), typeof(FileSelector));
        [Bindable(true)]
        public bool MouseDown
        {
            get { return (bool)GetValue(MouseDownProperty); }
            set { SetValue(MouseDownProperty, value); }
        }


        public static readonly DependencyProperty ModeSelectionProperty =
        DependencyProperty.Register("ModeSelection", typeof(string), typeof(FileSelector));
        [Bindable(true)]
        public string ModeSelection
        {
            get { return (string)GetValue(ModeSelectionProperty); }
            set { SetValue(ModeSelectionProperty, value); }
        }

        public static readonly DependencyProperty FileMaskProperty =
        DependencyProperty.Register("FileMask", typeof(List<string>), typeof(FileSelector));
        [Bindable(true)]
        public List<string> FileMask
        {
            get { return (List<string>)GetValue(FileMaskProperty); }
            set { SetValue(FileMaskProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(FileSelector));
        [Bindable(true)]
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsProperty =
        DependencyProperty.Register("SelectedItems", typeof(ObservableCollection<ListBoxFileName>), typeof(FileSelector), new PropertyMetadata());
        [Bindable(true)]
        public ObservableCollection<ListBoxFileName> SelectedItems
        {
            get { return (ObservableCollection<ListBoxFileName>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }
        #endregion
        

        private void DeleteFileName_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = FileNameList.Items.IndexOf(btn.DataContext);
            SelectedItems.RemoveAt(index);
        }

        private ListBoxItem _dragged;
        private Point _dragStartPoint;
        private bool _isMouseDown;

        private bool IsDragGesture(Point point)
        {
            var horizontalMove = Math.Abs(point.X - _dragStartPoint.X);
            var verticalMove = Math.Abs(point.Y - _dragStartPoint.Y);
            var hGesture = horizontalMove > SystemParameters.MinimumHorizontalDragDistance;
            var vGesture = verticalMove > SystemParameters.MinimumVerticalDragDistance;
            return (hGesture | vGesture);
        }

        bool _ClickOnItem = false;

        private void FileNameList_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_dragged != null)
            {
                return;
            }

            UIElement element = FileNameList.InputHitTest(e.GetPosition(FileNameList)) as UIElement;
            while (element != null)
            {
                if (element is ListBoxItem)
                {
                    _dragged = (ListBoxItem)element;
                    break;
                }
                element = VisualTreeHelper.GetParent(element) as UIElement;
            }

            if (e.ClickCount != 1)
            {
                return;
            }
            _dragStartPoint = e.GetPosition(e.Source as FrameworkElement);

            _isMouseDown = true;


        }

        private void FileListBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown == true && IsDragGesture(e.GetPosition(e.Source as FrameworkElement)))
            {

                if (_dragged == null)
                    return;
                if (e.LeftButton == MouseButtonState.Released)
                {
                    _dragged = null;
                    return;
                }

                List<ListBoxFileName> _draggedList = new List<ListBoxFileName>();
                if (_dragged.IsSelected == true)
                {
                    
                    for (var i = 0; i < FileNameList.SelectedItems.Count; i++)
                    {
                        _draggedList.Add(FileNameList.SelectedItems[i] as ListBoxFileName);
                    }

                    var dragData = new DataObject(typeof(List<ListBoxFileName>), _draggedList);
                    var dragSource = this;
                    dragData.SetData("DragSource", dragSource);
                    DragDrop.DoDragDrop(_dragged, dragData, DragDropEffects.All);
                }
                else
                {
                    ListBoxFileName lb = _dragged.Content as ListBoxFileName;
                    _draggedList.Add(lb);
                    var dragData = new DataObject(typeof(List<ListBoxFileName>), _draggedList);
                    var dragSource = this;
                    dragData.SetData("DragSource", dragSource);
                    DragDrop.DoDragDrop(_dragged, dragData, DragDropEffects.All);
                }

            }
        }



        private void FileListBox_Drop(object sender, DragEventArgs e)
        {
           if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                string[] droppedFilePaths = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                foreach(string filemask in FileMask)
                {
                    for (var i = 0; i < droppedFilePaths.Length; i++)
                    {
                        if (Path.GetExtension(droppedFilePaths[i]) == filemask)
                        {
                            ListBoxFileName filename = new ListBoxFileName();
                            filename.FileName = droppedFilePaths[i];
                            filename.FileIsSelected = false;
                            SelectedItems.Add(filename);
                        }
                    }
                }
            }

            DependencyObject child = e.OriginalSource as DependencyObject;
            
            var dataObj = e.Data as DataObject;
            var dragSource = e.Data.GetData("DragSource") as FileSelector;

            if (dragSource != null)
            {
                string dragFileSelectorSourceName = dragSource.Name.ToString();
                string dragFileSelectorTargetName = Utils.FindParent<FileSelector>(child).Name.ToString();

                string dragChannelControlsSourceName =  Utils.FindParent<ChannelControls>(dragSource).Name.ToString();
                string dragChannelControlsTargetName = Utils.FindParent<ChannelControls>(child).Name.ToString();

                List<ListBoxFileName> FileNames = dataObj.GetData(typeof(List<ListBoxFileName>)) as List<ListBoxFileName>;

                if (dragChannelControlsSourceName != dragChannelControlsTargetName || dragFileSelectorSourceName != dragFileSelectorTargetName)
                {
                    foreach (var Files in FileNames)
                    {
                            ListBoxFileName CopiedFileName = Files.Clone() as ListBoxFileName;
                            CopiedFileName.FileIsSelected = false;
                        SelectedItems.Add(CopiedFileName);
                    }
                }
            }
        }

        public event EventHandler FileSelectorChanged;
        protected virtual void OnFileSelectorChanged(RoutedEventArgs e)
        {
            var handler = FileSelectorChanged;
            if (handler != null)
                handler(this, e);
        }
        public static readonly RoutedEvent TapEvent = EventManager.RegisterRoutedEvent("Tap", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FileSelector));

        // Provide CLR accessors for the event
        public event RoutedEventHandler Tap
        {
            add { AddHandler(TapEvent, value); }
            remove { RemoveHandler(TapEvent, value); }
        }

        // This method raises the Tap event
        void RaiseTapEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(FileSelector.TapEvent);
            RaiseEvent(newEventArgs);
        }


        private void FileNameList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnFileSelectorChanged(e);
            RaiseTapEvent();
        }

        /*private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.MouseDown = true;
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.MouseDown = false;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            this.MouseDown = false;
        }*/

        private void FileNameList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                
                /*for(int i = SelectedItems.Count - 1; i >= 0; i--)
                {
                    if(SelectedItems[i].FileIsSelected == true)
                    {
                        SelectedItems.Remove(SelectedItems[i]);
                    }
                }*/
            }
        }

        private void ClearSelected_Click(object sender, RoutedEventArgs e)
        {
            for (int i = SelectedItems.Count - 1; i >= 0; i--)
            {
                if (SelectedItems[i].FileIsSelected == true)
                {
                    SelectedItems.RemoveAt(i);
                }
            }
        }

        private void ClearUnselected_Click(object sender, RoutedEventArgs e)
        {
            for(int i = SelectedItems.Count - 1; i >= 0; i--)
            {
                if (SelectedItems[i].FileIsSelected == false)
                {
                    SelectedItems.RemoveAt(i);
                }
            }
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            SelectedItems.Clear();
        }
    }

    /*public class ListBoxCustom : ListBox
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ListBoxItemCustom();
        }
    }

    public class ListBoxItemCustom : ListBoxItem
    {
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (IsSelected)
                e.Handled = true;
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (IsSelected)
                base.OnMouseLeftButtonDown(e);

            if (!IsSelected)
            {
                IsSelected = true;
            }
        }
    }

    public class ListBoxFileName : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        private string FileNameValue;
        public string FileName
        {
            get { return this.FileNameValue; }

            set
            {
                if (value != this.FileNameValue)
                {
                    this.FileNameValue = value;
                    NotifyPropertyChanged("FileName");
                }
            }
        }

        private bool FileIsSelectedValue;
        public bool FileIsSelected
        {
            get { return this.FileIsSelectedValue; }

            set
            {
                if (value != this.FileIsSelectedValue)
                {
                    this.FileIsSelectedValue = value;
                    NotifyPropertyChanged("FileIsSelected");
                }
            }
        }
    }*/
}

//Thread t;
/*public static readonly DependencyProperty DefaultFolderProperty =
DependencyProperty.Register("DefaultFolder", typeof(string), typeof(FileSelector), new PropertyMetadata("C:"));
public string DefaultFolder
{
    get
    {
        if (!this.Dispatcher.CheckAccess())
        {
            return (string)this.Dispatcher.Invoke(DispatcherPriority.Background, (DispatcherOperationCallback)delegate { return this.GetValue(DefaultFolderProperty); }, DefaultFolderProperty);
        }
        else
        {
            return (string)this.GetValue(DefaultFolderProperty);
        }
    }
    set
    {
        if (!this.Dispatcher.CheckAccess())
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,(SendOrPostCallback)delegate { this.SetValue(DefaultFolderProperty, value); }, value);
        }
        else
        {
            this.SetValue(DefaultFolderProperty, value);
        }
    }
}*/

/*private void AddFileDialog()
{
    System.Windows.Forms.OpenFileDialog vfb = new System.Windows.Forms.OpenFileDialog();
    vfb.Multiselect = true;
    vfb.Title = "Add Files";
    vfb.InitialDirectory = DefaultFolder;
    System.Windows.Forms.DialogResult dr = vfb.ShowDialog();

    if (dr == System.Windows.Forms.DialogResult.OK)
    {
        this.Dispatcher.Invoke(DispatcherPriority.Send, new Action(delegate
        {
            for (var i = 0; i < vfb.FileNames.Length; i++)
            {
                ListBoxFileName filename = new ListBoxFileName();
                filename.FileName = vfb.FileNames[i];
                filename.FileIsSelected = false;
                FileNamesItems.Add(filename);
            }
        }));
    }
}*/

/*private void AddFiles_Click(object sender, RoutedEventArgs e)
{
    t = new Thread(new ThreadStart(AddFileDialog));
    t.SetApartmentState(ApartmentState.STA);
    t.Start();
}*/

/*private void ClearFiles_Click(object sender, RoutedEventArgs e)
{
    FileNamesItems.Clear();
}*/

/*private void SelectFolderDialog()
{
    WPFFolderBrowserDialog vfb = new WPFFolderBrowserDialog();
    vfb.InitialDirectory = DefaultFolder;
    if (vfb.ShowDialog() ?? false)
    {
        this.Dispatcher.Invoke(DispatcherPriority.Send, new Action(delegate
        {
            DefaultFolder = vfb.FileName;
        }));
    }
}*/

/*private void SelectFolder_Click(object sender, RoutedEventArgs e)
{
    t = new Thread(new ThreadStart(SelectFolderDialog));
    t.SetApartmentState(ApartmentState.STA);
    t.Start();
}*/



